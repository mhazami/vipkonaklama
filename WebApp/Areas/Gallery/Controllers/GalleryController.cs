using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.Gallery;
using Radyn.Gallery.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Gallery.Controllers
{
    public class GalleryController : WebDesignBaseController
    {

        public ActionResult GetDetails(Guid id)

        {
            var obj = GalleryComponent.Instance.GalleryFacade.Get(id);
           
           return PartialView("PVDetails", obj);
        }
        public ActionResult GetModify(Guid? Id)
        {
            ViewBag.PhotoList = Id.HasValue
                ? GalleryComponent.Instance.PhotoFacade.Where(x => x.GalleryId == Id)
                : new List<Photo>();
            return PartialView("PVModify",
                Id.HasValue
                    ? GalleryComponent.Instance.GalleryFacade.Get(Id)
                    : new Radyn.Gallery.DataStructure.Gallery { Enabled = true });
        }

        [RadynAuthorize]
        public ActionResult Index(Guid? parentId)
        {
            var galleryFacade = GalleryComponent.Instance.GalleryFacade;
            ViewBag.ParentGallery = new SelectList(galleryFacade.SelectKeyValuePair(x => x.Id, x => x.Title, gallery => gallery.IsExternal == false), "Key", "Value");
            var predicateBuilder = new PredicateBuilder<Radyn.Gallery.DataStructure.Gallery>();
            if (parentId.HasValue)
                predicateBuilder.And(x => x.ParentGallery == parentId);
            else predicateBuilder.And(x => x.ParentGallery == null);
            var list = galleryFacade.Where(predicateBuilder.GetExpression());
          return View(list);
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Index(FormCollection formCollection)
        {
            var parentid = formCollection["Gallerys"];
            var facade = GalleryComponent.Instance.GalleryFacade;
            ViewBag.ParentGallery = new SelectList(facade.SelectKeyValuePair(x => x.Id, x => x.Title, gallery => gallery.IsExternal == false), "Key", "Value");
            var predicateBuilder = new PredicateBuilder<Radyn.Gallery.DataStructure.Gallery>();
            if (!string.IsNullOrEmpty(parentid))
                predicateBuilder.And(x => x.ParentGallery == parentid.ToGuid());
            else predicateBuilder.And(x => x.ParentGallery == null);
            var list = facade.Where(predicateBuilder.GetExpression());
            return View(list);
        }

        public ActionResult Details(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            TempData["ParentGallery"] = new SelectList(GalleryComponent.Instance.GalleryFacade.SelectKeyValuePair(x => x.Id, x => x.Title, gallery => gallery.IsExternal == false), "Key", "Value");
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var gallery = new Radyn.Gallery.DataStructure.Gallery { Enabled = true };

            try
            {
                this.RadynTryUpdateModel(gallery);
                HttpPostedFileBase image = null;
                if (RadynSession["Image"] != null)
                {
                    image = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }

                List<HttpPostedFileBase> fileBases = null;

                if (RadynSession["PhotoList"] != null)
                {
                    fileBases = (List<HttpPostedFileBase>)RadynSession["PhotoList"];
                    RadynSession.Remove("PhotoList");
                }
                gallery.CurrentUICultureName = collection["LanguageId"];
                if (GalleryComponent.Instance.GalleryFacade.Insert(gallery, image, fileBases))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                TempData["ParentGallery"] = new SelectList(GalleryComponent.Instance.GalleryFacade.SelectKeyValuePair(x => x.Id, x => x.Title, gallery1 => gallery1.IsExternal), "Key", "Value");
                return View(gallery);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid id)
        {
            TempData["ParentGallery"] = new SelectList(GalleryComponent.Instance.GalleryFacade.SelectKeyValuePair(x => x.Id, x => x.Title, gallery => gallery.IsExternal), "Key", "Value");
            ViewBag.Id = id;
            return View(GalleryComponent.Instance.GalleryFacade.Get(id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(Guid id, FormCollection collection)
        {

            var gallery = GalleryComponent.Instance.GalleryFacade.Get(id);
            try
            {
                this.RadynTryUpdateModel(gallery, collection);
                gallery.CurrentUICultureName = collection["LanguageId"];
                HttpPostedFileBase image = null;
                if (RadynSession["Image"] != null)
                {
                    image = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                List<HttpPostedFileBase> fileBases = null;

                if (RadynSession["PhotoList"] != null)
                {
                    fileBases = (List<HttpPostedFileBase>)RadynSession["PhotoList"];
                    RadynSession.Remove("PhotoList");
                }
                if (GalleryComponent.Instance.GalleryFacade.Update(gallery, image, fileBases))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                TempData["ParentGallery"] = new SelectList(GalleryComponent.Instance.GalleryFacade.SelectKeyValuePair(x => x.Id, x => x.Title, gallery1 => gallery1.IsExternal), "Key", "Value");
                ViewBag.Id = id;
                return View(gallery);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid id)
        {
            ViewBag.Id = id;
           return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(Guid id, FormCollection collection)
        {

            try
            {
                if (GalleryComponent.Instance.GalleryFacade.Delete(id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return RedirectToAction("Delete",new {Id=id});
            }
        }

        public ActionResult GalleryList()
        {
            var list = GalleryComponent.Instance.GalleryFacade.OrderBy(x=>x.Order);
            if (list.Count == 1)
            {
                RedirectToAction("GalleryPhotos", "Photo", new { Area = "Gallery", galleyId = list[0].Id });
            }
            return View(list.ToList());
        }

      
     
    }
}
