using System.IO;
using MicroWeb.FileSystem.Interfaces;
using Newtonsoft.Json;

namespace MicroWeb.FileSystem
{
	public class JsonReader : IJsonReader
	{
		public TModel Deserialise<TModel>(string path)
		{
			using (var sr = new StreamReader(path))
			{
				var file = sr.ReadToEnd();
				return JsonConvert.DeserializeObject<TModel>(file);
			}
		}
	}
}
