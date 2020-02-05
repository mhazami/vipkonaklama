using System.Web.Mvc;
using Radyn.WebApp.AppCode.Base;


namespace Radyn.WebApp.Areas.WebDesign.Controllers
{
    public class MailController : WebDesignBaseController
    {
        public ActionResult SendMail(string mail, string subject, string body)
        {

            
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

    }
}