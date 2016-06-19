using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MicroWeb.Routing.Models;

namespace MicroWeb.SelfHost
{
//	public class MicroWebListener : IMicroWebListener
////	public class MicroWebListener
//	{
//		private readonly IHttpListener _listener;
//
//		public MicroWebListener(IHttpListener listener)
//		{
//			_listener = listener;
//		}
//
//		public void Run(params string[] prefixes)
//		{
//			//When a request comes in, run the ListenerCallback
//			var result = _listener.BeginGetContext(ListenerCallback, prefixes);
//
//			//Wait for an incoming request!
//			result.AsyncWaitHandle.WaitOne();
//		}
//
//		public void StartServer(params string[] prefixes)
//		{
//			if (prefixes == null || prefixes.Length == 0)
//				throw new ArgumentException(
//					"Sorry... There were no prefixes passed to MicroWeb Server! Please pass at least one prefix.");
//
//			AddPrefixes(prefixes);
//
//			_listener.Start();
//		}
//
//		private void AddPrefixes(IEnumerable<string> prefixes)
//		{
//			foreach (var s in prefixes)
//				_listener.Prefixes.Add(s);
//		}
//
//		public void Stop()
//		{
//			throw new NotImplementedException();
//		}
//
//		private void ListenerCallback(IAsyncResult result)
//		{
//			var context = GetContext(result);
//			_listener.BeginGetContext(ListenerCallback, null);
//
//			HandleRequest(context);
//		}
//
//		private HttpListenerContext GetContext(IAsyncResult result)
//		{
//			//Gets the HttpListenerContext
//			return _listener.EndGetContext(result);
//		}
//
//		private void HandleRequest(HttpListenerContext ctx)
//		{
//			try
//			{
//				WriteResponse(ctx);
//			}
//			catch (Exception ex)
//			{
//				throw new Exception(
//					string.Format("Sorry... There has been an unexpected error!" +
//							"\n\nMessage: \n{0}" + "\n\nInner Exception: {1}" + "\n\nStack Trace: {2}",
//							ex.Message, ex.InnerException, ex.StackTrace));
//			}
//		}
//
//		private void WriteResponse(HttpListenerContext ctx)
//		{
//			var bytes = ResolveRouteToBytes(ctx);
//
//			ctx.Response.ContentLength64 = bytes.Length;
//			ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
//			ctx.Response.OutputStream.Close();
//		}
//
//		private byte[] ResolveRouteToBytes(HttpListenerContext ctx)
//		{
//			return Encoding.UTF8.GetBytes(
//				_microWeb.ResolveRoute(MapRequestInfo(ctx.Request)));
//		}
//
//		private static MicroWebRequest MapRequestInfo(HttpListenerRequest request)
//		{
//			return new MicroWebRequest
//			{
//				Headers = request.Headers,
//				HttpMethod = request.HttpMethod,
//				QueryStrings = request.QueryString,
//				RawUrl = request.RawUrl,
//				Url = request.Url
//			};
//		}
//
//	}
//
//	public interface IMicroWebListener
//	{
//		void Run(params string[] prefixes);
//		void StartServer(params string[] prefixes);
//		void Stop();
//	}
}
