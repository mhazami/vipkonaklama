using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Radyn.Security;
using Radyn.Web.Mvc.Utility;

namespace Radyn.WebApp.AppCode.Security
{
    public class RadynAuthorize : AuthorizeAttribute, IActionFilter, IRadynActionInvoke
    {
        public RadynAuthorize()
        {
            this.DoAuthorize = true;
            this.AccessLevel = UserType.User;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            filterContext.RequestContext.HttpContext.Response.Redirect(Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + "/Security/User/Login?returnUrl=" + filterContext.RequestContext.HttpContext.Request.RawUrl);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return this.Autherized(httpContext);
        }

        public bool DoAuthorize { get; set; }

        public UserType AccessLevel { get; set; }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.Autherized(filterContext.HttpContext);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        private bool Autherized(HttpContextBase httpContext)
        {
            if (!DoAuthorize) return true;

            if (this.AccessLevel == UserType.Host && SessionParameters.UserType != UserType.Host)
                httpContext.Response.Redirect("~/Account/AccessDeny");

            switch (SessionParameters.UserType)
            {
                case UserType.None:
                    System.Web.Security.FormsAuthentication.SignOut();
                    break;
                case UserType.Host:
                    break;

                case UserType.User:

                    if (SessionParameters.User == null)
                    {
                        System.Web.Security.FormsAuthentication.SignOut();
                    }
                    else
                    {
                        if (httpContext == null || httpContext.Request == null || httpContext.Request.Url == null)
                            return false;
                        var routeData = RouteTable.Routes.GetRouteData(httpContext);
                        var url = "";

                        if (routeData != null)
                        {
                            var area = routeData.DataTokens["area"];
                            var controller = routeData.Values["controller"];
                            var action = routeData.Values["action"];
                            if (area != null)
                                url += "/" + area;
                            url += "/" + controller;
                            url += "/" + action;
                            url = url.ToLower();
                        }
                        else
                            url = httpContext.Request.Url.AbsolutePath.ToLower();

                        var hasAccess = SecurityComponent.Instance.UserFacade.AccessMenu(SessionParameters.User.Id, url);

                        if (hasAccess == null && url.EndsWith("/index"))
                        {
                            url = url.Substring(0, url.LastIndexOf("/index"));
                            hasAccess = SecurityComponent.Instance.UserFacade.AccessMenu(SessionParameters.User.Id, url);
                        }
                        if (!url.StartsWith("/account"))
                        {
                            if (hasAccess == null)
                                httpContext.Response.Redirect("~/Account/AccessDeny");
                            
                        }
                    }
                    break;
            }
            return base.AuthorizeCore(httpContext);
        }

        public bool IsAutherized(HttpContextBase HttpRequestBase)
        {
            if (this.Autherized(HttpRequestBase)) return true;
            HttpRequestBase.Response.Redirect(Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + "/Security/User/Login");
            return false;
        }

        public bool HasAccess(HttpContextBase HttpRequestBase, string url)
        {
            var hasAccess = AppCode.Filter.Autherized.HasAccess(url);
            if (hasAccess) return true;
            HttpRequestBase.Response.Redirect(Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + "/Account/AccessDeny");
            return false;
        }

        public bool OnRadynActionExecuting(HttpContextBase HttpRequestBase, string url)
        {
            return true;
        }
    }
}