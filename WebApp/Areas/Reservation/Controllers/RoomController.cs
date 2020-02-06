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
        public ActionResult Index()
        {
            var list = ReservationComponent.Instance.RoomFacade.GetAll();
            if (list.Count == 0) return this.Redirect("~/Reservation/Room/Create");
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(byte Id)
        {
            return View(ReservationComponent.Instance.RoomFacade.Get(Id));
        }

        private void FillViewBags()
        {
            ViewBag.RoomType = new SelectList(ReservationComponent.Instance.RoomTypeFacade.SelectKeyValuePair(x => x.Id, x => x.Title), "Key", "Value");
        }
        [RadynAuthorize]
        public ActionResult Create()
        {
            FillViewBags();
            return View(new Room());
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
                    return (!string.IsNullOrEmpty(Request.QueryString["AddNew"])) ? this.Redirect("~/Reservation/Room/Create") : this.Redirect("~/Reservation/Room/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Room/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags();
                return View(room);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(byte Id)
        {
            FillViewBags();
            return View(ReservationComponent.Instance.RoomFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(byte Id, FormCollection collection)
        {
            var room = ReservationComponent.Instance.RoomFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(room);
                if (ReservationComponent.Instance.RoomFacade.Update(room))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/Room/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Room/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags();
                return View(room);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(byte Id)
        {
            return View(ReservationComponent.Instance.RoomFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(byte Id, FormCollection collection)
        {
            var room = ReservationComponent.Instance.RoomFacade.Get(Id);
            try
            {
                if (ReservationComponent.Instance.RoomFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/Room/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Room/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(room);
            }
        }

        public JsonResult GetRoomsByType(byte roomType, short? roomId, string entryDate, string exitDate, string entryTime, string exitTime)
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
