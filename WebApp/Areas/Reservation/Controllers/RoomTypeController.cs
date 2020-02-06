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
    public class RoomTypeController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = ReservationComponent.Instance.RoomTypeFacade.GetAll();
            if (list.Count == 0) return this.Redirect("~/Reservation/RoomType/Create");
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(byte Id)
        {
            return View(ReservationComponent.Instance.RoomTypeFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new RoomType());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var roomType = new RoomType();
            try
            {
                this.RadynTryUpdateModel(roomType);
                if (ReservationComponent.Instance.RoomTypeFacade.Insert(roomType))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return (!string.IsNullOrEmpty(Request.QueryString["AddNew"])) ? this.Redirect("~/Reservation/RoomType/Create") : this.Redirect("~/Reservation/RoomType/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/RoomType/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(roomType);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(byte Id)
        {
            return View(ReservationComponent.Instance.RoomTypeFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(byte Id, FormCollection collection)
        {
            var roomType = ReservationComponent.Instance.RoomTypeFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(roomType);
                if (ReservationComponent.Instance.RoomTypeFacade.Update(roomType))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/RoomType/Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/RoomType/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(roomType);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(byte Id)
        {
            return View(ReservationComponent.Instance.RoomTypeFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(byte Id, FormCollection collection)
        {
            var roomType = ReservationComponent.Instance.RoomTypeFacade.Get(Id);
            try
            {
                if (ReservationComponent.Instance.RoomTypeFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return this.Redirect("~/Reservation/RoomType/Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return this.Redirect("~/Reservation/RoomType/Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(roomType);
            }
        }
        public ActionResult UploadFilePicId(HttpPostedFileBase fileBase)
        {
            var filePicId = Request.Files["UpFilePicId"];
            if (filePicId != null)
            {
                if (filePicId.InputStream != null)
                {
                    RadynSession["UpFilePicId"] = filePicId;
                }
            }
            return Content("true");
        }
        public ActionResult RemoveFilePicId(HttpPostedFileBase fileBase)
        {
            RadynSession.Remove("UpFilePicId");
            return Content("");
        }

        public JsonResult GetPersonCapacity(byte id)
        {
            var obj = new object();
            var roomtype = ReservationComponent.Instance.RoomTypeFacade.FirstOrDefault(x => x.Id == id);
            if (roomtype == null || roomtype.PersonCapacity == null)
            {
                obj = new
                {
                    person = 0
                };
            }
            else
            {
                obj = new
                {
                    person = roomtype.PersonCapacity
                };

            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}
