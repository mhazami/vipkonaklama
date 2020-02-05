using System;
using System.Web.Mvc;
using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.AppCode.Security;


namespace Radyn.WebApp.Areas.ContentManager.Controllers
{
    public class WebDesignPagesController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ContentManagerComponent.Instance.WebDesignPagesFacade.GetByWebSiteId(this.WebSite.Id);
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [RadynAuthorize]
        [SourceCodeFile("ImageBrowser Controller", "~/Controllers/ImageBrowserController.cs")]
        public ActionResult Create()
        {
            return View(new Container());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            var container = new Pages();
            try
            {
                this.RadynTryUpdateModel(container, collection);
                this.RadynTryUpdateModel(container.HtmlDesgin, collection);
                container.CurrentUICultureName = collection["LanguageId"];
                container.HtmlDesgin.CurrentUICultureName = collection["LanguageId"];
                if (ContentManagerComponent.Instance.WebDesignPagesFacade.Insert(this.WebSite.Id, container, container.HtmlDesgin))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(container);
            }
        }

        [RadynAuthorize]
        [SourceCodeFile("ImageBrowser Controller", "~/Controllers/ImageBrowserController.cs")]
        public ActionResult Edit(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int Id, FormCollection collection)
        {
            var container = ContentManagerComponent.Instance.PagesFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(container, collection);
                this.RadynTryUpdateModel(container.HtmlDesgin, collection);
              
                container.CurrentUICultureName = collection["LanguageId"];
                container.HtmlDesgin.CurrentUICultureName = collection["LanguageId"];
                if (ContentManagerComponent.Instance.PagesFacade.Update(container, container.HtmlDesgin))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.Id = Id;
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int Id, FormCollection collection)
        {
            try
            {
                if (ContentManagerComponent.Instance.WebDesignPagesFacade.Delete(this.WebSite.Id, Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.Id = Id;
                return View();
            }
        }
    }
}