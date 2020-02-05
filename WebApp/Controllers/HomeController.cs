using Radyn.Reservation;
using Radyn.Reservation.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.AppCode.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Radyn.Reservation.Enum;
using Enums = Radyn.WebDesign.Definition.Enums;

namespace Radyn.WebApp.Controllers
{
    public class HomeController : WebDesignBaseController
    {
        public ActionResult Test()
        {
            return View();
        }

        [WebDesignHost]
        
        public ActionResult Index(string culture)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                SessionParameters.Culture = culture;
                return RedirectToAction("Index");
            }
            if (SessionParameters.CurrentWebSite != null &&
                SessionParameters.CurrentWebSite.Status == Enums.WebSiteStatus.NoProblem &&
                SessionParameters.CurrentWebSite.Configuration.IntroPageUrl != null &&
                !string.IsNullOrEmpty(SessionParameters.CurrentWebSite.Configuration.IntroPageUrl.Trim()))
                return Redirect(SessionParameters.CurrentWebSite.Configuration.IntroPageUrl);
            return View(SessionParameters.CurrentWebSite);
        }


        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }


        public ActionResult UserReservation()
        {
            return View();
        }

        public ActionResult RoomType()
        {
            return PartialView("PVRoomType");
        }
        private void FillViewBags()
        {
            var time = Utility.EnumUtils.ConvertEnumToIEnumerableInLocalization<TimeSheet>().Select(x => new KeyValuePair<string, string>(x.Key, x.Value));
            ViewBag.Time = new SelectList(time, "Key", "Value");
           // ViewBag.ReserveType = new SelectList(ReservationComponent.Instance.ReserveTypeFacade.SelectKeyValuePair(x => x.Id, x => x.Title), "Key", "Value");
        }
        public ActionResult Order(byte roomtypeId)
        {
            FillViewBags();
            var order = new Order()
            {
                RoomTypeId = roomtypeId,

            };
            return PartialView("PVOrder", order);
        }

        [HttpPost]
        public ActionResult Order(FormCollection collection)
        {
            var order = new Order();
            try
            {
                this.RadynTryUpdateModel(order);
                if (ReservationComponent.Instance.OrderFacade.Insert(order))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect($"~/Home/Order?roomtypeId={order.RoomTypeId}");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect($"~/Home/Order?roomtypeId={order.RoomTypeId}");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags();
                return View(order);
            }
        }

    }
}
