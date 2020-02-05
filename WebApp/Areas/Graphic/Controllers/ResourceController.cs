using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.Graphic;
using Radyn.Graphic.Definition;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;



namespace Radyn.WebApp.Areas.Graphic.Controllers
{
    public class ResourceController : WebDesignBaseController
    {

        public ActionResult GetDetails(Guid Id)
        {
            var resource = GraphicComponent.Instance.ResourceFacade.Get(Id);
            return PartialView("PVDetails", resource);
        }
        public ActionResult GetModify(Guid? Id)
        {
            ViewBag.Types = new SelectList(EnumUtils.ConvertEnumToIEnumerableInLocalization<Enums.ResourceType>().Select(
                    keyValuePair =>
                        new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Enums.ResourceType>(),
                            keyValuePair.Value)), "Key", "Value");
            ViewBag.LanguageList = new SelectList(CommonComponent.Instance.LanguageFacade.GetValidList(), "Id", "DisplayName");
            return PartialView("PVModify", Id.HasValue ? GraphicComponent.Instance.ResourceFacade.Get(Id) : new Radyn.Graphic.DataStructure.Resource());
        }
        [RadynAuthorize]
        public ActionResult Index(Guid themeId)
        {
            var slides = GraphicComponent.Instance.ResourceFacade.GetAll();
            ViewBag.ThemeId = themeId;
            return View(slides);
        }
        public ActionResult Details(short id)
        {
            ViewBag.Id = id;
            return View();
        }

        [RadynAuthorize]
        public ActionResult Create(Guid themeId)
        {
            ViewBag.ThemeId = themeId;
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var resource = new Radyn.Graphic.DataStructure.Resource();

            try
            {
                this.RadynTryUpdateModel(resource);
                HttpPostedFileBase ResourceFile = null;
                if (RadynSession["ResourceFile"] != null)
                {
                    ResourceFile = (HttpPostedFileBase)RadynSession["ResourceFile"];
                    RadynSession.Remove("ResourceFile");
                }
              
                if (GraphicComponent.Instance.ResourceFacade.Insert(resource, ResourceFile))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { themeId = resource.ThemeId });
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index",new { themeId =resource.ThemeId});
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View();
            }
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

            var resource = GraphicComponent.Instance.ResourceFacade.Get(id);
            try
            {
                this.RadynTryUpdateModel(resource, collection);
               HttpPostedFileBase resourceFile = null;
                if (RadynSession["ResourceFile"] != null)
                {
                    resourceFile = (HttpPostedFileBase)RadynSession["ResourceFile"];
                    RadynSession.Remove("ResourceFile");
                }
                if (GraphicComponent.Instance.ResourceFacade.Update(resource, resourceFile))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);

                    return RedirectToAction("Index", new { themeId = resource.ThemeId });

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { themeId = resource.ThemeId });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
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
            var resource = GraphicComponent.Instance.ResourceFacade.Get(id);
            try
            {
                if (GraphicComponent.Instance.ResourceFacade.Delete(id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { themeId = resource.ThemeId });
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { themeId = resource.ThemeId });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = id;
                return View();
            }
        }
       
       
    }
}
