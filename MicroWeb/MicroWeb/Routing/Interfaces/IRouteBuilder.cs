using System.Collections.Generic;
using MicroWeb.Routing.Models;

namespace MicroWeb.Routing.Interfaces
{
	public interface IRouteBuilder
	{
		byte Build(MicroWebRoute route);
	}
}