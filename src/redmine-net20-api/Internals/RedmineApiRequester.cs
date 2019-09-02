
using System;
using System.Collections.Specialized;
using RedmineClient.Extensions;
using RedmineClient.Internals.Serialization;
#if (NET20 || NET40)
using System.Net;
#else
using System.Net.Http;
#endif

namespace RedmineClient.Internals
{
    internal sealed class RedmineApiRequester : IRedmineApiRequester
    {
        private readonly IRedmineHttpClientFactory httpClientFactory;
        private readonly IRedmineApiClientSettings apiClientSettings;
        private readonly IRedmineSerializer serializer;

        public RedmineApiRequester(IRedmineHttpClientFactory httpClientFactory, IRedmineApiClientSettings apiClientSettings,IRedmineSerializer serializer)
        {
            this.httpClientFactory = httpClientFactory;
            this.apiClientSettings = apiClientSettings;
            this.serializer = serializer;
        }

        public T Get<T>(string url, NameValueCollection parameters) where T : class, new()
        {
            var client = httpClientFactory.GetClient(apiClientSettings,serializer);
            return client.Get<T>(url+serializer.Type, parameters);
        }

        public void Post<T>(string url, T data) where T : class, new()
        {
            var serializedData = SerializeData(data);
            var client = httpClientFactory.GetClient(apiClientSettings, serializer);
            client.Post(url, serializedData);
        }

        public void Put<T>(string url, T data) where T : class, new()
        {
            var serializedData = SerializeData(data);
            var client = httpClientFactory.GetClient(apiClientSettings, serializer);
            client.Put(url, serializedData);
        }

        public void Patch<T>(string url, T data) where T : class, new()
        {
            var serializedData = SerializeData(data);
            var client = httpClientFactory.GetClient(apiClientSettings, serializer);
            client.Patch(url, serializedData);
        }

        public void Delete(string url)
        {
            var client = httpClientFactory.GetClient(apiClientSettings, serializer);
            client.Delete(url);
        }

        private string SerializeData<T>(T data) where T : class, new()
        {
            var stringContent = data == default(T) ? string.Empty : serializer.Serialize(data);

            return stringContent;

            //TODO: trebuie setat header  content to "application/json"
        }
    }

    internal interface IRedmineApiRequester
    {
        T Get<T>(string url, NameValueCollection parameters) where T : class, new();
    }


#if (NET20 || NET40)

    internal sealed class RedmineWebClient : WebClient
    {
        private const string X_REDMINE_SWITCH_USER = "X-Redmine-Switch-User";
        private const string X_REDMINE_API_KEY = "X-Redmine-API-Key";

        private readonly IRedmineApiClientSettings apiClientSettings;

        public RedmineWebClient(IRedmineApiClientSettings apiClientSettings)
        {
            this.apiClientSettings = apiClientSettings;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var wr = base.GetWebRequest(address);

            if (wr is HttpWebRequest httpWebRequest)
            {
                if (apiClientSettings.UseCookies)
                {
                    httpWebRequest.Headers.Add(HttpRequestHeader.Cookie, "redmineCookie");
                    httpWebRequest.CookieContainer = apiClientSettings.CookieContainer;
                }

                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate |
                                                        DecompressionMethods.None;

                httpWebRequest.UserAgent = apiClientSettings.UserAgent ?? "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.163 Safari/535.1";
                
                httpWebRequest.KeepAlive = apiClientSettings.KeepAlive;
                httpWebRequest.Referer = apiClientSettings.Referer;
                httpWebRequest.TransferEncoding = apiClientSettings.TransferEncoding;

                httpWebRequest.AllowAutoRedirect = apiClientSettings.AllowAutoRedirect;
                if(apiClientSettings.ClientCertificates != null)
                {
                    httpWebRequest.ClientCertificates = apiClientSettings.ClientCertificates;
                }

                if (apiClientSettings.MaximumAutomaticRedirections > 0)
                {
                    httpWebRequest.MaximumAutomaticRedirections = apiClientSettings.MaximumAutomaticRedirections;
                }

                httpWebRequest.PreAuthenticate = apiClientSettings.PreAuthenticate;

                if (apiClientSettings.UseApiKey)
                {
                    httpWebRequest.Headers.Add(X_REDMINE_API_KEY ,apiClientSettings.ApiKey);
                }
                else
                {
                    httpWebRequest.Credentials = apiClientSettings.Credentials;
                    httpWebRequest.UseDefaultCredentials = apiClientSettings.UseDefaultCredentials;
                    httpWebRequest.UseDefaultCredentials = apiClientSettings.Credentials != null;
                }

                httpWebRequest.CachePolicy = apiClientSettings.CachePolicy;

                if (apiClientSettings.UseProxy)
                {
                    if (apiClientSettings.Proxy != null)
                    {
                        Proxy = apiClientSettings.Proxy;
                        Proxy.Credentials = apiClientSettings.Credentials;
                    }
                }

                if (apiClientSettings.Timeout.HasValue)
                {
                    httpWebRequest.Timeout = apiClientSettings.Timeout.Value.Milliseconds;
                }

                if (!apiClientSettings.ImpersonateUser.IsNullOrWhiteSpace())
                {
                    httpWebRequest.Headers.Add(X_REDMINE_SWITCH_USER, apiClientSettings.ImpersonateUser);
                }

                return httpWebRequest;
            }

            return base.GetWebRequest(address);
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = null;

            try
            {
                response = base.GetWebResponse(request);
            }
            catch (Exception e)
            {
                if (e is WebException)
                {
                    if (IgnoreErrors)
                    {
                        WebException we = e as WebException;
                        if (we.Status == WebExceptionStatus.ProtocolError)
                        {
                            response = we.Response;
                        }
                    }
                    else throw e;
                }
            }

            if (response == null) return null;

            if (response is HttpWebResponse)
            {
                HandleRedirect(request, response);
                HandleCookies(request, response);
            }

            return response;
        }

        private void HandleCookies(WebRequest request, WebResponse response)
        {

        }

        private void HandleRedirect(WebRequest request, WebResponse response)
        {

        }

    }
#endif
#if !(NET20 || NET40)
/// <summary>
    /// 
    /// </summary>
    public sealed class RedmineWebClient: HttpClient//, IRedmineRestClient
    {
        
}
#endif

    /// <summary>
    /// 
    /// </summary>
    public interface IResult
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    public interface IResult<out TOut> : IResult
    {
        /// <summary>
        /// 
        /// </summary>
        TOut Data { get; }
    }
}