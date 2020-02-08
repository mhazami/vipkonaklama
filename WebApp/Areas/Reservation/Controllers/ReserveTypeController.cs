using Radyn.Reservation;
using Radyn.Reservation.DataStructure;
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
        public ActionResult Index()
        {
            var list = ReservationComponent.Instance.ReserveTypeFacade.OrderBy(x => x.Order, x => x.Enable);
            if (list.Count() == 0) { return RedirectToAction("Create"); }
            return View(list);
        }

        public ActionResult Create()
        {
            return View(new ReserveType());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ReserveType reserveType = new ReserveType();
            try
            {
                this.RadynTryUpdateModel(reserveType, collection);
                reserveType.CurrentUICultureName = collection["LanguageId"];
                if (!ReservationComponent.Instance.ReserveTypeFacade.Insert(reserveType))
                {
                    ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(reserveType);
            }
            catch (Exception ex)
            {
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(reserveType);
            }
        }


        public ActionResult Edit(Guid id)
        {
            return View(id);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            var culture = collection["LanguageId"];
            ReserveType reserveType = ReservationComponent.Instance.ReserveTypeFacade.GetLanuageContent(culture, id);
            try
            {
                this.RadynTryUpdateModel(reserveType, collection);
                if (!ReservationComponent.Instance.ReserveTypeFacade.Update(reserveType))
                {
                    ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(id);
            }
            catch (Exception ex)
            {
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(id);
            }
        }


        public ActionResult Details(Guid id)
        {
            return View(id);
        }

        public ActionResult GetModify(Guid? id, string culture)
        {
            ReserveType reservetype = null;
            if (id.HasValue && id != Guid.Empty)
            {
                reservetype = ReservationComponent.Instance.ReserveTypeFacade.GetLanuageContent(culture, id);
            }
            else
            {
                reservetype = new ReserveType();
            }
            this.FillViewBag();
            return PartialView("PVModify", reservetype);
        }

        public ActionResult GetDetails(Guid id,string culture)
        {
            var reservetype = ReservationComponent.Instance.ReserveTypeFacade.GetLanuageContent(culture, id);
            return PartialView("PVDetails", reservetype);
        }

        public ActionResult Delete(Guid id)
        {
            return View(id);
        }

        [HttpPost]
        public ActionResult Delete(Guid id,FormCollection collection)
        {
            var culture = collection["LanguageId"];
            ReserveType reserveType = ReservationComponent.Instance.ReserveTypeFacade.GetLanuageContent(culture, id);
            try
            {
                if (!ReservationComponent.Instance.ReserveTypeFacade.Delete(reserveType))
                {
                    ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(id);
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