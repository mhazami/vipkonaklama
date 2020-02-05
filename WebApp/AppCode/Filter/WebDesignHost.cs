using System.Web.Mvc;
using Radyn.WebApp.Areas.WebDesign.Tools;

namespace Radyn.WebApp.AppCode.Filter
{
    public class WebDesignHost : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
             AppExtention.GetWebSite();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }

}