using System.Collections.Generic;
using MicroWeb.Routing.Models;

namespace MicroWeb.Routing.Interfaces
{
	public interface IRouteHandler
	{
		IDictionary<string, MicroWebRoute> ConfigureRoutes();
	    byte[] ResolveRoute(MicroWebRequest request, IDictionary<string, MicroWebRoute> routes);
	}
}