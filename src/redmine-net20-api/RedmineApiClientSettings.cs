using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Cache;
using System.Security.Cryptography.X509Certificates;

namespace RedmineClient
{
    /// <summary>
    /// 
    /// </summary>
    public class RedmineApiClientSettings : IRedmineApiClientSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string HostUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RedmineSerializationType SerializationType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UseProxy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UseCookies { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool PreAuthenticate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public NameValueCollection QueryString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICredentials Credentials { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IWebProxy Proxy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RequestCachePolicy CachePolicy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IRedmineAuthentication Authentication { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MaximumAutomaticRedirections { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public X509CertificateCollection ClientCertificates { get; set; }

        /// <inheritdoc />
        public bool AllowAutoRedirect { get; set; }

        /// <inheritdoc />
        public string Referer { get; set; }

        /// <inheritdoc />
        public string TransferEncoding { get; set; }

        /// <inheritdoc />
        public bool UseApiKey { get; set; }

        /// <inheritdoc />
        public string ApiKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImpersonateUser { get; set; } 
    }
}