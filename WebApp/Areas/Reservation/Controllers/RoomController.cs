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
    public class RoomController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid floorId)
        {
            List<Room> list;
            list = ReservationComponent.Instance.RoomFacade.Where(x => x.FloorId == floorId);
            if (list.Count == 0) return RedirectToAction("Create", new { FloorId = floorId });
            ViewBag.Floor = ReservationComponent.Instance.HotelFloorFacade.Get(floorId);
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(ReservationComponent.Instance.RoomFacade.Get(Id));
        }

        private void FillViewBags(Guid floorId)
        {
            var floor = ReservationComponent.Instance.HotelFloorFacade.Get(floorId);
            ViewBag.RoomType = new SelectList(ReservationComponent.Instance.RoomTypeFacade.SelectKeyValuePair(x => x.Id, x => x.Title, x => x.HotelId == floor.HotelId), "Key", "Value");
        }
        [RadynAuthorize]
        public ActionResult Create(Guid floorId)
        {
            var floor = ReservationComponent.Instance.HotelFloorFacade.Get(floorId);
            FillViewBags(floorId);
            var room = new Room() { FloorId = floorId, HotelFloor = floor };
            return View(room);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var room = new Room();
            try
            {
                this.RadynTryUpdateModel(room);
                if (ReservationComponent.Instance.RoomFacade.Insert(room))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return  this.Redirect("~/Reservation/Room/Index?floorId=" + room.FloorId);
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Room/Index?floorId=" + room.FloorId);
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags(collection["FloorId"].ToGuid());
                return View(room);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            var item = ReservationComponent.Instance.RoomFacade.Get(Id);
            FillViewBags(item.FloorId);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var room = ReservationComponent.Instance.RoomFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(room);
                if (ReservationComponent.Instance.RoomFacade.Update(room))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/Room/Index?floorId=" + room.FloorId);
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Room/Index?floorId=" + room.FloorId);
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags(collection["FloorId"].ToGuid());
                return View(room);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(ReservationComponent.Instance.RoomFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var room = ReservationComponent.Instance.RoomFacade.Get(Id);
            try
            {
                if (ReservationComponent.Instance.RoomFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/Room/Index?floorId=" + room.FloorId);
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Room/Index?floorId=" + room.FloorId);
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(room);
            }
        }

        public JsonResult GetRoomsByType(byte roomType, Guid? roomId, string entryDate, string exitDate, string entryTime, string exitTime)
        {
            var results = new List<object>();
            if (string.IsNullOrEmpty(entryDate) && string.IsNullOrEmpty(exitDate))
            {
                return Json(results, JsonRequestBehavior.AllowGet);
            }
            PredicateBuilder<Order> query = new PredicateBuilder<Order>();
            query.And(x => x.EntryDate.CompareTo(entryDate) >= 1 && x.ExitDate.CompareTo(entryDate) <= 1);
            query.And(x => x.EntryDate.CompareTo(exitDate) >= 1 && x.ExitDate.CompareTo(exitDate) <= 1);
            IEnumerable<Room> list;
            if (roomId.HasValue)
                list = ReservationComponent.Instance.RoomFacade.Where(x => (x.Idle && x.RoomTypeId == roomType) || x.Id == roomId);
            else
                list = ReservationComponent.Instance.RoomFacade.Where(x => x.Idle && x.RoomTypeId == roomType);

            foreach (var element in list)
            {
                results.Add(new { id = element.Id, title = element.Title });
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

    }
}
