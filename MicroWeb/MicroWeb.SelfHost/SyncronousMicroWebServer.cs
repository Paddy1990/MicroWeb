using System;
using System.Net;
using System.Text;
using System.Threading;
using MicroWeb.Routing.Models;

namespace MicroWeb.SelfHost
{
	public class SyncronousMicroWebServer : IMicroWebServer
	{
		private readonly HttpListener _listener;
		private readonly IMicroWeb _microWeb;
		private readonly string[] _prefixes;

		public SyncronousMicroWebServer(params string[] prefixes)
		{
			if (!HttpListener.IsSupported)
				ThrowNotSupportedException();

			_microWeb = new MicroWeb();

			_listener = new HttpListener();
			_prefixes = prefixes;

			StartListener();
		}

		public void Run()
		{
			//Not sure why I've got two threadPools inside each other?
			ThreadPool.QueueUserWorkItem((o) =>
			{
				Console.WriteLine("MicroWeb Server running...");
				while (_listener.IsListening)
					QueueRequests();
			});
		}

		public void Stop()
		{
			_listener.Stop();
			_listener.Close();
		}

		private void QueueRequests()
		{
			ThreadPool.QueueUserWorkItem(c =>
			{
				var ctx = c as HttpListenerContext;
				if (ctx == null)
					ThrowNullReferenceException();

				HandleRequest(ctx);

				ctx.Response.OutputStream.Close();

			}, _listener.GetContext());
		}

		private void StartListener()
		{
			if (_prefixes == null || _prefixes.Length == 0)
				ThrowArgumentException();

			AddPrefixes();

			_listener.Start();
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
				ThrowGeneralException(ex);
			}
		}

		private void WriteResponse(HttpListenerContext ctx)
		{
			var bytes = Encoding.UTF8.GetBytes(
				_microWeb.ResolveRoute(MapRequestInfo(ctx.Request)));

			ctx.Response.ContentLength64 = bytes.Length;
			ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
		}

		private static void ThrowGeneralException(Exception ex)
		{
			throw new Exception(
				string.Format("Sorry... There has been an unexpected error!" +
							  "\n\nMessage: \n{0}" + "\n\nInner Exception: {1}" + "\n\nStack Trace: {2}",
							  ex.Message, ex.InnerException, ex.StackTrace));
		}

		private static void ThrowNotSupportedException()
		{
			throw new NotSupportedException(
				"Sorry... The operating system isn't supported! Minimum requirements are 'Windows XP SP2' or 'Server 2003'.");
		}

		private void ThrowArgumentException()
		{
			throw new ArgumentException(
				"Sorry... There were no prefixes passed to MicroWeb Server! Please pass at least one prefix.");
		}

		private static void ThrowNullReferenceException()
		{
			throw new NullReferenceException(
				"Sorry... There has been an issue with the request! The HttpListenerContext is null;");
		}

		private static MicroWebRequest MapRequestInfo(HttpListenerRequest request)
		{
			return new MicroWebRequest
			{
				Headers = request.Headers,
				HttpMethod = request.HttpMethod,
				QueryStrings = request.QueryString,
				AbsolutePath = request.RawUrl,
				Uri = request.Url
			};
		}

	}
}
