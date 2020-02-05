using System;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.Common.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Common.Controllers
{
    public class ProvinceController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = CommonComponent.Instance.ProvinceFacade.GetAll();
            if (list.Count == 0) return this.Redirect("~/Common/Province/Create");
            return View(list);
        }
        public ActionResult GetDetail(int Id)
        {
            return PartialView("PVDetails", CommonComponent.Instance.ProvinceFacade.Get(Id));
        }
        public ActionResult GetModify(int? Id)
        {
            ViewBag.Country = new SelectList(CommonComponent.Instance.CountryFacade.GetAll(), "Id", "Name");
            return PartialView("PVModify", Id.HasValue ? CommonComponent.Instance.ProvinceFacade.Get(Id) : new Province());
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
        public ActionResult Create(FormCollection collection)
        {
            var province = new Province();
            try
            {
                this.RadynTryUpdateModel(province);
                if (CommonComponent.Instance.ProvinceFacade.Insert(province))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return (!string.IsNullOrEmpty(Request.QueryString["AddNew"])) ? this.Redirect("~/Common/Province/Create") : this.Redirect("~/Common/Province/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Common/Province/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(province);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int32 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Int32 Id, FormCollection collection)
        {
            var province = CommonComponent.Instance.ProvinceFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(province);
                if (CommonComponent.Instance.ProvinceFacade.Update(province))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Common/Province/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Common/Province/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(province);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int32 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Int32 Id, FormCollection collection)
        {
            var province = CommonComponent.Instance.ProvinceFacade.Get(Id);
            try
            {
                if (CommonComponent.Instance.ProvinceFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Common/Province/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Common/Province/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View(province);
            }
        }
    }
}
