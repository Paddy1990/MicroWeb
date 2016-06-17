using System;
using System.Net;

namespace MicroWeb.SelfHost
{
	public class MicroWebHttpListener : IHttpListener
	{
		private readonly HttpListener _httpListener;

		public HttpListenerPrefixCollection Prefixes
		{
			get { return _httpListener.Prefixes; }
		}

		public string Realm
		{
			get { return _httpListener.Realm; }
			set { _httpListener.Realm = value; }
		}

		public HttpListenerTimeoutManager TimeoutManager 
		{
			get { return _httpListener.TimeoutManager; }
		}

		public MicroWebHttpListener()
		{
			_httpListener = new HttpListener();
		}

		public void Abort()
		{
			_httpListener.Abort();
		}

		public IAsyncResult BeginGetContext(AsyncCallback callback, Object state)
		{
			return _httpListener.BeginGetContext(callback, state);
		}

		public void Close()
		{
			_httpListener.Close();
		}

		public HttpListenerContext EndGetContext(IAsyncResult asyncResult)
		{
			return _httpListener.EndGetContext(asyncResult);
		}

		public bool IsListening()
		{
			return _httpListener.IsListening;
		}

		public void Start()
		{
			_httpListener.Start();
		}

		public void Stop()
		{
			_httpListener.Stop();
		}
	}
}
