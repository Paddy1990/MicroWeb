using MicroWeb.Routing.Models;

namespace MicroWeb
{
	public interface IMicroWeb
	{
		string ResolveRoute(MicroWebRequest microWebRequest);
	}
}