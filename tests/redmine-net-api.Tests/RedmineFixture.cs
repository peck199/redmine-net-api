
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Redmine.Net.Api;

namespace redmine.net.api.Tests
{
    public class RedmineFixture
    {
        public RedmineManager RedmineManager { get; set; }

        internal RedmineOptions Options { get; set; }


        public RedmineFixture()
        {
            var builder = new ConfigurationBuilder();

            builder.AddUserSecrets("f8b9e946-b547-42f1-861c-f719dca00a84");

            var Configuration = builder.Build();

            var u = Configuration["Redmine401:Username"];
            var p = Configuration["Redmine401:Password"];
            var a = Configuration["Redmine401:ApiKey"];
            var h = Configuration["Redmine401:Host"];

            Options = new RedmineOptions()
            {
                Uri = h, Username = u, ApiKey = a, Password = p
            };

            SetMimeTypeXML();
            SetMimeTypeJSON();
        }

        [Conditional("JSON")]
        private void SetMimeTypeJSON()
        {
            RedmineManager = new RedmineManager(Options.Uri, Options.ApiKey, MimeFormat.Json);
        }

        [Conditional("XML")]
        private void SetMimeTypeXML()
        {
            RedmineManager = new RedmineManager(Options.Uri, Options.ApiKey);
        }
    }
}