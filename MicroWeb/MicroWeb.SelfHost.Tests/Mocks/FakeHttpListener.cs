using System;
using System.Net;

namespace MicroWeb.SelfHost.Tests.Mocks
{
	public class FakeHttpListener : IHttpListener
	{
		public HttpListenerPrefixCollection Prefixes { get; private set; }

		public string Realm { get; set; }

		public HttpListenerTimeoutManager TimeoutManager { get; private set; }

		public void Abort()
		{
			throw new NotImplementedException();
		}

		public IAsyncResult BeginGetContext(AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public void Close()
		{
			throw new NotImplementedException();
		}

		public HttpListenerContext EndGetContext(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		public bool IsListening()
		{
			throw new NotImplementedException();
		}

		public void Start()
		{
			throw new NotImplementedException();
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}

	}
}
