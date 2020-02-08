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
using static Radyn.Reservation.Enum;

namespace Radyn.WebApp.Areas.Reservation.Controllers
{
    public class ReservePriceController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ReservationComponent.Instance.ReservePriceFacade.GetAll();
            if (list.Count == 0) return this.Redirect("~/Reservation/ReservePrice/Create");
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(ReservationComponent.Instance.ReservePriceFacade.Get(Id));
        }

        private void FillViewBags()
        {
            var time = Utility.EnumUtils.ConvertEnumToIEnumerableInLocalization<TimeSheet>().Select(x => new KeyValuePair<string, string>(x.Key, x.Value));
            var dayType = Utility.EnumUtils.ConvertEnumToIEnumerableInLocalization<DayType>().Select(x => new KeyValuePair<string, string>(x.Key, x.Value));
            ViewBag.Time = new SelectList(time, "Key", "Value");
            ViewBag.DayType = new SelectList(dayType, "Key", "Value");
            ViewBag.ReserveType = new SelectList(ReservationComponent.Instance.ReserveTypeFacade.SelectKeyValuePair(x => x.Id, x => x.Title), "Key", "Value");
            ViewBag.RoomType = new SelectList(ReservationComponent.Instance.RoomTypeFacade.SelectKeyValuePair(x => x.Id, x => x.Title), "Key", "Value");
        }
        [RadynAuthorize]
        public ActionResult Create()
        {
            FillViewBags();
            return View(new ReservePrice());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var reservePrice = new ReservePrice();
            try
            {
                this.RadynTryUpdateModel(reservePrice);
                if (ReservationComponent.Instance.ReservePriceFacade.Insert(reservePrice))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return (!string.IsNullOrEmpty(Request.QueryString["AddNew"])) ? this.Redirect("~/Reservation/ReservePrice/Create") : this.Redirect("~/Reservation/ReservePrice/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/ReservePrice/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags();
                return View(reservePrice);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            FillViewBags();
            return View(ReservationComponent.Instance.ReservePriceFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var reservePrice = ReservationComponent.Instance.ReservePriceFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(reservePrice);
                if (ReservationComponent.Instance.ReservePriceFacade.Update(reservePrice))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/ReservePrice/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/ReservePrice/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags();
                return View(reservePrice);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(ReservationComponent.Instance.ReservePriceFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var reservePrice = ReservationComponent.Instance.ReservePriceFacade.Get(Id);
            try
            {
                if (ReservationComponent.Instance.ReservePriceFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/ReservePrice/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/ReservePrice/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(reservePrice);
            }
        }
    }
}
