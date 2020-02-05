using System.Collections.Generic;
using System.Web.Mvc;
using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.WebApp.AppCode.Base;


namespace Radyn.WebApp.Areas.ContentManager.Controllers
{
    public class UIPanelController : WebDesignBaseController
    {


        public ActionResult GetMarquee()
        {
            return PartialView("PVWebDesignMarquee ");
        }

      

        public ActionResult GetWebDesignFooter()
        {
            ViewBag.Id = this.WebSite.Configuration.FooterId;
            return PartialView("PVWebDesignFooter", this.WebSite.Configuration);
        }
        public ActionResult GetWebDesignHeader()
        {
            ViewBag.Id = this.WebSite.Configuration.HeaderId;
            return PartialView("PVWebDesignHeader", this.WebSite.Configuration);
        }
        public ActionResult GetWebDesignMenu()
        {
            var tree = ContentManagerComponent.Instance.WebDesignMenuFacade.MenuTree(this.WebSite.Id);
            ViewBag.MenuHtml = this.WebSite.Configuration.MenuHtml;
            return PartialView("PVWebDesignMenuShow", tree);
        }







        public ActionResult GetMenu(List<Radyn.ContentManager.DataStructure.Menu> menus,MenuHtml MenuHtml=null)
        {
           
            ViewBag.MenuHtml = MenuHtml;
            return PartialView("PVMenuShow", menus);
        }


    }
}
