using System;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.ContentManager.Controllers
{
    public class ContainerController : WebDesignBaseController
    {

        public ActionResult GetDetail(Guid Id)
        {
            return PartialView("PVDetails", ContentManagerComponent.Instance.ContainerFacade.Get(Id));
        }
        public ActionResult GetModify(Guid? Id)
        {
            return PartialView("PVModify", Id.HasValue ? ContentManagerComponent.Instance.ContainerFacade.Get(Id) : new Container());
        }

        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ContentManagerComponent.Instance.ContainerFacade.Where(menu => menu.IsExternal == false);
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

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            var container = new Container();
            try
            {
                this.RadynTryUpdateModel(container, collection);
                container.CurrentUICultureName = collection["LanguageId"];
                if (ContentManagerComponent.Instance.ContainerFacade.Insert(container))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return Redirect("~/ContentManager/Container/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Redirect("~/ContentManager/Container/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Lanuages = CommonComponent.Instance.LanguageFacade.Where(x => x.Enabled);
                return View(container);
            }
        }

        [RadynAuthorize]
        [SourceCodeFile("ImageBrowser Controller", "~/Controllers/ImageBrowserController.cs")]
        public ActionResult Edit(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var container = ContentManagerComponent.Instance.ContainerFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(container, collection);
                container.CurrentUICultureName = collection["LanguageId"];
                if (ContentManagerComponent.Instance.ContainerFacade.Update(container))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return Redirect("~/ContentManager/Container/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Redirect("~/ContentManager/Container/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(container);
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
            var container =
                ContentManagerComponent.Instance.ContainerFacade.Get(Id);
            try
            {
                if (ContentManagerComponent.Instance.ContainerFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return Redirect("~/ContentManager/Container/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Redirect("~/ContentManager/Container/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(container);
            }
        }
    }
}