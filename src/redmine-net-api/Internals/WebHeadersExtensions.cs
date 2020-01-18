using System.Collections.Generic;
using System.Net;

namespace Redmine.Net.Api.Internals
{
    internal static class WebHeadersExtensions
    {
        public static Dictionary<string, string> ToDictionary(this WebHeaderCollection headerCollection)
        {
            if (headerCollection == null)
            {
                return null;
            }
            
            Dictionary<string,string> headers = new  Dictionary<string, string>();

            foreach (string key in headerCollection.Keys)
            {
                headers.Add(key, headerCollection[key]);
            }

            return headers;
        }

        public static void DisableProxyForRequest(this WebRequest request)
        {
            request.Proxy = null;
        }
    }
}