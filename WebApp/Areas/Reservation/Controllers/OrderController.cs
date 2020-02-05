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
    public class OrderController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ReservationComponent.Instance.OrderFacade.GetAll();
            if (list.Count == 0) return this.Redirect("~/Reservation/Order/Create");
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(ReservationComponent.Instance.OrderFacade.Get(Id));
        }

        private void FillViewBags()
        {
            ViewBag.Room = new SelectList(ReservationComponent.Instance.RoomFacade.Where(x => x.Idle), "Id", "Title", "");
            ViewBag.RoomType = new SelectList(ReservationComponent.Instance.RoomTypeFacade.GetAll(), "Id", "Title");
            ViewBag.Customer = new SelectList(ReservationComponent.Instance.CustomerFacade.GetAll(), "Id", "Title");
            ViewBag.User = new SelectList(SecurityComponent.Instance.UserFacade.GetAll(), "Id", "Title");
        }
        [RadynAuthorize]
        public ActionResult Create()
        {
            FillViewBags();
            return View(new Order());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var order = new Order();
            try
            {
                this.RadynTryUpdateModel(order);
                if (ReservationComponent.Instance.OrderFacade.Insert(order))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return (!string.IsNullOrEmpty(Request.QueryString["AddNew"])) ? this.Redirect("~/Reservation/Order/Create") : this.Redirect("~/Reservation/Order/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Order/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags();
                return View(order);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            FillViewBags();
            return View(ReservationComponent.Instance.OrderFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var order = ReservationComponent.Instance.OrderFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(order);
                if (ReservationComponent.Instance.OrderFacade.Update(order))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/Order/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Order/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                FillViewBags();
                return View(order);
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
                    return this.Redirect("~/Reservation/Order/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/Order/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(order);
            }
        }
    }
}
