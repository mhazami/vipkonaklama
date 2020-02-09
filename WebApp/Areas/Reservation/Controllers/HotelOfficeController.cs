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
    public class HotelOfficeController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid hotelId)
        {
            ViewBag.HotelId = hotelId;
            List<HotelOffice> list;
            list = ReservationComponent.Instance.HotelOfficeFacade.Where(x => x.HotelId == hotelId);
            if (list.Count == 0) return RedirectToAction("Create", new { hotelId = hotelId });
            return View(list);
        }
        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(ReservationComponent.Instance.HotelOfficeFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create(Guid hotelId)
        {
            var office = new HotelOffice();
            office.HotelId = hotelId;
            office.Hotel = ReservationComponent.Instance.HotelFacade.Get(hotelId);
            return View(office);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var hotelOffice = new HotelOffice();
            try
            {
                this.RadynTryUpdateModel(hotelOffice);
                if (ReservationComponent.Instance.HotelOfficeFacade.Insert(hotelOffice))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { hotelId = hotelOffice.HotelId });
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { hotelId = collection["HotelId"] });

            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(hotelOffice);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            var hotelOffice = ReservationComponent.Instance.HotelOfficeFacade.Get(Id);
            return View(ReservationComponent.Instance.HotelOfficeFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var hotelOffice = ReservationComponent.Instance.HotelOfficeFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(hotelOffice);
                if (ReservationComponent.Instance.HotelOfficeFacade.Update(hotelOffice))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { hotelId = hotelOffice.HotelId });

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { hotelId = collection["HotelId"] });

            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(hotelOffice);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(ReservationComponent.Instance.HotelOfficeFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var hotelOffice = ReservationComponent.Instance.HotelOfficeFacade.Get(Id);
            try
            {
                if (ReservationComponent.Instance.HotelOfficeFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { hotelId = hotelOffice.HotelId });

                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { hotelId = hotelOffice.HotelId });
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(hotelOffice);
            }
        }
    }
}
