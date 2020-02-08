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
    public class HotelFloorController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ReservationComponent.Instance.HotelFloorFacade.GetAll();
            if (list.Count == 0) return RedirectToAction("Create");
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(ReservationComponent.Instance.HotelFloorFacade.Get(Id));
        }

        private void FillViewBags()
        {
            ViewBag.Hotel = new SelectList(ReservationComponent.Instance.HotelFacade.SelectKeyValuePair(x => x.Id, x => x.Name), "Key", "Value");
        }
        [RadynAuthorize]
        public ActionResult Create()
        {
            FillViewBags();
            return View(new HotelFloor());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var hotelFloor = new HotelFloor();
            try
            {
                this.RadynTryUpdateModel(hotelFloor);
                if (ReservationComponent.Instance.HotelFloorFacade.Insert(hotelFloor))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");

                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");

            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags();
                return View(hotelFloor);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            FillViewBags();
            return View(ReservationComponent.Instance.HotelFloorFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var hotelFloor = ReservationComponent.Instance.HotelFloorFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(hotelFloor);
                if (ReservationComponent.Instance.HotelFloorFacade.Update(hotelFloor))
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
                FillViewBags();
                return View(hotelFloor);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(ReservationComponent.Instance.HotelFloorFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var hotelFloor = ReservationComponent.Instance.HotelFloorFacade.Get(Id);
            try
            {
                if (ReservationComponent.Instance.HotelFloorFacade.Delete(Id))
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
                return View(hotelFloor);
            }
        }
    }
}
