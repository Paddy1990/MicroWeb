using MicroWeb.Config.Models;

namespace MicroWeb.Config.Interfaces
{
	public interface IConfigLoader
	{
		Configuration Load();
	}
}