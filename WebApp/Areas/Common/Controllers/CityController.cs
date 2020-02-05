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
    public class CityController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = CommonComponent.Instance.CityFacade.GetAll();
            return View(list);
        }
        public ActionResult GetByProvinceId(int ProvinceId)
        {
            object obj = new object();
            List<object> result = new List<object>();

            List<KeyValuePair<string, string>> list = CommonComponent.Instance.CityFacade.SelectKeyValuePair(x => x.Id, x => x.Title,
                x => x.ProvinceId == ProvinceId);
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
        public ActionResult GetDetail(int Id)
        {
            return PartialView("PVDetails", CommonComponent.Instance.CityFacade.Get(Id));
        }
        public ActionResult GetModify(int? Id)
        {
            ViewBag.Provincelist = new SelectList(CommonComponent.Instance.ProvinceFacade.GetAll(), "Id", "Title");
            return PartialView("PVModify", Id.HasValue ? CommonComponent.Instance.CityFacade.Get(Id) : new City(){Enable = true});
        }
        [RadynAuthorize]
        public ActionResult Details(Int32 Id)
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
            var model = new City();

            try
            {
                this.RadynTryUpdateModel(model);
                if (CommonComponent.Instance.CityFacade.Insert(model))
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
        public ActionResult Edit(Int32 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(Int32 Id, FormCollection collection)
        {

            var model = CommonComponent.Instance.CityFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(model);
                if (CommonComponent.Instance.CityFacade.Update(model))
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
        public ActionResult Delete(Int32 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(Int32 Id, FormCollection collection)
        {
            var model = CommonComponent.Instance.CityFacade.Get(Id);

            try
            {
                if (CommonComponent.Instance.CityFacade.Delete(Id))
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
