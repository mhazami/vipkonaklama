using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Radyn.Advertisements;
using Radyn.Advertisements.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Advertisements.Controllers
{
    public class AdvertisementController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = AdvertisementsComponent.Instance.AdvertisementFacade.GetAll();
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(AdvertisementsComponent.Instance.AdvertisementFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            ViewBag.AdvertisementSectionPosition = new SelectList(AdvertisementsComponent.Instance.AdvertisementSectionPositionFacade.GetAll(), "Id", "Title");
            ViewBag.AdvertisementType = new SelectList(AdvertisementsComponent.Instance.AdvertisementTypeFacade.GetAll(), "Id", "Type");
            return View(new Advertisement());
        }

        [HttpPost, ValidateInput(false)]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var advertisement = new Advertisement();
            try
            {
                
                this.RadynTryUpdateModel(advertisement, collection);
                HttpPostedFileBase file = null;
                if (RadynSession["upFile"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["upFile"];
                    RadynSession.Remove("upFile");
                }

                if (AdvertisementsComponent.Instance.AdvertisementFacade.Insert(advertisement, file))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(advertisement);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            ViewBag.AdvertisementSectionPosition = new SelectList(AdvertisementsComponent.Instance.AdvertisementSectionPositionFacade.GetAll(), "Id", "Title");
            ViewBag.AdvertisementType = new SelectList(AdvertisementsComponent.Instance.AdvertisementTypeFacade.GetAll(), "Id", "Type");
            return View(AdvertisementsComponent.Instance.AdvertisementFacade.Get(Id));
        }

        [HttpPost, ValidateInput(false)]
        [RadynAuthorize]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var advertisement = AdvertisementsComponent.Instance.AdvertisementFacade.Get(Id);
            try
            {
               
                this.RadynTryUpdateModel(advertisement, collection);
                HttpPostedFileBase file = null;
                if (RadynSession["upFile"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["upFile"];
                    RadynSession.Remove("upFile");
                }

                if (AdvertisementsComponent.Instance.AdvertisementFacade.Update(advertisement, file))
                {

                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(advertisement);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(AdvertisementsComponent.Instance.AdvertisementFacade.Get(Id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var advertisement = AdvertisementsComponent.Instance.AdvertisementFacade.Get(Id);
            try
            {
                if (AdvertisementsComponent.Instance.AdvertisementFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInDelete + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return View(advertisement);
            }
        }

        public ActionResult UploadFile(IEnumerable<HttpPostedFileBase> fileBase)
        {
            HttpPostedFileBase file = Request.Files["upFile"];
            if (file != null)
            {
                if (file.InputStream != null)
                {

                    RadynSession["upFile"] = file;
                }
            }
            return Content("");
        }
        [RadynAuthorize(DoAuthorize = false)]

        public ActionResult RenderAdvertisement(string keyword)
        {
            var AdsHtml = AdvertisementsComponent.Instance.AdvertisementFacade.GetHtml4Position(keyword);
            return PartialView("AdsViewPartial", AdsHtml);
        }

        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult AdsClicked(Guid advId)
        {
            AdvertisementsComponent.Instance.AdvertisementFacade.AdsAddClickCount(advId);
            return null;
        }
        
        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult GetNewAdvertisement(string positionId, string SPkeyword)
        {
            var AdverTisementContent = AdvertisementsComponent.Instance.AdvertisementFacade.GetNewAdvertisement(positionId, SPkeyword);
            return PartialView("AdsViewPartial", AdverTisementContent);
        }
    }
}
