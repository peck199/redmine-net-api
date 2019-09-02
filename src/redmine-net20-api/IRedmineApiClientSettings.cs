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
    public interface IRedmineApiClientSettings
    {
        /// <summary>
        /// 
        /// </summary>
        string HostUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        RedmineSerializationType SerializationType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string UserAgent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool UseProxy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool UseCookies { get; set; }

        /// <summary>
        /// 
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool PreAuthenticate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool KeepAlive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        NameValueCollection QueryString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ICredentials Credentials { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IWebProxy Proxy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        RequestCachePolicy CachePolicy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IRedmineAuthentication Authentication { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int MaximumAutomaticRedirections { get; set; }

        /// <summary>
        /// 
        /// </summary>
        X509CertificateCollection ClientCertificates { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool AllowAutoRedirect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Referer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string TransferEncoding { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool UseApiKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ApiKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ImpersonateUser { get; set; }
    }
}