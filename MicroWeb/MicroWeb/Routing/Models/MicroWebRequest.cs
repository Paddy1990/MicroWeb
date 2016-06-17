using System;
using System.Collections.Specialized;

namespace MicroWeb.Routing.Models
{
	public class MicroWebRequest
	{
		public NameValueCollection QueryStrings { get; set; }
		public NameValueCollection Headers { get; set; }
		public string HttpMethod { get; set; }
		public string RawUrl { get; set; }
		public Uri Url { get; set; }
	}
}