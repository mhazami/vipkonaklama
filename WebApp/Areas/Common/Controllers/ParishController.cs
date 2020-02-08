using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.Common.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Common.Controllers
{
    public class ParishController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = CommonComponent.Instance.ParishFacade.GetAll();
            return View(list);
        }
        public ActionResult GetByCityId(int CityId)
        {
            object obj = new object();
            List<object> result = new List<object>();

            List<KeyValuePair<string, string>> list = CommonComponent.Instance.ParishFacade.SelectKeyValuePair(x => x.Id, x => x.Name,
                x => x.CityId == CityId);
            foreach (KeyValuePair<string, string> item in list)
            {
                obj = new
                {
                    Id = item.Key,
                    Name = item.Value
                };
                result.Add(obj);
            }

            return Json(result, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetDetail(Guid Id)
        {
            return PartialView("PVDetails", CommonComponent.Instance.ParishFacade.Get(Id));
        }
        public ActionResult GetModify(Guid? Id)
        {
            ViewBag.Citylist = new SelectList(CommonComponent.Instance.CityFacade.GetAll(), "Id", "Title");
            return PartialView("PVModify", Id.HasValue ? CommonComponent.Instance.ParishFacade.Get(Id) : new Parish());
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
           
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var model = new Parish();

            try
            {
                this.RadynTryUpdateModel(model);
                if (CommonComponent.Instance.ParishFacade.Insert(model))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(model);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {

            var model = CommonComponent.Instance.ParishFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(model);
                if (CommonComponent.Instance.ParishFacade.Update(model))
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
                ViewBag.Id = Id;
                return View(model);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var model = CommonComponent.Instance.ParishFacade.Get(Id);

            try
            {
                if (CommonComponent.Instance.ParishFacade.Delete(Id))
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
                return View(model);
            }
        }
    }
}
