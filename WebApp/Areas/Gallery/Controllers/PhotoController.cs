using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Radyn.Gallery;
using Radyn.Gallery.DataStructure;
using Radyn.Gallery.Tools;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Gallery.Controllers
{
    public class PhotoController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid GId, string externalurl = "")
        {
            ViewBag.GID = GId;
            ViewBag.externalurl = externalurl;
            return View(GalleryComponent.Instance.PhotoFacade.Where(x => x.GalleryId.Equals(GId)));
        }

        [RadynAuthorize]
        public ActionResult Details(Guid id)
        {
            ViewBag.externalurl = Request.QueryString["externalurl"];
            return View(GalleryComponent.Instance.PhotoFacade.Get(id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            ViewBag.externalurl = Request.QueryString["externalurl"];
            return View(new Radyn.Gallery.DataStructure.Photo());
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var photo = new Radyn.Gallery.DataStructure.Photo();

            try
            {

                this.RadynTryUpdateModel(photo);
                photo.GalleryId = Request.QueryString["GId"].ToGuid();
                HttpPostedFileBase image = null;
                if (RadynSession["Image"] != null)
                {
                    image = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                photo.CurrentUICultureName = collection["LanguageId"];
                if (string.IsNullOrEmpty(photo.Title))
                {
                    ShowMessage(Resources.Gallery1.PleaseEnterTitle, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                    return RedirectToAction("Create", new { GId = Request.QueryString["GId"], externalurl = Request.QueryString["externalurl"] });
                }
                if (GalleryComponent.Instance.PhotoFacade.Insert(photo, image))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { GId = Request.QueryString["GId"], externalurl = Request.QueryString["externalurl"] });
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { GId = Request.QueryString["GId"], externalurl = Request.QueryString["externalurl"] });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.externalurl = Request.QueryString["externalurl"];
                return View(photo);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid id)
        {
            ViewBag.externalurl = Request.QueryString["externalurl"];
            ViewBag.ParentGallery = new SelectList(GalleryComponent.Instance.PhotoFacade.GetAll(), "Id", "Title");
            return View(GalleryComponent.Instance.PhotoFacade.Get(id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            var photo = GalleryComponent.Instance.PhotoFacade.Get(id);

            try
            {

                this.RadynTryUpdateModel(photo);


                HttpPostedFileBase image = null;
                if (RadynSession["Image"] != null)
                {
                    image = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                photo.CurrentUICultureName = collection["LanguageId"];
                if (string.IsNullOrEmpty(photo.Title))
                {
                    ShowMessage(Resources.Gallery1.PleaseEnterTitle, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                    return RedirectToAction("Edit", new { Id = id, GId = Request.QueryString["GId"], externalurl = Request.QueryString["externalurl"] });
                }

                if (GalleryComponent.Instance.PhotoFacade.Update(photo, image))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { GId = Request.QueryString["GId"], externalurl = Request.QueryString["externalurl"] });

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);

                return RedirectToAction("Index", new { GId = Request.QueryString["GId"], externalurl = Request.QueryString["externalurl"] });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.externalurl = Request.QueryString["externalurl"];
                return View(photo);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid id)
        {
            ViewBag.externalurl = Request.QueryString["externalurl"];
            return View(GalleryComponent.Instance.PhotoFacade.Get(id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            var photo = GalleryComponent.Instance.PhotoFacade.Get(id);

            try
            {
                if (GalleryComponent.Instance.PhotoFacade.Delete(id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { GId = Request.QueryString["GId"], externalurl = Request.QueryString["externalurl"] });
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { GId = Request.QueryString["GId"], externalurl = Request.QueryString["externalurl"] });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.externalurl = Request.QueryString["externalurl"];
                return View(photo);
            }
        }



        public ActionResult GalleryPics(string GalleryId, string Time)
        {
            if (!string.IsNullOrEmpty(GalleryId))
            {
                var GId = GalleryId.ToGuid();
                var photos = GalleryComponent.Instance.PhotoFacade.Select(new Expression<Func<Photo, object>>[] { x => x.PicFile, x => x.Title, }, x => x.GalleryId.Equals(GId));
                return PartialView("GalleryViewer", photos);
            }
            return null;
        }

        public ActionResult GalleryPhotos(Guid galleyId)
        {

            var gallery = GalleryComponent.Instance.GalleryFacade.Get(galleyId);
            if (!gallery.HasImage())
            {
                ShowMessage(Resources.Gallery1.This_Gallery_Not_have_Photos);
                return null;
            }
            ViewBag.Category = gallery.Title;
            return View(gallery);
        }

        public ActionResult GetNewImageSlider(Guid galleryId)
        {

            var photo = GalleryComponent.Instance.PhotoFacade.Where(photo1 => photo1.GalleryId == galleryId);
            return PartialView("PartialViewSlide", photo);

        }

        public ActionResult ImageSlider(Guid galleryId)
        {
            var byFilter = GalleryComponent.Instance.PhotoFacade.Select(new Expression<Func<Photo, object>>[] { x => x.PicFile, x => x.Title }, photo => photo.GalleryId == galleryId);
            if (byFilter.Any())
                return PartialView("PartialPhotoSlider", byFilter);
            return Content("");
        }
    }
}
