using Radyn.Reservation;
using Radyn.Reservation.DataStructure;
using Radyn.Security;
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
    public class UserHotelAccessController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ReservationComponent.Instance.UserHotelAccessFacade.GetAll();
            if (list.Count == 0) return RedirectToAction("Create");
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details()
        {
            return View(ReservationComponent.Instance.UserHotelAccessFacade.Get());
        }

        private void FillViewBags()
        {
            ViewBag.User = new SelectList(SecurityComponent.Instance.UserFacade.GetAll(), "Id", "DescriptionField");
            ViewBag.Hotel = new SelectList(ReservationComponent.Instance.HotelFacade.SelectKeyValuePair(x => x.Id, x => x.Name), "Key", "Value");
            ViewBag.HotelOffice = new SelectList(ReservationComponent.Instance.HotelOfficeFacade.SelectKeyValuePair(x => x.Id, x => x.Name), "Key", "Value");
            var rooms = ReservationComponent.Instance.RoomFacade.GetAll();
            ReservationComponent.Instance.RoomFacade.GetLanuageContent(SessionParameters.Culture,rooms);
            ViewBag.Room = new SelectList(rooms, "Id", "Title");
        }
        [RadynAuthorize]
        public ActionResult Create()
        {
            FillViewBags();
            return View(new UserHotelAccess());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var userHotelAccess = new UserHotelAccess();
            try
            {
                this.RadynTryUpdateModel(userHotelAccess);
                if (ReservationComponent.Instance.UserHotelAccessFacade.Insert(userHotelAccess))
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
                return View(userHotelAccess);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit()
        {
            FillViewBags();
            return View(ReservationComponent.Instance.UserHotelAccessFacade.Get());
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            var userHotelAccess = ReservationComponent.Instance.UserHotelAccessFacade.Get();
            try
            {
                this.RadynTryUpdateModel(userHotelAccess);
                if (ReservationComponent.Instance.UserHotelAccessFacade.Update(userHotelAccess))
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
                return View(userHotelAccess);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete()
        {
            return View(ReservationComponent.Instance.UserHotelAccessFacade.Get());
        }

        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            var userHotelAccess = ReservationComponent.Instance.UserHotelAccessFacade.Get();
            try
            {
                if (ReservationComponent.Instance.UserHotelAccessFacade.Delete())
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
                return View(userHotelAccess);
            }
        }
    }
}
