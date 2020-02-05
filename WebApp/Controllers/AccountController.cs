using System.Linq;
using System.Web.Mvc;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Controllers
{
    public class AccountController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || SessionParameters.UserOperation == null)
                return Redirect("/security/User/Login");
            if (SessionParameters.UserOperation.Count() == 1)
                return Redirect("/Security/User/Menu?oid=" + SessionParameters.UserOperation.First().Id);
            return View(SessionParameters.UserOperation);
        }

        public ActionResult AccessDeny()
        {

            

            return View();
        }
    }
}
