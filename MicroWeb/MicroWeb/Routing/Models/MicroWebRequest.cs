using System;
using System.Collections.Specialized;

namespace MicroWeb.Routing.Models
{
	public class MicroWebRequest
	{
		public NameValueCollection QueryStrings { get; set; }
		public NameValueCollection Headers { get; set; }
		public string HttpMethod { get; set; }
		public string AbsolutePath { get; set; }
		public Uri Uri { get; set; }
	}
}