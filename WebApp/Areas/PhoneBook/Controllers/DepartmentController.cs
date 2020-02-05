using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Radyn.PhoneBook;
using Radyn.PhoneBook.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.Areas.PhoneBook.Security.Filter;

namespace Radyn.WebApp.Areas.PhoneBook.Controllers
{
    public class DepartmentController : PhoneBookBaseController<Department>
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = PhoneBookComponenets.Instance.DepartmentFacade.GetAll();
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Int16 Id)
        {
            return View(PhoneBookComponenets.Instance.DepartmentFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            ViewBag.OfficeList = new SelectList(PhoneBookComponenets.Instance.OfficeFacade.OrderBy(x => x.Title), "Id", "Title");

            return View(new Department());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var Department = new Department();
            try
            {
                this.RadynTryUpdateModel(Department);
                if (PhoneBookComponenets.Instance.DepartmentFacade.Insert(Department))
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
                return View(Department);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int16 Id)
        {
            ViewBag.OfficeList = new SelectList(PhoneBookComponenets.Instance.OfficeFacade.OrderBy(x => x.Title), "Id", "Title");

            return View(PhoneBookComponenets.Instance.DepartmentFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(Int16 Id, FormCollection collection)
        {
            var Department = PhoneBookComponenets.Instance.DepartmentFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(Department);
                if (PhoneBookComponenets.Instance.DepartmentFacade.Update(Department))
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
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return View(Department);
            }
        }

      

        [RadynAuthorize]
        public ActionResult Delete(Int16 Id)
        {
            return View(PhoneBookComponenets.Instance.DepartmentFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Int16 Id, FormCollection collection)
        {
            var Department = PhoneBookComponenets.Instance.DepartmentFacade.Get(Id);
            try
            {
                if (PhoneBookComponenets.Instance.DepartmentFacade.Delete(Id))
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
                return View(Department);
            }
        }
        public JsonResult GetDepartment(int? officeId)
        {
            var obj = new Object();
            var result = new List<object>();
            var list = PhoneBookComponenets.Instance.DepartmentFacade.Select(new Expression<Func<Department, object>>[]
            {
                x => x.Id,
                x => x.Title
            }, x => x.OfficeId == officeId || x.OfficeId == null);
            foreach (var item in list)
            {
                obj = new
                {
                    id = item.Id,
                    title = item.Title
                };
                result.Add(obj);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}