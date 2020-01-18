using System.Configuration;

namespace redmine.net.api.Tests
{
	internal sealed class RedmineOptions
	{
		public  string Uri { get;  set; }

		public  string ApiKey { get;  set; }

		public  string Username { get;  set; }

		public  string Password { get;  set; }
	}
}