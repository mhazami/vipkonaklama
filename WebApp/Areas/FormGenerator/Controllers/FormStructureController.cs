using System;
using System.Web.Mvc;
using Radyn.ContentManager;
using Radyn.FormGenerator;
using Radyn.FormGenerator.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.FormGenerator.Controllers
{
    public class FormStructureController : WebDesignBaseController
    {


        public ActionResult FormStructureDetails(Guid id)
        {
            var obj = FormGeneratorComponent.Instance.FormStructureFacade.Get(id);
            return PartialView("PVDetails", obj);
        }

        public ActionResult FormStructureModify(Guid? Id, string menuurl)
        {
            var formStructure = Id.HasValue ? FormGeneratorComponent.Instance.FormStructureFacade.Get(Id) : new FormStructure();
            ViewBag.MenuUrl = menuurl;
            return PartialView("PVModify", formStructure);
        }

        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = FormGeneratorComponent.Instance.FormStructureFacade.Where(x => x.IsExternal == false);
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult GetFormStructure(Guid? Id)
        {
            var form = Id.HasValue ? FormGeneratorComponent.Instance.FormStructureFacade.Get(Id) : new FormStructure();
            return PartialView("PartialViewFormStructure", form);
        }
        public ActionResult ModifyFormStructure(FormCollection collection)
        {

            try
            {
                if (collection["formId"].ToGuid() == Guid.Empty)
                {
                    var structure = new FormStructure();
                    this.RadynTryUpdateModel(structure);
                    if (FormGeneratorComponent.Instance.FormStructureFacade.Insert(structure))
                    {
                        ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                        return Content("true");
                    }
                    ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                    return Content("false");
                }
                var formStructure = FormGeneratorComponent.Instance.FormStructureFacade.Get(collection["formId"].ToGuid());
                this.RadynTryUpdateModel(formStructure);
                if (FormGeneratorComponent.Instance.FormStructureFacade.Update(formStructure))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return Content("true");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }
        }
        [RadynAuthorize]
        public ActionResult Create()
        {
            TempData["Containers"] = new SelectList(ContentManagerComponent.Instance.ContainerFacade.SelectKeyValuePair(x => x.Id, x => x.Title, x => x.IsExternal), "Key", "Value");
            return View(new FormStructure());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var formStructure = new FormStructure();
            try
            {
                this.RadynTryUpdateModel(formStructure);
                if (FormGeneratorComponent.Instance.FormStructureFacade.Insert(formStructure))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Edit", new { Id = formStructure.Id });
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                TempData["Containers"] = new SelectList(ContentManagerComponent.Instance.ContainerFacade.SelectKeyValuePair(x => x.Id, x => x.Title, x => x.IsExternal), "Key", "Value");
                return View(formStructure);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            TempData["Containers"] = new SelectList(ContentManagerComponent.Instance.ContainerFacade.SelectKeyValuePair(x => x.Id, x => x.Title, x => x.IsExternal), "Key", "Value");
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var formStructure = FormGeneratorComponent.Instance.FormStructureFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(formStructure);
                if (FormGeneratorComponent.Instance.FormStructureFacade.Update(formStructure))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                TempData["Containers"] = new SelectList(ContentManagerComponent.Instance.ContainerFacade.SelectKeyValuePair(x => x.Id, x => x.Title, x => x.IsExternal), "Key", "Value");
                ViewBag.Id = Id;
                return View(formStructure);
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
            var formStructure = FormGeneratorComponent.Instance.FormStructureFacade.Get(Id);
            try
            {
                if (FormGeneratorComponent.Instance.FormStructureFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(formStructure);
            }
        }



    }
}
