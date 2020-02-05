using System.Web;
using System.Web.Mvc;
using Radyn.Web.Mvc.Utility;

namespace Radyn.WebApp.AppCode.Security
{
    public class WebDesignUserAuthorize : AuthorizeAttribute, IRadynActionInvoke
    {
       
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.RequestContext.HttpContext.Response.Redirect(Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + "/WebDesign/UserPanel/Login?returnUrl=" + filterContext.RequestContext.HttpContext.Request.RawUrl);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (SessionParameters.WebDesignUser == null)return false;
            return SessionParameters.WebDesignUser.WebId == SessionParameters.CurrentWebSite.Id;
        }
        public bool IsAutherized(HttpContextBase HttpRequestBase)
        {
            if (SessionParameters.WebDesignUser != null)
                return true;
            HttpRequestBase.Response.Redirect(Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + "/WebDesign/UserPanel/Login");
            return false;
        }

        public bool HasAccess(HttpContextBase HttpRequestBase, string url)
        {
            return true;
        }

        public bool OnRadynActionExecuting(HttpContextBase HttpRequestBase, string url)
        {
            return true;
        }
    }
}