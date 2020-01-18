using System.Collections.Generic;
using System.Collections.Specialized;

namespace Redmine.Net.Api.Internals
{
    internal class RestRequest
    {
        public string Method { get; set; }
        public string Uri { get; set; }
        public string ContentType { get; set; }

        public Dictionary<string,string> Headers { get; set; }
        // headers.Add("Authorization", "Bearer "+oAuthBearerToken);
        public NameValueCollection Parameters { get; set; }
    }

    /// <inheritdoc />
    internal sealed class RestRequest<TIn> : RestRequest
    {
        public TIn Data { get; set; }
    }
}