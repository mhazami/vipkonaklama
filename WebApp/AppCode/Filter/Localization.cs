using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.AppCode.Filter
{
    public class Localization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (string.IsNullOrEmpty(SessionParameters.Culture))
            {
                var currentRequest = HttpContext.Current.Request;
                var ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == null || ipAddress.ToLower() == "unknown")
                    ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];
                if (ipAddress == "::1") ipAddress = "127.0.0.1";
                var defaultLang =CommonComponent.Instance.LanguageFacade.Select(x=>x.Id,c => c.IsDefault);
                if (defaultLang != null && defaultLang.Count > 0 && !string.IsNullOrEmpty(defaultLang[0]))
                    SessionParameters.Culture = defaultLang[0];
            }
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(SessionParameters.Culture);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(SessionParameters.Culture);
            base.OnActionExecuting(filterContext);
        }
    }
}