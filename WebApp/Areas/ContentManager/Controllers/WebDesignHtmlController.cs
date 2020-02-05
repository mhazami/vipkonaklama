using System;
using System.Web.Mvc;
using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.ContentManager.Controllers
{
    public class WebDesignHtmlController : WebDesignBaseController
    {

        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ContentManagerComponent.Instance.WebDesignHtmlFacade.Select(x=>x.HtmlDesgin, x => x.WebId == this.WebSite.Id && x.HtmlDesgin.IsExternal);
            return View(list);
        }
     

        
        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [RadynAuthorize]
        [SourceCodeFile("ImageBrowser Controller", "~/Controllers/ImageBrowserController.cs")]
        public ActionResult Create()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var htmlDesgin = new HtmlDesgin();
            try
            {
                this.RadynTryUpdateModel(htmlDesgin, collection);
                htmlDesgin.CurrentUICultureName = collection["LanguageId"];
                if (ContentManagerComponent.Instance.WebDesignHtmlFacade.Insert(this.WebSite.Id, htmlDesgin))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return View(htmlDesgin);
            }
        }

        [RadynAuthorize]
        [SourceCodeFile("ImageBrowser Controller", "~/Controllers/ImageBrowserController.cs")]
        public ActionResult Edit(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var htmlDesgin = ContentManagerComponent.Instance.HtmlDesginFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(htmlDesgin, collection);
                htmlDesgin.CurrentUICultureName = collection["LanguageId"];
                if (ContentManagerComponent.Instance.HtmlDesginFacade.Update(htmlDesgin))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return View(htmlDesgin);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {

            try
            {
                if (ContentManagerComponent.Instance.WebDesignHtmlFacade.Delete(this.WebSite.Id, Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                ViewBag.Id = Id;
                return View();
            }
        }
    }
}