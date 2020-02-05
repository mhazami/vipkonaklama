using System;
using System.Linq;
using System.Web.Mvc;
using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.ContentManager.Controllers
{

    public class SectionController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ContentManagerComponent.Instance.SectionFacade.GetAll();
            return View(list);
        }
        [RadynAuthorize]
        public ActionResult ModifySection(FormCollection formCollection)
        {

            try
            {
                var id = formCollection["Sectionid"];
                var state = formCollection["SectionState"];
                var section = new Section();
                var parentorOwn = string.IsNullOrEmpty(id) ? null : ContentManagerComponent.Instance.SectionFacade.Get(id.ToInt());
                switch (state)
                {

                    case "Create":
                        {
                            this.RadynTryUpdateModel(section, formCollection);
                            if (parentorOwn != null)
                                section.ParentSectionId = parentorOwn.Id;
                            if (ContentManagerComponent.Instance.SectionFacade.Insert(section))
                            {
                                ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                            messageIcon: MessageIcon.Succeed);
                                return Content("true");
                            }
                            ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                        messageIcon: MessageIcon.Error);
                            return Content("false");
                        }
                    case "Edit":
                        {
                            if (parentorOwn == null)
                            {
                                ShowMessage(Resources.ContentManager.Not_Exist_To_Edit, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                            messageIcon: MessageIcon.Error);
                                return Content("false");
                            }
                            this.RadynTryUpdateModel(parentorOwn, formCollection);
                            parentorOwn.IsMenu = formCollection["TxtIsMenu"].ToBool();
                            if (parentorOwn.IsMenu.Equals(false)) parentorOwn.ParentSection = null;
                            if (ContentManagerComponent.Instance.SectionFacade.Update(parentorOwn))
                            {
                                ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                            messageIcon: MessageIcon.Succeed);
                                return Content("true");
                            }
                            ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" }, messageIcon: MessageIcon.Error);
                            return Content("false");
                        }
                    case "Delete":
                        {
                            if (parentorOwn == null)
                            {
                                ShowMessage(Resources.ContentManager.Not_Exist_To_delete, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, " Close_thisModal();" },
                                            messageIcon: MessageIcon.Error);
                                return Content("false");
                            }
                            var sectionchild =
                                ContentManagerComponent.Instance.SectionFacade.Any(
                                    section1 => section1.ParentSectionId == parentorOwn.Id);

                            if (sectionchild)
                            {
                                ShowMessage(Resources.ContentManager.This_Item_Not_able_to_Delete_Becase_Have_ChildNode,
                                            Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                            messageIcon: MessageIcon.Error);
                                return Content("false");
                            }
                            if (ContentManagerComponent.Instance.MenuFacade.Delete(parentorOwn.Id))
                            {
                                ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                            messageIcon: MessageIcon.Succeed);
                                return Content("true");
                            }
                            ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                        messageIcon: MessageIcon.Error);
                            return Content("false");
                        }
                }
                return Content("true");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }
        }
        [RadynAuthorize]
        public ActionResult PartialViewModifySection(string state, int id)
        {
            var section = new Section();
            switch (state)
            {

                case "Create":
                    var section1 = ContentManagerComponent.Instance.SectionFacade.Get(id);
                    section = new Section { ParentSectionId = section1.Id };
                    break;
                case "Edit":
                    section = ContentManagerComponent.Instance.SectionFacade.Get(id);
                    break;
                case "Delete":
                    section = ContentManagerComponent.Instance.SectionFacade.Get(id);
                    break;
            }
            return PartialView("PartialModifySection", section);

        }
        [RadynAuthorize]
        public ActionResult LookUpSection()
        {
            return View();
        }

        public ActionResult GetSectionTree()
        {

            var list = ContentManagerComponent.Instance.SectionFacade.GetAll().ToList();
            return PartialView("PartialViewSectionTree", list.Where(menu => !menu.ParentSectionId.HasValue).ToList());
        }
        [RadynAuthorize]
        public ActionResult Details(int Id)
        {
            return View(ContentManagerComponent.Instance.SectionFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new Section());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var section = new Section();

            try
            {
                this.RadynTryUpdateModel(section);
                section.IsMenu = collection["TxtIsMenu"].ToBool();
                if (ContentManagerComponent.Instance.SectionFacade.Insert(section))
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
                ShowExceptionMessage(exception);

                return View(section);
            }
        }


        [RadynAuthorize]
        public ActionResult Edit(int id)
        {

            return View(ContentManagerComponent.Instance.SectionFacade.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var section = ContentManagerComponent.Instance.SectionFacade.Get(id);

            try
            {
                this.RadynTryUpdateModel(section);
                section.IsMenu = collection["TxtIsMenu"].ToBool();
                if (!section.IsMenu) section.ParentSectionId = null;
                if (ContentManagerComponent.Instance.SectionFacade.Update(section))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(section);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(int id)
        {
            return View(ContentManagerComponent.Instance.SectionFacade.Get(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var section = ContentManagerComponent.Instance.SectionFacade.Get(id);

            try
            {
                if (ContentManagerComponent.Instance.SectionFacade.Delete(id))
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
                ShowExceptionMessage(exception);
                return View(section);
            }
        }
    }
}