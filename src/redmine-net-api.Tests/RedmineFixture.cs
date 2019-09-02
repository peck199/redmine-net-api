using System.Diagnostics;
using RedmineClient;

namespace redmine.net.api.Tests
{
	public class RedmineFixture
    {
        public IRedmineApiClientSettings ClientSettings { get; }
        public RedmineApiClient RedmineApiClient { get; private set; }

	    public RedmineFixture ()
		{
            ClientSettings = new RedmineApiClientSettings();
			SetMimeTypeXML();
			SetMimeTypeJSON();
            RedmineApiClient = new RedmineApiClient(ClientSettings);
		}

		[Conditional("JSON")]
		private void SetMimeTypeJSON()
        {
            ClientSettings.SerializationType = RedmineSerializationType.Json;

        }

		[Conditional("XML")]
		private void SetMimeTypeXML()
		{
            ClientSettings.SerializationType = RedmineSerializationType.Xml;
        }
	}
}