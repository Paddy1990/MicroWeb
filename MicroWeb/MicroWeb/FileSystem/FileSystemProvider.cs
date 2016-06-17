using System.Collections.Generic;
using System.IO;
using System.Linq;
using MicroWeb.FileSystem.Interfaces;

namespace MicroWeb.FileSystem
{
	public class FileSystemProvider : IFileSystemProvider
	{
		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		public FileInfo GetFileInfo(string path)
		{
			if (!FileExists(path))
				throw new FileNotFoundException("Sorry... The file spicified can't be found!");

			return new FileInfo(path);
		}

		public IEnumerable<FileInfo> GetFiles(string path, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			if (!DirectoryExists(path))
				throw new DirectoryNotFoundException("Sorry... The directory could not be found!");

			return new DirectoryInfo(path).EnumerateFiles(searchPattern, searchOption);
		}

		public IEnumerable<FileInfo> GetFiles(string path, IEnumerable<string> searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			if (!DirectoryExists(path))
				throw new DirectoryNotFoundException("Sorry... The directory could not be found!");

			return new DirectoryInfo(path).EnumerateFiles("*", searchOption)
				.Where(x => searchPatterns.Any(y => y == x.Extension));
		}

		public string[] GetFileNames(string path, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			if (!DirectoryExists(path))
				throw new DirectoryNotFoundException("Sorry... The directory could not be found!");

			return Directory.GetFiles(path, searchPattern, searchOption);
		}


		public bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		public DirectoryInfo GetDirectoryInfo(string path)
		{
			if (!DirectoryExists(path))
				throw new DirectoryNotFoundException("Sorry... The directory could not be found!");

			return new DirectoryInfo(path);
		}

		public IEnumerable<DirectoryInfo> GetDirectories(
			string path, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			return GetDirectoryInfo(path).EnumerateDirectories(searchPattern, searchOption);
		}

		public IEnumerable<DirectoryInfo> GetDirectories(
			string path, IEnumerable<string> searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			return GetDirectoryInfo(path).EnumerateDirectories("*", searchOption)
				.Where(x => searchPatterns.Any(y => y == x.Name));
		}

		public string[] GetDirectoryNames(
			string path, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			if (!DirectoryExists(path))
				throw new DirectoryNotFoundException("Sorry... The directory could not be found!");

			return Directory.GetDirectories(path, searchPattern, searchOption);
		}

    }
}
