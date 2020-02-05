using System.Web.Mvc;

namespace Radyn.WebApp.AppCode.Filter
{
    public class RadynExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var message = filterContext.Exception.Message;
            if (filterContext.Exception.InnerException != null)
                message += filterContext.Exception.InnerException;

            Log.Save(message, LogType.ApplicationError, filterContext.RequestContext.HttpContext.Request.RawUrl, filterContext.Exception.StackTrace);

            filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Error?msg=" + message + "&referUrl=" + filterContext.RequestContext.HttpContext.Request.RawUrl);
        }
    }
}