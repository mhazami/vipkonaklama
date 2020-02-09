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
using System.Web.Script.Serialization;

namespace Radyn.WebApp.Areas.Reservation.Controllers
{
    public class OfficeRoomController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid officeId)
        {
            //var list = ReservationComponent.Instance.OfficeRoomFacade.Where(x => x.OfficeId == officeId);
            //return View(list);
            var office = ReservationComponent.Instance.HotelOfficeFacade.Get(officeId);
            var tree = ReservationComponent.Instance.RoomFacade.GetTree(office.HotelId, officeId);
            var result = new JavaScriptSerializer().Serialize(tree);
            return View(result);
        }

        public ActionResult GetRooms()
        {
            var officeId = Request.QueryString["officeId"].ToGuid();
            var office = ReservationComponent.Instance.HotelOfficeFacade.Get(officeId);
            var tree = ReservationComponent.Instance.RoomFacade.GetTree(office.HotelId, officeId);

            var x = new JavaScriptSerializer().Serialize(tree);
            return PartialView("PVGetOperation", x);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(ReservationComponent.Instance.OfficeRoomFacade.Get(Id));
        }

        private void FillViewBags()
        {
            ViewBag.HotelOffice = new SelectList(ReservationComponent.Instance.HotelOfficeFacade.GetAll(), "Id", "Title");
            ViewBag.Room = new SelectList(ReservationComponent.Instance.RoomFacade.GetAll(), "Id", "Title");
        }
        [RadynAuthorize]
        public ActionResult Create()
        {
            FillViewBags();
            return View(new OfficeRoom());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var officeRoom = new OfficeRoom();
            try
            {
                this.RadynTryUpdateModel(officeRoom);
                if (ReservationComponent.Instance.OfficeRoomFacade.Insert(officeRoom))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return (!string.IsNullOrEmpty(Request.QueryString["AddNew"])) ? this.Redirect("~/Reservation/OfficeRoom/Create") : this.Redirect("~/Reservation/OfficeRoom/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/OfficeRoom/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags();
                return View(officeRoom);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            FillViewBags();
            return View(ReservationComponent.Instance.OfficeRoomFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var officeRoom = ReservationComponent.Instance.OfficeRoomFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(officeRoom);
                if (ReservationComponent.Instance.OfficeRoomFacade.Update(officeRoom))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/OfficeRoom/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/OfficeRoom/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags();
                return View(officeRoom);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(ReservationComponent.Instance.OfficeRoomFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var officeRoom = ReservationComponent.Instance.OfficeRoomFacade.Get(Id);
            try
            {
                if (ReservationComponent.Instance.OfficeRoomFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/OfficeRoom/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/OfficeRoom/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(officeRoom);
            }
        }
    }
}
