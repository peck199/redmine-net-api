
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Redmine.Net.Api.Extensions;

namespace Redmine.Net.Api.Internals
{
    internal sealed partial class RestClient : IRestClient, ISyncRestClient
    {
        private readonly RedmineRestApiSettings _settings;
        
        public RestClient(RedmineRestApiSettings settings)
        {
            _settings = settings;
        }

        public RestResponse Create<T>(RestRequest<T> request) where T : class
        {
            throw new NotImplementedException();
        }

        public RestResponse Update<T>(RestRequest<T> request) where T : class
        {
            throw new NotImplementedException();
        }

        public RestResponse Delete(RestRequest<int> request)
        {
            throw new NotImplementedException();
        }

        public RestResponse Get(RestRequest request)
        {
            var wr = CreateHttpWebRequest(request);

            var response = GetWebResponse(wr);

            return response;
        }

        private WebRequest CreateHttpWebRequest(RestRequest restRequest)
        {
            UriBuilder uriBuilder = new UriBuilder(restRequest.Uri)
            {
                Query = restRequest.Parameters.ToQueryString()
            };

            var request = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);

            request.Credentials = _settings.Credentials;
            request.Method = restRequest.Method;
            request.CachePolicy = _settings.CachePolicy;
            // request.AuthenticationLevel = 
            request.ConnectionGroupName = string.Empty;
            request.ContentType = restRequest.ContentType;
            // request.ImpersonationLevel = 
            // request.PreAuthenticate = _settings.
            request.Proxy = _settings.Proxy;
            request.Timeout = _settings.Timeout;
            request.UseDefaultCredentials = _settings.DefaultCredentials;


            var token = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{_settings.Username}:{_settings.Password}"));
            var basicAuthorization = $"Basic {token}";


            if (_settings.ProtocolVersion != null)
            {
                request.Headers.Add("Version", _settings.ProtocolVersion.ToString());
            }

            if (!_settings.Referer.IsNullOrWhiteSpace())
            {
                request.Headers.Add("Referrer", _settings.Referer);
            }

            if (!_settings.UserAgent.IsNullOrWhiteSpace())
            {
                request.Headers.Add("User-Agent", _settings.UserAgent);
            }

            //if (!string.IsNullOrEmpty(ImpersonateUser))
            //{
            //    request.Headers.Add("X-Redmine-Switch-User", ImpersonateUser);
            //}

            if (!_settings.ApiKey.IsNullOrWhiteSpace())
            {
                request.Headers.Add(RedmineKeys.KEY, _settings.ApiKey);
            }

            return request;
        }

        private void WriteData(WebRequest wr, string inputData, string contentType, Encoding encoding)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] requestBytes = encoding.GetBytes(inputData);

            using (var requestStream = wr.GetRequestStream())
            {
                wr.ContentType = contentType;
                wr.ContentLength = requestBytes.Length;

                requestStream.Write(requestBytes, 0, requestBytes.Length);
            }
        }


        private static RestResponse GetWebResponse(WebRequest wr)
        {
            try
            {
                using (var response = (HttpWebResponse)wr.GetResponse())
                {
                    var encoding = Encoding.GetEncoding(response.CharacterSet);
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseStream, encoding))
                        {
                            var content = streamReader.ReadToEnd();
                            return new RestResponse
                            {
                                Content = content,
                                ContentType = response.ContentType,
                                ResponseUri = response.ResponseUri,
                                Headers = response.Headers.ToDictionary(),
                                StatusCode = (int)response.StatusCode,
                                StatusDescription = response.StatusDescription,
                                Cookies = response.Cookies
                            };
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                if (wex.Response is HttpWebResponse httpResponse)
                {
                    throw new ApplicationException(
                        $"Remote server call {wr.Method} {wr.RequestUri} resulted in a http error {httpResponse.StatusCode.ToString()} {httpResponse.StatusDescription}.", wex);
                }

                throw new ApplicationException($"Remote server call {wr.Method} {wr.RequestUri} resulted in an error.", wex);
            }
        }


        //        public static void AddTls11AndTls12()
        //        {
        //#if (NET20 || NET40)
        //            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls;
        //#else
        //            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
        //#endif
        //        }


        public static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }


        public static void DisableCertificateValidation()
        {
#if NETFRAMEWORK
            ServicePointManager.ServerCertificateValidationCallback += AcceptAllCertifications;
#else

#endif
        }
    }
}