using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Radyn.WebApp.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/Security/User/Login");
        }
    }
}