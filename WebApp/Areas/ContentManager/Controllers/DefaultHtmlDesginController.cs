using System;
using System.Web;
using System.Web.Mvc;
using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.ContentManager.Controllers
{
    public class DefaultHtmlDesginController : WebDesignBaseController
    {

        public ActionResult GetDetail(Guid Id)
        {
            var culture = SessionParameters.Culture;
            return PartialView("PVDetails", ContentManagerComponent.Instance.DefaultHtmlDesginFacade.GetLanuageContent(culture,Id));
        }
        public ActionResult GetModify(Guid? Id, string culture)
        {
            if (string.IsNullOrEmpty(culture)) culture = SessionParameters.Culture;
            return PartialView("PVModify", Id.HasValue ? ContentManagerComponent.Instance.DefaultHtmlDesginFacade.GetLanuageContent(culture, Id) : new DefaultHtmlDesgin());
        }
        
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ContentManagerComponent.Instance.DefaultHtmlDesginFacade.OrderBy(x=>x.Title);
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
            var DefaultHtmlDesgin = new DefaultHtmlDesgin();
            try
            {
                this.RadynTryUpdateModel(DefaultHtmlDesgin, collection);
                DefaultHtmlDesgin.CurrentUICultureName = collection["LanguageId"];
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                if (ContentManagerComponent.Instance.DefaultHtmlDesginFacade.Insert(DefaultHtmlDesgin, file))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                     return Redirect("~/ContentManager/DefaultHtmlDesgin/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                 return Redirect("~/ContentManager/DefaultHtmlDesgin/Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
               
                return View(DefaultHtmlDesgin);
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
            var DefaultHtmlDesgin = ContentManagerComponent.Instance.DefaultHtmlDesginFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(DefaultHtmlDesgin, collection);
                DefaultHtmlDesgin.CurrentUICultureName = collection["LanguageId"];
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                if (ContentManagerComponent.Instance.DefaultHtmlDesginFacade.Update(DefaultHtmlDesgin, file))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                     return Redirect("~/ContentManager/DefaultHtmlDesgin/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                 return Redirect("~/ContentManager/DefaultHtmlDesgin/Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(DefaultHtmlDesgin);
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
            var DefaultHtmlDesgin = ContentManagerComponent.Instance.DefaultHtmlDesginFacade.Get(Id);
            try
            {
                if (ContentManagerComponent.Instance.DefaultHtmlDesginFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                     return Redirect("~/ContentManager/DefaultHtmlDesgin/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                 return Redirect("~/ContentManager/DefaultHtmlDesgin/Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View();
            }
        }
    }
}