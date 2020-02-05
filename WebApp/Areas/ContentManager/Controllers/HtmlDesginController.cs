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
    public class HtmlDesginController : WebDesignBaseController
    {

        public ActionResult GetDetail(Guid Id)
        {
            var culture = SessionParameters.Culture;
            return PartialView("PVDetails", ContentManagerComponent.Instance.HtmlDesginFacade.GetLanuageContent(culture,Id));
        }
        public ActionResult GetModify(Guid? Id, string culture)
        {
            if (string.IsNullOrEmpty(culture)) culture = SessionParameters.Culture;
            return PartialView("PVModify", Id.HasValue ? ContentManagerComponent.Instance.HtmlDesginFacade.GetLanuageContent(culture, Id) : new HtmlDesgin());
        }
        
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ContentManagerComponent.Instance.HtmlDesginFacade.Where(menu => menu.IsExternal == false);
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
                if (ContentManagerComponent.Instance.HtmlDesginFacade.Insert(htmlDesgin))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                     return Redirect("~/ContentManager/HtmlDesgin/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                 return Redirect("~/ContentManager/HtmlDesgin/Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
               
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
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                     return Redirect("~/ContentManager/HtmlDesgin/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                 return Redirect("~/ContentManager/HtmlDesgin/Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                ViewBag.Id = Id;
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
            var htmlDesgin = ContentManagerComponent.Instance.HtmlDesginFacade.Get(Id);
            try
            {
                if (ContentManagerComponent.Instance.HtmlDesginFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                     return Redirect("~/ContentManager/HtmlDesgin/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                 return Redirect("~/ContentManager/HtmlDesgin/Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(htmlDesgin);
            }
        }
    }
}