using System;
using System.Collections.Specialized;
using System.IO;
using MicroWeb.Routing.Models;

namespace MicroWeb.Routing.Extensions
{
	public static class MicroWebRequestExtension
	{
		public static bool IsFile(this MicroWebRequest request)
		{
			return Path.HasExtension(request.RawUrl);
		}
	}
}