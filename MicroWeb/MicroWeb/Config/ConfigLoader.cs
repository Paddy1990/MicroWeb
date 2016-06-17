using System;
using MicroWeb.Config.Interfaces;
using MicroWeb.Config.Models;
using MicroWeb.FileSystem.Interfaces;

namespace MicroWeb.Config
{
	public class ConfigLoader : IConfigLoader
	{
		private readonly IJsonReader _jsonReader;

		public ConfigLoader(IJsonReader jsonReader)
		{
			_jsonReader = jsonReader;
		}

		public Configuration Load()
		{
			var baseConfigLocation = string.Format(@"{0}\config.json", AppDomain.CurrentDomain.BaseDirectory);
			return _jsonReader.Deserialise<Configuration>(baseConfigLocation);
		}
	}
}
