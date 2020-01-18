using System;
using System.Collections.Generic;
using System.Net;

namespace Redmine.Net.Api.Internals
{
    internal sealed class RestResponse
    {
        public int StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public string Content { get; set; }
        
        public string ContentType { get; set; }
        
        public Dictionary<string,string> Headers { get; set; }
        
        public Uri ResponseUri { get; set; }

        public CookieCollection Cookies { get; set; }
    }
}