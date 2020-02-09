using Radyn.Reservation;
using Radyn.Reservation.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Radyn.Reservation.Enum;
using ReserveType = Radyn.Reservation.DataStructure.ReserveType;

namespace Radyn.WebApp.Areas.Reservation.Controllers
{
    public class ReserveTypeController : WebDesignBaseController
    {
        // GET: Reservation/ReserveType
        public ActionResult Index(Guid hotelId)
        {
            ViewBag.HotelId = hotelId;
            var list = ReservationComponent.Instance.ReserveTypeFacade.OrderBy(x => x.Order, x => x.HotelId == hotelId);
            if (list.Count() == 0) { return Redirect("~/Reservation/ReserveType/Create?hotelId=" + hotelId); }
            return View(list);
        }

        public ActionResult Create(Guid hotelId)
        {

            return View(new ReserveType() { HotelId = hotelId });
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ReserveType reserveType = new ReserveType();
            try
            {
                this.RadynTryUpdateModel(reserveType, collection);
                reserveType.CurrentUICultureName = collection["LanguageId"];
                reserveType = FixModel(reserveType);
                if (!ReservationComponent.Instance.ReserveTypeFacade.Insert(reserveType))
                {
                    ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                    return View(reserveType);
                }
                ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                return Redirect("~/Reservation/ReserveType/Index?hotelId=" + reserveType.HotelId);
            }
            catch (Exception ex)
            {
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(reserveType);
            }
        }
        private ReserveType FixModel(ReserveType obj)
        {
            obj.StartTime = obj.StartTime.ToEnum<TimeSheet>().GetDescriptionInLocalization();
            obj.EndTime = obj.EndTime.ToEnum<TimeSheet>().GetDescriptionInLocalization();
            return obj;
        }

        public ActionResult Edit(Guid id)
        {
            var obj = ReservationComponent.Instance.ReserveTypeFacade.Get(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            var culture = collection["LanguageId"];
            ReserveType reserveType = ReservationComponent.Instance.ReserveTypeFacade.GetLanuageContent(culture, id);
            try
            {
                this.RadynTryUpdateModel(reserveType, collection);
                reserveType = FixModel(reserveType);
                if (!ReservationComponent.Instance.ReserveTypeFacade.Update(reserveType))
                {
                    ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                    return View(id);
                }
                ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                return Redirect("~/Reservation/ReserveType/Index?hotelId=" + reserveType.HotelId);
            }
            catch (Exception ex)
            {
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(id);

            }
        }


        public ActionResult Details(Guid id)
        {
            var obj = ReservationComponent.Instance.ReserveTypeFacade.Get(id);
            return View(obj);
        }

        public ActionResult GetModify(Guid? id, Guid? hotelId, string culture)
        {
            ReserveType reservetype = null;
            if (id.HasValue && id != Guid.Empty)
            {
                reservetype = ReservationComponent.Instance.ReserveTypeFacade.GetLanuageContent(culture, id);
            }
            else
            {
                var hotel = ReservationComponent.Instance.HotelFacade.Get(hotelId);
                reservetype = new ReserveType() { HotelId = hotelId.Value, Hotel = hotel };
            }
            this.FillViewBag();
            return PartialView("PVModify", reservetype);
        }

        public ActionResult GetDetails(Guid id, string culture)
        {
            var reservetype = ReservationComponent.Instance.ReserveTypeFacade.GetLanuageContent(culture, id);
            return PartialView("PVDetails", reservetype);
        }

        public ActionResult Delete(Guid id)
        {
            var obj = ReservationComponent.Instance.ReserveTypeFacade.Get(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            var culture = collection["LanguageId"];
            ReserveType reserveType = ReservationComponent.Instance.ReserveTypeFacade.GetLanuageContent(culture, id);
            try
            {
                if (!ReservationComponent.Instance.ReserveTypeFacade.Delete(reserveType))
                {
                    ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                    return View(id);
                }
                ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                return Redirect("~/Reservation/ReserveType/Index?hotelId=" + reserveType.HotelId);
            }
            catch (Exception ex)
            {
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(id);
            }
        }

        private void FillViewBag()
        {
            var time = Utility.EnumUtils.ConvertEnumToIEnumerableInLocalization<TimeSheet>().Select(x => new KeyValuePair<string, string>(x.Key, x.Value));
            ViewBag.Time = new SelectList(time, "Key", "Value");
            ViewBag.Hotel = new SelectList(ReservationComponent.Instance.HotelFacade.SelectKeyValuePair(x => x.Id, x => x.Name), "Key", "Value");
        }
    }
}