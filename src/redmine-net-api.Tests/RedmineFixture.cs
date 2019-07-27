using System.Diagnostics;

namespace redmine.net.api.Tests
{
	public class RedmineFixture
	{
	    public RedmineManager RedmineManager { get; set; }

	    public RedmineFixture ()
		{
			SetMimeTypeXML();
			SetMimeTypeJSON();
		}

		[Conditional("JSON")]
		private void SetMimeTypeJSON()
		{
			
		}

		[Conditional("XML")]
		private void SetMimeTypeXML()
		{
			
		}
	}
}