using System;
using System.Web;
using System.Web.Routing;
using Radyn.Utility;

namespace Radyn.FileManager.Handler
{
    public class FileRouteHandler : IRouteHandler
    {

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var id = requestContext.RouteData.Values["id"];
           

            return (id != null && !string.IsNullOrEmpty(id.ToString())&& id.ToString().ToGuid() != Guid.Empty) ? new FileHandler(id.ToString().ToGuid()) : new FileHandler(Guid.Empty);
        }
    }
}
