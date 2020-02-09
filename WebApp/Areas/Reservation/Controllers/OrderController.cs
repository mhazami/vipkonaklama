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
using static Radyn.Reservation.Enum;

namespace Radyn.WebApp.Areas.Reservation.Controllers
{
    public class OrderController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid hotelId)
        {
            ViewBag.HotelId = hotelId;
            var list = ReservationComponent.Instance.OrderFacade.Where(x => x.ReserveType.HotelId == hotelId);
            if (list.Count == 0) return this.Redirect("~/Reservation/Order/Create?hotelId=" + hotelId);
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(ReservationComponent.Instance.OrderFacade.Get(Id));
        }

        private void FillViewBags(Guid hotelId)
        {
            ViewBag.Room = new SelectList(ReservationComponent.Instance.RoomFacade.SelectKeyValuePair(x => x.Id, x => x.Title, x => x.Idle && x.HotelFloor.HotelId == hotelId), "Key", "Value", "");
            ViewBag.RoomType = new SelectList(ReservationComponent.Instance.RoomTypeFacade.SelectKeyValuePair(x => x.Id, x => x.Title, x => x.HotelId == hotelId), "Key", "Value");
            ViewBag.ReserveType = new SelectList(ReservationComponent.Instance.ReserveTypeFacade.SelectKeyValuePair(x => x.Id, x => x.Title, x => x.HotelId == hotelId), "Key", "Value");
            ViewBag.Paymentype = EnumUtils.ConvertEnumToIEnumerableInLocalization<PaymentType>().Select(x => new KeyValuePair<string, string>(x.Key, x.Value)).ToList();
        }
        [RadynAuthorize]
        public ActionResult Create(Guid hotelId)
        {
            FillViewBags(hotelId);
            return View(new Order
            {
                OrderDate = DateTime.Now,
                ReserveType = new Radyn.Reservation.DataStructure.ReserveType() { HotelId = hotelId },
                RoomType = new RoomType() { HotelId = hotelId },
            });
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var order = new Order() { Customer = new Customer(), Room = new Room(), RoomType = new RoomType(), ReserveType = new Radyn.Reservation.DataStructure.ReserveType() };
            try
            {
                this.RadynTryUpdateModel(order, collection);
                this.RadynTryUpdateModel(order.Customer, collection);
                this.RadynTryUpdateModel(order.ReserveType, collection);
                if (SessionParameters.User != null)
                    order.UserId = SessionParameters.User.Id;
                if (ReservationComponent.Instance.OrderFacade.InsertWithCustomer(order))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/Order/Index?hotelId=" + order.ReserveType.HotelId);
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(order);
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags(order.ReserveType.HotelId);
                return View(order);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            var obj = ReservationComponent.Instance.OrderFacade.Get(Id);
            FillViewBags(obj.ReserveType.HotelId);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var order = ReservationComponent.Instance.OrderFacade.Get(Id);
            if (order.Customer == null)
            {
                order.Customer = new Customer();
            }
            try
            {
                this.RadynTryUpdateModel(order);
                this.RadynTryUpdateModel(order.Customer, collection);
                if (SessionParameters.User != null)
                    order.UserId = SessionParameters.User.Id;
                if (ReservationComponent.Instance.OrderFacade.UpdateWithCustomer(order))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/Order/Index?hotelId=" + order.ReserveType.HotelId);
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(order);
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags(order.ReserveType.HotelId);
                return View(order);
            }
        }
        [HttpPost]
        public ActionResult Checkout(FormCollection collection)
        {
            var Id = collection["Id"].ToGuid();
            var order = ReservationComponent.Instance.OrderFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(order);
                order.RoomId = null;
                if (SessionParameters.User != null)
                    order.UserId = SessionParameters.User.Id;
                if (ReservationComponent.Instance.OrderFacade.Update(order))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return Content("ok");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("ok");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags(order.ReserveType.HotelId);
                return Content(exception.Message);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(ReservationComponent.Instance.OrderFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var order = ReservationComponent.Instance.OrderFacade.Get(Id);
            try
            {
                if (ReservationComponent.Instance.OrderFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/Order/Index?hotelId=" + order.ReserveType.HotelId);
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(order);
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(order);
            }
        }

        public string CalculateOfferPrice(DateTime? exitDate, DateTime? entryDate, Guid reserveType, byte? roomtypeId)
        {
            if (!roomtypeId.HasValue || roomtypeId == 0 || reserveType == Guid.Empty || !entryDate.HasValue || !exitDate.HasValue)
            {
                return "0";
            }
            var price = ReservationComponent.Instance.OrderFacade.GetTotalPrice(entryDate.Value, exitDate.Value, roomtypeId.Value, reserveType);
            return price.ToString();
        }
    }
}
