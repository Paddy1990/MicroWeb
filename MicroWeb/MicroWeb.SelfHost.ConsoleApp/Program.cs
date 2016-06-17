using System;

namespace MicroWeb.SelfHost.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var ws = new MicroWebServer("http://localhost:8080/");
			ws.Run();

			Console.ReadKey();
			ws.Stop();
		}
	}
}
