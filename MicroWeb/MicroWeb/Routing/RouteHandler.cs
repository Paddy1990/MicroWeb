using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MicroWeb.Config.Interfaces;
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

		private IDictionary<string, MicroWebRoute> Routes { get; set; }
		
		public RouteHandler(IConfigManager configManager, IFileSystemProvider fileSystemProvider)
		{
			_configManager = configManager;
			_fileSystemProvider = fileSystemProvider;

			Routes = new Dictionary<string, MicroWebRoute>();
		}

		public IDictionary<string, MicroWebRoute> ConfigureRoutes()
		{
			//TODO: Get the base directory from config!
			ConfigureRoute(_configManager.Configuration.BaseDirectory);
			return Routes;
		}

		private void ConfigureRoute(string baseDirectory)
		{
			//Get All files in directory
			var files = _fileSystemProvider.GetFiles(
				baseDirectory, _configManager.Configuration.FileTypes).ToList();

			foreach (var fileInfo in files)
			{
				var directory = fileInfo.Directory ?? new DirectoryInfo(baseDirectory);
				var route = MapRouteModel(directory, fileInfo);

				Routes[route.FilePath] = route;
			}

			var subDirectories = _fileSystemProvider.GetDirectories(baseDirectory, "*", SearchOption.AllDirectories);
			//Recersively Call the ConfigureRoute(string path) method to populate all routes.
			foreach (var directoryInfo in subDirectories)
				ConfigureRoute(directoryInfo.FullName);
		}
		
		public string ResolveRoute(MicroWebRequest request, IDictionary<string, MicroWebRoute> routes)
		{
			var a = request.Url;
			
			//If it's a request for a route. Infer the html file from the route
			if (!request.IsFile())
			{
				var b = routes.ContainsKey(request.RawUrl + "/" + 
					char.ToUpper(request.Url.Segments.Last()[0]) + request.Url.Segments.Last().Substring(1) + ".html");

				//Read the file and return string - could maybe just return in bytes?
				//TODO: change to return bytes!
				return "HTML";
			}

			return "<!DOCTYPE html><html><head><script src='/source.js'></script></head><body><div><p>Hello World</p></div></body></html>";
		}

		private MicroWebRoute MapRouteModel(DirectoryInfo baseDirectory, FileInfo fileInfo)
		{
			return new MicroWebRoute
			{
				DirectoryName = baseDirectory.Name,
				AbsoluteDirectoryPath = baseDirectory.FullName,
				DirectoryPath = GetAbsolutePath(baseDirectory.FullName, _configManager.Configuration.BaseDirectory),
				FileName = fileInfo.Name.ToLower(),
				FilePath = GetAbsolutePath(fileInfo.FullName, _configManager.Configuration.BaseDirectory),
				AbsoluteFilePath = fileInfo.FullName,
				FileExtension = fileInfo.Extension
			};
		}

		private static string GetAbsolutePath(string fullPath, string baseDirectory)
		{
			var baseDirectoryIndex = fullPath.LastIndexOf(baseDirectory, StringComparison.Ordinal);
			if (baseDirectoryIndex == -1)
				throw new Exception("The base Directory was not found in the full path");

			var relativeStartIndex = baseDirectoryIndex + baseDirectory.Length;
			if (relativeStartIndex >= fullPath.Length)
				throw new Exception("The start index of the relative path was greater than the fullpath!");

			return @"\" + fullPath.Substring(relativeStartIndex);
		}

	}
}