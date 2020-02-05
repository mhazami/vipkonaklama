using System.Web.Mvc;
using Radyn.WebApp.Areas.PhoneBook.Tools;

namespace Radyn.WebApp.Areas.PhoneBook.Security.Filter
{
    public class PhoneBookHost : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
             AppExtention.GetPhoneBookConfig();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }

}