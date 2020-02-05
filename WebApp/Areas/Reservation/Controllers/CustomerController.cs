using Radyn.Common;
using Radyn.Reservation;
using Radyn.Reservation.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Reservation.Controllers
{
    public class CustomerController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ReservationComponent.Instance.CustomerFacade.GetAll();
            if (list.Count == 0) return this.Redirect("~/Reservation/Customer/Create");
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(ReservationComponent.Instance.CustomerFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            FillDrp();
            return View(new Customer());
        }

        private void FillDrp()
        {
            ViewBag.Country = new SelectList(CommonComponent.Instance.CountryFacade.SelectKeyValuePair(x => x.Id, x => x.PhoneCode + "(" + x.Name + ")"), "Key", "Value");
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var customer = new Customer();
            FillDrp();
            try
            {
                this.RadynTryUpdateModel(customer);
                if (ReservationComponent.Instance.CustomerFacade.Insert(customer))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return (!string.IsNullOrEmpty(Request.QueryString["AddNew"])) ? this.Redirect("~/Reservation/Customer/Create") : this.Redirect("~/Reservation/Customer/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Customer/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(customer);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            FillDrp();
            return View(ReservationComponent.Instance.CustomerFacade.SimpleGet(Id));
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var customer = ReservationComponent.Instance.CustomerFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(customer);
                if (ReservationComponent.Instance.CustomerFacade.Update(customer))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/Customer/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Customer/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(customer);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(ReservationComponent.Instance.CustomerFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var customer = ReservationComponent.Instance.CustomerFacade.Get(Id);
            try
            {
                if (ReservationComponent.Instance.CustomerFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/Customer/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Customer/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(customer);
            }
        }
    }
}
