using System.Collections.Generic;
using IoC.Container;
using IoC.Container.FluentApi;
using MicroWeb.Config;
using MicroWeb.Config.Interfaces;
using MicroWeb.FileSystem;
using MicroWeb.FileSystem.Interfaces;
using MicroWeb.Routing;
using MicroWeb.Routing.Interfaces;
using MicroWeb.Routing.Models;

namespace MicroWeb
{
	public class MicroWeb : IMicroWeb
	{
		private readonly IocContainer _iocContainer;
		private IConfigManager _configManager;

		private IDictionary<string, MicroWebRoute> Routes { get; set; }

		public MicroWeb()
		{
			_iocContainer = new IocContainer(RegisterDependencies);

			_configManager = _iocContainer.Resolve<IConfigManager>();

			var routeHandler = _iocContainer.Resolve<IRouteHandler>();
			Routes = routeHandler.ConfigureRoutes();
		}

		public string ResolveRoute(MicroWebRequest microWebRequest)
		{
			var routeHandler = _iocContainer.Resolve<IRouteHandler>();
			return routeHandler.ResolveRoute(microWebRequest, Routes);
		}

		private static void RegisterDependencies(IocBuilder builder)
		{
			builder.Register<IRouteHandler>().Concrete<RouteHandler>();

			builder.Register<IConfigManager>().Concrete<ConfigManager>().AsSingleton();
			builder.Register<IConfigLoader>().Concrete<ConfigLoader>();

			builder.Register<IFileSystemProvider>().Concrete<FileSystemProvider>();
			builder.Register<IJsonReader>().Concrete<JsonReader>();
		}
	}
}
