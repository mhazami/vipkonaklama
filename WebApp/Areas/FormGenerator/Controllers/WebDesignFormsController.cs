using System;
using System.Web.Mvc;
using Radyn.ContentManager;
using Radyn.FormGenerator;
using Radyn.FormGenerator.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.Areas.WebDesign.Tools;

namespace Radyn.WebApp.Areas.FormGenerator.Controllers
{
    public class WebDesignFormsController : WebDesignBaseController<WebDesignForms>
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = FormGeneratorComponent.Instance.WebDesignFormsFacade.Select(forms => forms.WebSiteForm, x=>x.WebId == this.WebSite.Id);
            return View(list);
        }
        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [RadynAuthorize]
        public ActionResult Create()
        {
            TempData["Containers"] = new SelectList(ContentManagerComponent.Instance.WebDesignContainerFacade.GetByWebSiteId(this.WebSite.Id), "Id", "Title");
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            var formStructure = new FormStructure();
            try
            {
                this.RadynTryUpdateModel(formStructure, collection);
                if (FormGeneratorComponent.Instance.WebDesignFormsFacade.Insert(this.WebSite.Id, formStructure))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Edit", new { Id = formStructure.Id });
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                TempData["Containers"] =
                new SelectList(
                    ContentManagerComponent.Instance.WebDesignContainerFacade.GetByWebSiteId(this.WebSite.Id), "Id", "Title");
                return View();
            }
        }


        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            PrepareViewBags(new WebDesignForms(){WebId =this.WebSite.Id,FormId = Id},PageMode.Edit );
            return View();
        }

        public override void PrepareViewBags(WebDesignForms Model, PageMode pageMode)
        {
            base.PrepareViewBags(Model, pageMode);
            ViewBag.IsForUser = FormGeneratorComponent.Instance.WebDesignUserFormsFacade.Get(this.WebSite.Id, Model.FormId) !=
                                null;
            var firstOrDefault =
                FormGeneratorComponent.Instance.FormAssigmentFacade.FirstOrDefault(x => x.FormStructureId == Model.FormId);
            ViewBag.Datas = new SelectList(AppExtention.GetFormList(), "Key", "Value",
                firstOrDefault != null ? firstOrDefault.Url : "");
            ViewBag.Id = Model.FormId;
            TempData["Containers"] =
                new SelectList(
                    ContentManagerComponent.Instance.WebDesignContainerFacade.GetByWebSiteId(this.WebSite.Id), "Id", "Title");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var formStructure = FormGeneratorComponent.Instance.FormStructureFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(formStructure, collection);
                var forUser = collection["ForUser"].ToBool();
                var url = collection["Selected"];
                this.RadynTryUpdateModel(formStructure, collection);
                if (forUser)
                    url = null;
                if (FormGeneratorComponent.Instance.WebDesignFormsFacade.UpdateAndAssgine(this.WebSite.Id,formStructure,url,forUser))
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
                PrepareViewBags(new WebDesignForms() { WebId = this.WebSite.Id, FormId = Id }, PageMode.Edit);
                return View();
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
                if (FormGeneratorComponent.Instance.WebDesignFormsFacade.Delete(this.WebSite.Id, Id))
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