using System;
using System.Web.Mvc;
using Radyn.ContentManager;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.Areas.ContentManager.Tools;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.WebDesign.Controllers
{
    public class UIPanelController : WebDesignBaseController
    {












        public ActionResult GetAbout()
        {
            return PartialView("PVAbout", null);
        }
        public ActionResult Index(string culture)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                SessionParameters.Culture = culture;
                return RedirectToAction("Index");
            }
            return View(this.WebSite);
        }
        public ActionResult WebDesignNewsView(Int32 Id)
        {
            return Redirect("/News/UIPanel/NewsView?Id=" + Id);

        }
      


        public ActionResult GetIndex()
        {

            if (this.WebSite.Configuration.DefaultHTMLID == null) return PartialView("PVIndex");
            var htmlDesign = ContentManagerComponent.Instance.WebDesignHtmlFacade.SelectFirstOrDefault(c => c.HtmlDesgin,
                 c => c.HtmlDesginId == this.WebSite.Configuration.DefaultHTMLID);

            ViewBag.Html = this.GetHtml(htmlDesign, this.WebSite.Title, DefaultContrainerId: this.WebSite.Configuration.DefaultContainerID);
            return PartialView("PVIndex", this.WebSite);
        }



    }

}
