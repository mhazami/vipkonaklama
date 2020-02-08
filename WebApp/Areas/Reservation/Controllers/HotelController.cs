using Radyn.Common;
using Radyn.Reservation;
using Radyn.Reservation.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Reservation.Controllers
{
    public class HotelController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ReservationComponent.Instance.HotelFacade.GetAll();
            if (list.Count == 0) return this.Redirect("~/Reservation/Hotel/Create");
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(ReservationComponent.Instance.HotelFacade.Get(Id));
        }

        private void FillViewBags()
        {
            ViewBag.Country = new SelectList(CommonComponent.Instance.CountryFacade.SelectKeyValuePair(x => x.Id, x => x.Name), "Key", "Value");
            ViewBag.Province = new SelectList(CommonComponent.Instance.ProvinceFacade.SelectKeyValuePair(x => x.Id, x => x.Title), "Key", "Value");
            ViewBag.City = new SelectList(CommonComponent.Instance.CityFacade.SelectKeyValuePair(x => x.Id, x => x.Title), "Key", "Value");
            ViewBag.Parish = new SelectList(CommonComponent.Instance.ParishFacade.SelectKeyValuePair(x => x.Id, x => x.Name), "Key", "Value");
        }
        [RadynAuthorize]
        public ActionResult Create()
        {
            FillViewBags();
            return View(new Hotel());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var hotel = new Hotel();
            try
            {
                HttpPostedFileBase image = null;
                if (RadynSession["Image"] != null)
                {
                    image = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                    hotel.Picture = image.PraperFile();
                }
                this.RadynTryUpdateModel(hotel);
                if (ReservationComponent.Instance.HotelFacade.Insert(hotel))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return (!string.IsNullOrEmpty(Request.QueryString["AddNew"])) ? this.Redirect("~/Reservation/Hotel/Create") : this.Redirect("~/Reservation/Hotel/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Hotel/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags();
                return View(hotel);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            FillViewBags();
            return View(ReservationComponent.Instance.HotelFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var hotel = ReservationComponent.Instance.HotelFacade.Get(Id);
            try
            {
                HttpPostedFileBase image = null;
                if (RadynSession["Image"] != null)
                {
                    image = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                    hotel.Picture = image.PraperFile();
                }
                this.RadynTryUpdateModel(hotel);
                if (ReservationComponent.Instance.HotelFacade.Update(hotel))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/Hotel/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Hotel/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags();
                return View(hotel);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(ReservationComponent.Instance.HotelFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var hotel = ReservationComponent.Instance.HotelFacade.Get(Id);
            try
            {
                if (ReservationComponent.Instance.HotelFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/Hotel/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Hotel/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(hotel);
            }
        }
    }
}
