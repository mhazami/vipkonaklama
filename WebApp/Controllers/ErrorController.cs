using System.Web.Mvc;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.AppCode.Security;
using Enums = Radyn.WebDesign.Definition.Enums;

namespace Radyn.WebApp.Controllers
{
    public class ErrorController : LocalizedController
    {
        
        
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult NotFound()
        {

            return View();
        }







    }
}
