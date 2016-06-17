using System.Collections.Generic;
using System.IO;

namespace MicroWeb.FileSystem.Interfaces
{
	public interface IFileSystemProvider
	{
		bool FileExists(string path);

		FileInfo GetFileInfo(string path);

		IEnumerable<FileInfo> GetFiles(
			string path, IEnumerable<string> searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly);

		IEnumerable<FileInfo> GetFiles(
			string path, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly);

		string[] GetFileNames(
			string path, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly);


		bool DirectoryExists(string path);

		DirectoryInfo GetDirectoryInfo(string path);

		IEnumerable<DirectoryInfo> GetDirectories(
			string path, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly);

		IEnumerable<DirectoryInfo> GetDirectories(
			string path, IEnumerable<string> searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly);

		string[] GetDirectoryNames(
			string path, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly);

	}
}
