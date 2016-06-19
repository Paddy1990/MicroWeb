using MicroWeb.Routing.Models;

namespace MicroWeb
{
	public interface IMicroWeb
	{
		void AppStart();
		string ResolveRoute(MicroWebRequest microWebRequest);
	}
}