using System;
using System.Collections.Generic;
using MicroWeb.Config.Interfaces;
using MicroWeb.Config.Models;
using MicroWeb.Constants;

namespace MicroWeb.Config
{
	public class ConfigManager : IConfigManager
	{
		public Configuration Configuration { get; private set; }

		public ConfigManager()
		{
			Configuration = GetConfiguration();
		}

		private Configuration GetConfiguration()
		{
			return new Configuration
			{
				BaseDirectory = string.Format("{0}Theme",AppDomain.CurrentDomain.BaseDirectory),
				FileTypes = new List<string> {
					FileTypes.Css, FileTypes.Html, FileTypes.JavaScript, 
					FileTypes.Jpeg, FileTypes.Jpg, FileTypes.Png
				},
				RouteMatchType = Match.Route
			};
		}


	}
}