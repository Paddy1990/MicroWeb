namespace MicroWeb.Routing.Models
{
	public class MicroWebRoute
	{
		public string DirectoryName { get; set; }
		public string DirectoryPath { get; set; }
		public string AbsoluteDirectoryPath { get; set; }

		public string FileName { get; set; }
		public string FilePath { get; set; }
		public string AbsoluteFilePath { get; set; }

		public string FileExtension { get; set; }

		public string Url { get; set; }
	}
}
