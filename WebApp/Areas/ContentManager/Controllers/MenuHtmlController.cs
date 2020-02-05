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
    public class MenuHtmlController : WebDesignBaseController
    {

        public ActionResult GetDetail(Guid Id)
        {
            var culture = SessionParameters.Culture;
            return PartialView("PVDetails", ContentManagerComponent.Instance.MenuHtmlFacade.GetLanuageContent(culture,Id));
        }
        public ActionResult GetModify(Guid? Id, string culture)
        {
            if (string.IsNullOrEmpty(culture)) culture = SessionParameters.Culture;
            return PartialView("PVModify", Id.HasValue ? ContentManagerComponent.Instance.MenuHtmlFacade.GetLanuageContent(culture, Id) : new MenuHtml());
        }
        
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ContentManagerComponent.Instance.MenuHtmlFacade.Where(menu => menu.IsExternal == false);
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
            var MenuHtml = new MenuHtml();
            try
            {
                this.RadynTryUpdateModel(MenuHtml, collection);
                MenuHtml.CurrentUICultureName = collection["LanguageId"];
              
                if (ContentManagerComponent.Instance.MenuHtmlFacade.Insert(MenuHtml))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                     return Redirect("~/ContentManager/MenuHtml/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                 return Redirect("~/ContentManager/MenuHtml/Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                return View(MenuHtml);
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
            var MenuHtml = ContentManagerComponent.Instance.MenuHtmlFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(MenuHtml, collection);
                MenuHtml.CurrentUICultureName = collection["LanguageId"];
                if (ContentManagerComponent.Instance.MenuHtmlFacade.Update(MenuHtml))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                     return Redirect("~/ContentManager/MenuHtml/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                 return Redirect("~/ContentManager/MenuHtml/Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(MenuHtml);
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
            var MenuHtml = ContentManagerComponent.Instance.MenuHtmlFacade.Get(Id);
            try
            {
                if (ContentManagerComponent.Instance.MenuHtmlFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                     return Redirect("~/ContentManager/MenuHtml/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                 return Redirect("~/ContentManager/MenuHtml/Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(MenuHtml);
            }
        }
    }
}