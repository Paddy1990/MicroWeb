using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MicroWeb.Config.Interfaces;
using MicroWeb.Constants;
using MicroWeb.FileSystem.Interfaces;
using MicroWeb.Routing.Extensions;
using MicroWeb.Routing.Interfaces;
using MicroWeb.Routing.Models;

namespace MicroWeb.Routing
{
	public class RouteHandler : IRouteHandler
	{
		private readonly IConfigManager _configManager;
		private readonly IFileSystemProvider _fileSystemProvider;
		private readonly IRouteBuilder _routeBuilder;

		private IDictionary<string, MicroWebRoute> Routes { get; set; }
		
		public RouteHandler(IConfigManager configManager, IFileSystemProvider fileSystemProvider, IRouteBuilder routeBuilder)
		{
			_configManager = configManager;
			_fileSystemProvider = fileSystemProvider;
			_routeBuilder = routeBuilder;

			Routes = new Dictionary<string, MicroWebRoute>();
		}

		public IDictionary<string, MicroWebRoute> ConfigureRoutes()
		{
			//TODO: Get the base directory from config!
			ConfigureRoute(_configManager.Configuration.BaseDirectory, true);
			return Routes;
		}

		private void ConfigureRoute(string baseDirectory, bool isBaseDirectory = false)
		{
			//Get All files in directory
			var files = _fileSystemProvider.GetFiles(
				baseDirectory, _configManager.Configuration.FileTypes).ToList();

			//TODO: Think I may have to do something about the base directory / Home page.
			//Check for an index.html file in the base directory. 
			//If there isn't one look for index folder.
			//If there isn't one throw can't find Index page maybe,
			//or just throw when the user gets to the page? Can'tcfind route/file exception?
			if (isBaseDirectory)
			{
				//Find the base file.
				//TODO:Maybe change this to get it from config? or just always look for an index.html page for now...
				var baseFile = files.FirstOrDefault(f => f.Name == Match.BaseFileName);
				//TODO: Add check to baseFile...
				var directory = baseFile.Directory ?? new DirectoryInfo(baseDirectory);
				var baseRoute = MapBaseRoute(directory, baseFile);
				Routes[baseRoute.Url] = baseRoute;
			}
			else
			{
				foreach (var fileInfo in files)
				{
					var directory = fileInfo.Directory ?? new DirectoryInfo(baseDirectory);
					var route = MapRouteModel(directory, fileInfo);
					Routes[route.Url] = route;
				}
			}

			var subDirectories = _fileSystemProvider.GetDirectories(baseDirectory, "*", SearchOption.AllDirectories);
			//Recersively Call the ConfigureRoute(string path) method to populate all routes.
			foreach (var directoryInfo in subDirectories)
				ConfigureRoute(directoryInfo.FullName);
		}

		private MicroWebRoute MapBaseRoute(DirectoryInfo baseDirectory, FileInfo fileInfo)
		{
			var route = MapRouteModel(baseDirectory, fileInfo);
			route.Url = "/" + route.FileName;
			return route;
		}

		public byte ResolveRoute(MicroWebRequest request, IDictionary<string, MicroWebRoute> routes)
		{
			var uri = request.Uri;
			
			//If it's a request for a route. Infer the html file from the route
			//Thought the best way to do this was check if the request is for a file, 
			//rather than check if it's a route that has been requested. E.g. /contact rather than /source.js.
			if (!request.IsFile())
			{
				MicroWebRoute route;
				var isBaseRoute = request.AbsolutePath == "/" ? true : false;
				if (isBaseRoute)
				{
					if (!routes.ContainsKey(request.AbsolutePath + Match.BaseFileName))
						throw new Exception("Can't find route...");

					route = routes[request.AbsolutePath + Match.BaseFileName];
				}
				else
				{
					if (routes.ContainsKey(request.AbsolutePath + char.ToUpper(request.AbsolutePath.First()) + request.AbsolutePath.Substring(1) + ".html"))
						throw new Exception("Can't find route...");

					route = routes[request.AbsolutePath + char.ToUpper(request.AbsolutePath.First()) + request.AbsolutePath.Substring(1) + ".html"];
				}
				//Read the file and return string - could maybe just return in bytes?
				//TODO: change to return bytes!
				return _routeBuilder.Build(route);
			}

			return "<!DOCTYPE html><html><head><script src='/source.js'></script></head><body><div><p>Hello World</p></div></body></html>";
		}

		private MicroWebRoute MapRouteModel(DirectoryInfo baseDirectory, FileInfo fileInfo)
		{
			var filePath = GetAbsolutePath(fileInfo.FullName, _configManager.Configuration.BaseDirectory);
			return new MicroWebRoute
			{
				DirectoryName = baseDirectory.Name,
				AbsoluteDirectoryPath = baseDirectory.FullName,
				DirectoryPath = GetAbsolutePath(baseDirectory.FullName, _configManager.Configuration.BaseDirectory),
				FileName = fileInfo.Name.ToLower(),
				FilePath = filePath,
				AbsoluteFilePath = fileInfo.FullName,
				FileExtension = fileInfo.Extension,
				Url = ConvertToUrl(filePath)
			};
		}

		private string ConvertToUrl(string filePath)
		{
			return filePath.Replace(@"\", @"/");
		}

		private static string GetAbsolutePath(string fullPath, string baseDirectory)
		{
			//TODO:Need to fix this as it's breaking for base directory files. 
			//Not sure how to get around this base directory / home page issue...
			var directory = baseDirectory.TrimEnd('\\');
			var baseDirectoryIndex = fullPath.LastIndexOf(directory, StringComparison.Ordinal);
			if (baseDirectoryIndex == -1)
				throw new Exception("The base Directory was not found in the full path");

			var relativeStartIndex = baseDirectoryIndex + directory.Length;
			if (relativeStartIndex > fullPath.Length)
				throw new Exception("The start index of the relative path was greater than the fullpath!");

			return fullPath.Substring(relativeStartIndex);
		}

	}
}