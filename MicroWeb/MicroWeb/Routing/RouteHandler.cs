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

		private IDictionary<string, MicroWebRoute> Routes { get; set; }
		
		public RouteHandler(IConfigManager configManager, IFileSystemProvider fileSystemProvider)
		{
			_configManager = configManager;
			_fileSystemProvider = fileSystemProvider;

			Routes = new Dictionary<string, MicroWebRoute>();
		}

		public IDictionary<string, MicroWebRoute> ConfigureRoutes()
		{
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
				Routes[route.Url] = route;
			}

			var subDirectories = _fileSystemProvider.GetDirectories(baseDirectory, "*", SearchOption.AllDirectories);
			//Recersively Call the ConfigureRoute(string path) method to populate all routes.
			foreach (var directoryInfo in subDirectories)
				ConfigureRoute(directoryInfo.FullName);
		}

		public byte[] ResolveRoute(MicroWebRequest request, IDictionary<string, MicroWebRoute> routes)
		{
            MicroWebRoute route;
			//If it's a request for a route. Infer the html file from the route
			//Thought the best way to do this was check if the request is for a file, 
			//rather than check if it's a route that has been requested. E.g. /contact rather than /source.js.
			if (request.IsFile())
			{
				if (!routes.ContainsKey(request.AbsolutePath))
					throw new Exception("Can't find route...");

				route = routes[request.AbsolutePath];
				return ParseFile(route);
			}

		    route = routes[request.AbsolutePath + Match.FileName];

			return ParseFile(route);
		}

	    private byte[] ParseFile(MicroWebRoute route)
	    {
	        throw new NotImplementedException();
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