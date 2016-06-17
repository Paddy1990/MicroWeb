using System;
using System.Net;
using System.Text;
using MicroWeb.Routing.Models;

namespace MicroWeb.SelfHost
{
	public class MicroWebServer : IMicroWebServer
	{
		private readonly HttpListener _listener;
		private readonly IMicroWeb _microWeb;
		private readonly string[] _prefixes;

		public MicroWebServer(params string[] prefixes)
		{
			if (!HttpListener.IsSupported)
				throw new NotSupportedException(
					"Sorry... The operating system isn't supported! Minimum requirements are 'Windows XP SP2' or 'Server 2003'.");

			_microWeb = new MicroWeb();

			_listener = new HttpListener();
			_prefixes = prefixes;

			StartServer();
		}

		public void Run()
		{
			//When a request comes in, run the ListenerCallback
			var result = _listener.BeginGetContext(ListenerCallback, null);

			//Wait for an incoming request!
			result.AsyncWaitHandle.WaitOne();
		}

		public void Stop()
		{
			_listener.Stop();
			_listener.Close();
		}

		private void StartServer()
		{
			if (_prefixes == null || _prefixes.Length == 0)
				throw new ArgumentException(
					"Sorry... There were no prefixes passed to MicroWeb Server! Please pass at least one prefix.");

			AddPrefixes();

			_listener.Start();
		}

		private void ListenerCallback(IAsyncResult result)
		{
			var context = GetContext(result);
			_listener.BeginGetContext(ListenerCallback, null);

			HandleRequest(context);

		}

		private HttpListenerContext GetContext(IAsyncResult result)
		{
			//Gets the HttpListenerContext
			return _listener.EndGetContext(result);
		}

		private void AddPrefixes()
		{
			foreach (string s in _prefixes)
				_listener.Prefixes.Add(s);
		}

		private void HandleRequest(HttpListenerContext ctx)
		{
			try
			{
				WriteResponse(ctx);
			}
			catch (Exception ex)
			{
				throw new Exception(
					string.Format("Sorry... There has been an unexpected error!" +
							"\n\nMessage: \n{0}" + "\n\nInner Exception: {1}" + "\n\nStack Trace: {2}",
							ex.Message, ex.InnerException, ex.StackTrace));
			}
		}

		private void WriteResponse(HttpListenerContext ctx)
		{
			var bytes = ResolveRouteToBytes(ctx);

			ctx.Response.ContentLength64 = bytes.Length;
			ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
			ctx.Response.OutputStream.Close();
		}

		private byte[] ResolveRouteToBytes(HttpListenerContext ctx)
		{
			return Encoding.UTF8.GetBytes(
				_microWeb.ResolveRoute(MapRequestInfo(ctx.Request)));
		}

		private static MicroWebRequest MapRequestInfo(HttpListenerRequest request)
		{
			return new MicroWebRequest
			{
				Headers = request.Headers,
				HttpMethod = request.HttpMethod,
				QueryStrings = request.QueryString,
				RawUrl = request.RawUrl,
				Url = request.Url
			};
		}

	}
}