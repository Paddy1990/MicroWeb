using System;
using System.Net;

namespace MicroWeb.SelfHost
{
	public interface IHttpListener
	{
		HttpListenerPrefixCollection Prefixes { get; }
		string Realm { get; set; }
		HttpListenerTimeoutManager TimeoutManager { get; }
		void Abort();
		IAsyncResult BeginGetContext(AsyncCallback callback, Object state);
		void Close();
		HttpListenerContext EndGetContext(IAsyncResult asyncResult);
		bool IsListening();
		void Start();
		void Stop();
	}
}