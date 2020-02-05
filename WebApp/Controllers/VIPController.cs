using Radyn.Reservation;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Tools;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Constants;
using Radyn.WebApp.AppCode.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Radyn.Reservation.Enum;

namespace Radyn.WebApp.Controllers
{
    public class VIPController : WebDesignBaseController
    {
        // GET: VIP
        public ActionResult Index()
        {
            var x = TempData["Error"];
            return View();
        }

        public ActionResult ReservationBanner()
        {
            FillDrp();
            return PartialView("ReservationBanner");
        }

        [HttpPost]
        public ActionResult ReservationBanner(FormCollection collection)
        {
            var order = new Order();
            string result = string.Empty;
            try
            {
                this.RadynTryUpdateModel(order);

                if (ReservationComponent.Instance.OrderFacade.Insert(order))
                {
                    return Redirect($"/VIP/GetCustomerInfo?orderId={order.Id}");
                }
                throw new Exception("خطا در ذخیره اطلاعات");

            }
            catch (Exception exception)
            {
                TempData["Error"] = Constants.Errorbox(exception.Message);
                TempData.Keep("Error");
                return Redirect("/VIP/Index#test");
            }
        }

        public ActionResult GetCustomerInfo(Guid? orderId)
        {
            FillDrp();
            Order order = null;
            if (orderId.HasValue && orderId != Guid.Empty)
            {
                order = ReservationComponent.Instance.OrderFacade.Get(orderId);
            }
            else
            {
                order = new Order();
            }

            if (order.Customer == null)
            {
                order.Customer = new Customer();
            }
            return View(order);
        }
        [HttpPost]
        public ActionResult GetCustomerInfo(FormCollection collection)
        {
            Order order = null;

            try
            {
                var Id = Guid.Parse(collection["Id"]);
                if (Id != Guid.Empty)
                {
                    order = ReservationComponent.Instance.OrderFacade.Get(Id);
                    this.RadynTryUpdateModel(order);
                    if (order.Customer == null) order.Customer = new Customer();
                    order.Customer.FirstName = collection["FirstName"];
                    order.Customer.LastName = collection["LastName"];
                    order.Customer.Email = collection["Email"];
                    order.Customer.CountryId = collection["CountryId"].ToInt();
                    order.Customer.PhoneNumber = collection["PhoneNumber"];
                    if (ReservationComponent.Instance.OrderFacade.UpdateWithCustomer(order))
                    {
                        return Redirect($"/VIP/Payment?orderId={order.Id}");
                    }
                }
                else
                {
                    order = new Order() { Customer = new Customer() };
                    this.RadynTryUpdateModel(order);
                    if (ReservationComponent.Instance.OrderFacade.InsertWithCustomer(order))
                    {
                        return Redirect($"/VIP/Payment?orderId={order.Id}");
                    }
                }

                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(order);
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(order);
            }
        }

        public ActionResult Payment(Guid? orderId)
        {
            ViewBag.PaymentType = EnumUtils.ConvertEnumToIEnumerableInLocalization<PaymentType>().Select(x => new KeyValuePair<string, string>(x.Key, x.Value)).OrderByDescending(x => x.Key).ToList();
            var order = ReservationComponent.Instance.OrderFacade.Get(orderId);
            if (order.Customer == null)
            {
                order.Customer = new Customer();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult Payment(FormCollection collection)
        {
            var Id = collection["Id"].ToGuid();
            var order = ReservationComponent.Instance.OrderFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(order);
                if (ReservationComponent.Instance.OrderFacade.Update(order))
                {
                    return Redirect($"/VIP/Confirm?orderId={order.Id}");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(order);
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(order);
            }
        }

        public ActionResult Confirm(Guid? orderId)
        {            
            var order = ReservationComponent.Instance.OrderFacade.Get(orderId);
            if (order == null)
                return Redirect("/");
            if (order.Customer == null)
            {
                order.Customer = new Customer();
            }
            return View(order);
        }

        private void FillDrp()
        {
            ViewBag.RoomType = new SelectList(ReservationComponent.Instance.RoomTypeFacade.SelectKeyValuePair(x => x.Id, x => x.Title), "Key", "Value");
            ViewBag.ReserveType = new SelectList(EnumUtils.ConvertEnumToIEnumerableInLocalization<ReserveType>(), "Key", "Value");
        }

        public JsonResult GetTotalPrice(DateTime? startdate, DateTime? enddate, byte roomtypeId, ReserveType reserveType)
        {
            var obj = new object();
            if (!startdate.HasValue)
            {
                obj = new
                {
                    status = "Lütfen bir giriş tarihi seçin",
                    price = "0 TL"
                };
            }
            else if (!enddate.HasValue)
            {
                obj = new
                {
                    status = "Lütfen bir Çikiş tarihi seçin",
                    price = "0 TL"
                };
            }
            else if (startdate.Value.CompareTo(enddate) == 1)
            {
                obj = new
                {
                    status = "Giriş tarihi Çikiş tarihinden Sonra olamaz",
                    price = ""
                };
            }
            else
            {
                decimal price = ReservationComponent.Instance.OrderFacade.GetTotalPrice(startdate.Value, enddate.Value, roomtypeId, reserveType);
                obj = new
                {
                    status = "OK",
                    price = $"{price.ToString()} TL"
                };
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

    }
}