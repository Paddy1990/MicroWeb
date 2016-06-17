using MicroWeb.Config.Models;

namespace MicroWeb.Config.Interfaces
{
	public interface IConfigManager
	{
		Configuration Configuration { get; }
	}
}