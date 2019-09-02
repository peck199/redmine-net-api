
using System;
using System.Collections.Specialized;
#if (NET20 || NET40)
using System.Net;
using System.Text;
using RedmineClient.Extensions;
using RedmineClient.Internals.Serialization;
using RedmineClient.Types;

namespace RedmineClient.Internals
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class RedmineHttpClient : IRedmineRestClient
    {
        private readonly IRedmineApiClientSettings apiClientSettings;
        private readonly IRedmineSerializer serializer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiClientSettings"></param>
        /// <param name="serializer"></param>
        public RedmineHttpClient(IRedmineApiClientSettings apiClientSettings,IRedmineSerializer serializer)
        {
            this.apiClientSettings = apiClientSettings;
            this.serializer = serializer;
        }

        public TOut Get<TOut>(string url, NameValueCollection parameters) where TOut : class, new()
        {
            using (var webClient = new RedmineWebClient(apiClientSettings))
            {
                try
                {
                    webClient.QueryString = parameters;
                    var response = webClient.DownloadString(url);

                    return serializer.Deserialize<TOut>(response);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(serializer);
                }
                return default(TOut);
            }
        }

        public PagedResults<TOut> List<TOut>(string url) where TOut : class, new()
        {
            using (var webClient = new RedmineWebClient(apiClientSettings))
            {
                try
                {
                    var response = webClient.DownloadString(url);

                    return serializer.DeserializeToPagedResults<TOut>(response);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(serializer);
                }
                return default(PagedResults<TOut>);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serializedData"></param>
        public T Post<T>(string url, string serializedData) where T : class, new()
        {
            using (var webClient = new RedmineWebClient(apiClientSettings))
            {
                try
                {
                    var response = webClient.UploadString(url, HttpVerbs.POST, serializedData);
                    return serializer.Deserialize<T>(response);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(serializer);
                }
            }

            return default(T);
        }

        public void Post(string url, string serializedData)
        {
            using (var webClient = new RedmineWebClient(apiClientSettings))
            {
                try
                {
                    var response = webClient.UploadString(url, HttpVerbs.POST, serializedData);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(serializer);
                }
            }
        }

        public void Put(string url, string serializedData)
        {
            using (var webClient = new RedmineWebClient(apiClientSettings))
            {
                try
                {
                    webClient.UploadString(url, HttpVerbs.PUT, serializedData);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(serializer);
                }
            }
        }

        public T Put<T>(string url, string serializedData) where T : class, new()
        {
            using (var webClient = new RedmineWebClient(apiClientSettings))
            {
                try
                {
                    var response = webClient.UploadString(url, HttpVerbs.PUT, serializedData);
                    return serializer.Deserialize<T>(response);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(serializer);
                }
            }

            return default(T);
        }

        public void Patch(string url, string serializedData)
        {
            using (var webClient = new RedmineWebClient(apiClientSettings))
            {
                try
                {
                    webClient.UploadString(url, HttpVerbs.PATCH, serializedData);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(serializer);
                }
            }
        }

        public T Patch<T>(string url, string serializedData) where T : class, new()
        {
            using (var webClient = new RedmineWebClient(apiClientSettings))
            {
                try
                {
                    var response = webClient.UploadString(url, HttpVerbs.PATCH, serializedData);
                    return serializer.Deserialize<T>(response);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(serializer);
                }
            }

            return default(T);
        }

        public void Delete(string url)
        {
            DoActionRequest((wc) => wc.UploadString(url, HttpVerbs.DELETE));
        }

        public T Delete<T>(string url) where T : class, new()
        {
            var response = DoRequest((wc) => wc.UploadString(url, HttpVerbs.DELETE));

            return serializer.Deserialize<T>(response);
        }

        public byte[] Download(string url)
        {
            using (var webClient = new RedmineWebClient(apiClientSettings))
            {
                try
                {
                    return webClient.DownloadData(url);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(serializer);
                }
                return null;
            }
        }

        public Upload Upload(string address, byte[] data)
        {
            using (var webClient = new RedmineWebClient(apiClientSettings))
            {
                try
                {
                    webClient.Headers.Add(HttpRequestHeader.ContentType, "application/octet-stream");
                    var response = webClient.UploadData(address, data);
                    var responseString = Encoding.ASCII.GetString(response);
                    return serializer.Deserialize<Upload>(responseString);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(serializer);
                }
                return null;
            }
        }

        private string DoRequest(Func<RedmineWebClient, string> func)
        {
            using (var webClient = new RedmineWebClient(apiClientSettings))
            {
                try
                {
                    return func(webClient);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(serializer);
                }
                return default(string);
            }
        }

        private void DoActionRequest(Action<RedmineWebClient> action)
        {
            using (var webClient = new RedmineWebClient(apiClientSettings))
            {
                try
                {
                     action(webClient);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(serializer);
                }
               
            }
        }
    }
}
#endif