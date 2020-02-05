using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebDesign;
using Radyn.WebDesign.DataStructure;
using Radyn.WebDesign.Definition;

namespace Radyn.WebApp.Areas.WebDesign.Controllers
{
    public class ResourceController : WebDesignBaseController
    {

        public ActionResult GetDetails(Guid Id)
        {
            var resource = WebDesignComponent.Instance.ResourceFacade.Get(Id);
            ViewBag.UseLayouts = EnumUtils.ConvertEnumToIEnumerable<Enums.UseLayout>().Select(
                keyValuePair =>
                    new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Enums.UseLayout>(),
                        keyValuePair.Value));
            return PartialView("PVDetails", resource);
        }
        public ActionResult GetModify(Guid? Id, string culture)
        {
            if (string.IsNullOrEmpty(culture)) culture = SessionParameters.Culture;
            ViewBag.Types = new SelectList(EnumUtils.ConvertEnumToIEnumerableInLocalization<Enums.ResourceType>().Select(
                    keyValuePair =>
                        new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Enums.ResourceType>(),
                            keyValuePair.Value)), "Key", "Value");

            ViewBag.UseLayouts = EnumUtils.ConvertEnumToIEnumerable<Enums.UseLayout>().Select(
                keyValuePair =>
                    new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Enums.UseLayout>(),
                        keyValuePair.Value));
            
            return PartialView("PVModify", Id.HasValue ? WebDesignComponent.Instance.ResourceFacade.GetLanuageContent(culture,Id) : new Resource());
        }
        [RadynAuthorize]
        public ActionResult Index()
        {
            var slides = WebDesignComponent.Instance.ResourceFacade.Where(x => x.WebId == this.WebSite.Id);
            return View(slides);
        }
        public ActionResult Details(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        [RadynAuthorize]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var resource = new Radyn.WebDesign.DataStructure.Resource();

            try
            {
                this.RadynTryUpdateModel(resource);
                HttpPostedFileBase ResourceFile = null;
                if (RadynSession["ResourceFile"] != null)
                {
                    ResourceFile = (HttpPostedFileBase)RadynSession["ResourceFile"];
                    RadynSession.Remove("ResourceFile");
                }
                resource.WebId = this.WebSite.Id;
                resource.CurrentUICultureName = collection["LanguageId"];
                UpdateLayoutUse(collection, resource);
                if (WebDesignComponent.Instance.ResourceFacade.Insert(resource, ResourceFile))
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
                return View();
            }
        }
        private static void UpdateLayoutUse(FormCollection collection, Resource resource)
        {
            var str = string.Empty;
            foreach (var variable in collection.AllKeys.Where(x => x.StartsWith("SelectType-")))
            {
                var key = variable.Substring(11, variable.Length - 11);
                if (!string.IsNullOrEmpty(str)) str += ",";
                str += key;
            }

            resource.UseLayoutId = str;
        }
        [RadynAuthorize]
        public ActionResult Edit(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(Guid id, FormCollection collection)
        {

            var resource = WebDesignComponent.Instance.ResourceFacade.Get(id);
            try
            {
                this.RadynTryUpdateModel(resource, collection);
                HttpPostedFileBase resourceFile = null;
                if (RadynSession["ResourceFile"] != null)
                {
                    resourceFile = (HttpPostedFileBase)RadynSession["ResourceFile"];
                    RadynSession.Remove("ResourceFile");
                }
                resource.CurrentUICultureName = collection["LanguageId"];
                UpdateLayoutUse(collection, resource);
                if (WebDesignComponent.Instance.ResourceFacade.Update(resource, resourceFile))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    SessionParameters.CurrentWebSite = null;
                    return RedirectToAction("Index");

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.Id = id;
                return View();
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
                if (WebDesignComponent.Instance.ResourceFacade.Delete(id))
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
                ViewBag.Id = id;
                return View();
            }
        }
        public ActionResult RemoveImage()
        {
            RadynSession.Remove("ResourceFile");
            return Content("");
        }
        public ActionResult UploadImage(IEnumerable<HttpPostedFileBase> fileBase)
        {
            HttpPostedFileBase file = Request.Files["UploadResourceFile"];
            if (file != null)
            {
                if (file.InputStream != null)
                {

                    RadynSession["ResourceFile"] = file;
                }
            }
            return Content("");
        }


    }
}
