namespace MicroWeb.FileSystem.Interfaces
{
	public interface IJsonReader
	{
		TModel Deserialise<TModel>(string path);
	}
}