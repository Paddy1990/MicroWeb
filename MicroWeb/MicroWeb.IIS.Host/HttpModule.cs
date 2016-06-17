using System;
using System.Web;
using MicroWeb.Routing.Models;

namespace MicroWeb.IIS.Host
{
    public class HttpModule : IHttpModule
    {
	    private readonly IMicroWeb _microWeb;

	    public HttpModule()
	    {
			_microWeb = new MicroWeb();
	    }

	    public void Init(HttpApplication context)
	    {
		    context.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
	    }

		public void Dispose() { }

	    private void OnPreRequestHandlerExecute(object sender, EventArgs eventArgs)
	    {
			var app = (HttpApplication)sender;
			var request = app.Context.Request; 

			app.Context.Response.Write(_microWeb.ResolveRoute(MapRequestInfo(request)));
	    }

		private MicroWebRequest MapRequestInfo(HttpRequest request)
		{
			return new MicroWebRequest
			{
				Headers = request.Headers,
				HttpMethod = request.HttpMethod,
//				Path = request.Path,
//				PathInfo = request.PathInfo,
//				PhysicalApplicationPath = request.PhysicalApplicationPath,
//				PhysicalPath = request.PhysicalPath,
				QueryStrings = request.QueryString,
				RawUrl = request.RawUrl,
//				RequestType = request.RequestType,
				Url = request.Url
			};
		}

    }
}
