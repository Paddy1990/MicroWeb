using System.IO;
using MicroWeb.FileSystem.Interfaces;

namespace MicroWeb.FileSystem
{
	public class FileReader : IFileReader
	{
		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

	}
}