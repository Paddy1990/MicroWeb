using System;
using System.Web;
using MicroWeb.StartUp;

namespace MicroWeb.IisHost
{
    public class HttpModule : IHttpModule
    {
	    public HttpModule()
	    {
			var startup = new Startup();
			startup.AppStart();
	    }

	    public void Init(HttpApplication context)
	    {
		    context.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
			context.
	    }

	    private void OnPreRequestHandlerExecute(object sender, EventArgs eventArgs)
	    {
			HttpApplication app = (HttpApplication)sender;
			HttpRequest request = app.Context.Request; 
			app.Context.Response.Write(new RouteHandler().HelloWorld());
	    }

	    public void Dispose() { }
    }
}
