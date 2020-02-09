using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Radyn.Reservation;
using Radyn.Reservation.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Reservation.Controllers
{
    public class HotelFloorController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid hotelId)
        {
            List<HotelFloor> list;
            list = ReservationComponent.Instance.HotelFloorFacade.Where(x => x.HotelId == hotelId);
            if (list.Count == 0) return RedirectToAction("Create", new { hotelId = hotelId });
            ViewBag.Hotel = ReservationComponent.Instance.HotelFacade.Get(hotelId);
            return View(list);
        }
        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(ReservationComponent.Instance.HotelFloorFacade.Get(Id));
        }
        [RadynAuthorize]
        public ActionResult Create(Guid hotelId)
        {
            var floor = new HotelFloor();
            floor.HotelId = hotelId;
            floor.Hotel = ReservationComponent.Instance.HotelFacade.Get(hotelId);
            return View(floor);
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
                    return RedirectToAction("Index", new { hotelId = hotelFloor.HotelId });
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { hotelId = collection["HotelId"] });

            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(hotelFloor);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            var hotelFloor = ReservationComponent.Instance.HotelFloorFacade.Get(Id);
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
                    return RedirectToAction("Index", new { hotelId = hotelFloor.HotelId });

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { hotelId = collection["HotelId"] });

            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
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
                    return RedirectToAction("Index", new { hotelId = hotelFloor.HotelId });

                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { hotelId = hotelFloor.HotelId });
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(hotelFloor);
            }
        }
    }
}
