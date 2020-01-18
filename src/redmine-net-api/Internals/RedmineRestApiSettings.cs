using System.Net;
using System.Net.Cache;
using System.Net.Security;

namespace Redmine.Net.Api.Internals
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RedmineRestApiSettings
    {
        public string ApiKey { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Host { get; private set; }
        public string UserAgent { get; private set; }
        public RedmineApiSerializationType ApiSerializationType { get; private set; }
        public bool IsHttps { get; private set; }
        public bool DefaultCredentials { get; private set; }
        public bool KeepAlive { get; private set; }
        public IWebProxy Proxy { get; private set; }
        public SecurityProtocolType ProtocolType { get; private set; }
        public RemoteCertificateValidationCallback CertificateValidationCallback { get; private set; }
        public CookieContainer Container { get; private set; }
        public ICredentials Credentials { get; private set; }
        public RequestCachePolicy CachePolicy { get; private set; }

        public DecompressionMethods DecompressionMethods { get; private set; } =
            DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.None;
        public int Timeout { get; private set; }

        public string ContentType { get; private set; }
        public string Format { get; private set; }
        public string Referer { get; private set; }
        public System.Version ProtocolVersion { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializationType"></param>
        public RedmineRestApiSettings(RedmineApiSerializationType serializationType)
        {
            SetApiSerializationType(serializationType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public RedmineRestApiSettings UseApiKeyAuthentication(string apiKey)
        {
            ApiKey = apiKey;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public RedmineRestApiSettings UseBasicAuthentication(string username, string password)
        {
            Username = username;
            Password = password;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public RedmineRestApiSettings SetHost(string host)
        {
            Host = host;
            return this;
        }

        private RedmineRestApiSettings SetApiSerializationType(RedmineApiSerializationType redmineApiSerializationType)
        {
            ApiSerializationType = redmineApiSerializationType;
            if (redmineApiSerializationType == RedmineApiSerializationType.Json)
            {
                ContentType = "application/json";
                Format = "json";
            }
            else
            {
                ContentType = "application/xml";
                Format = "xml"; 
            }
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RedmineRestApiSettings UseHttps()
        {
            IsHttps = true;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public RedmineRestApiSettings SetProxy(IWebProxy proxy)
        {
            Proxy = proxy;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityProtocolType"></param>
        /// <returns></returns>
        public RedmineRestApiSettings SetSecurityProtocolType(SecurityProtocolType securityProtocolType)
        {
            ProtocolType = securityProtocolType;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteCertificateValidationCallback"></param>
        /// <returns></returns>
        public RedmineRestApiSettings UseRemoteCertificateValidationCallback(RemoteCertificateValidationCallback remoteCertificateValidationCallback)
        {
            CertificateValidationCallback = remoteCertificateValidationCallback;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public RedmineRestApiSettings SetUserAgent(string userAgent)
        {
            UserAgent = userAgent;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public RedmineRestApiSettings SetCookies(CookieContainer cookieContainer)
        {
            Container = cookieContainer;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public RedmineRestApiSettings UseCredentials(ICredentials credentials)
        {
            Credentials = credentials;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestCachePolicy"></param>
        /// <returns></returns>
        public RedmineRestApiSettings UseCachePolicy(RequestCachePolicy requestCachePolicy)
        {
            CachePolicy = requestCachePolicy;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decompressionMethods"></param>
        /// <returns></returns>
        public RedmineRestApiSettings SetDecompressionMethods(DecompressionMethods decompressionMethods)
        {
            DecompressionMethods = decompressionMethods;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public RedmineRestApiSettings SetTimeout(int milliseconds)
        {
            Timeout = milliseconds;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keepAlive"></param>
        /// <returns></returns>
        public RedmineRestApiSettings SetKeepAlive(bool keepAlive)
        {
            KeepAlive = keepAlive;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RedmineRestApiSettings UseDefaultCredentials()
        {
            DefaultCredentials = true;
            return this;
        }

        
    }
}