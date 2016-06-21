using MicroWeb.Routing.Models;

namespace MicroWeb
{
	public interface IMicroWeb
	{
		void AppStart();
		byte[] ResolveRoute(MicroWebRequest microWebRequest);
	}
}