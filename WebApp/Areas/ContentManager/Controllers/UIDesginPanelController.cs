using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.ContentManager;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Definition;
using Radyn.WebApp.AppCode.Base;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.Areas.ContentManager.Tools;


namespace Radyn.WebApp.Areas.ContentManager.Controllers
{
    public class UIDesginPanelController : WebDesignBaseController<PartialLoad>
    {


        public ActionResult Index(Guid Id)
        {
            ViewBag.HideMenu = true;
            var html = ContentManagerComponent.Instance.HtmlDesginFacade.Get(Id);
            ViewBag.OperationList = new SelectList(SessionParameters.UserOperation, "Id", "Title");
            ViewBag.PartialTypes = new SelectList(EnumUtils.ConvertEnumToIEnumerable<Enums.PartialTypes>(), "Key", "Value");
            ViewBag.LanguageList = CommonComponent.Instance.LanguageFacade.GetValidList();
            return View(html);
        }
        public ActionResult GetDesginHtml(Guid Id, string culture)
        {
            if (string.IsNullOrEmpty(culture))
                culture = SessionParameters.Culture;
            var firstOrDefault = ContentManagerComponent.Instance.HtmlDesginFacade.GetLanuageContent(culture, Id);
            var title = string.Empty;
            ViewBag.Html = this.GetHtml(firstOrDefault, title, "", null, true);
            return PartialView("PVHtml");
        }
        public JsonResult AssginNameHtml(Guid Id)
        {
            var list = ContentManagerComponent.Instance.HtmlDesginFacade.ReturnCustomeAttributes(Id);
            var results = new List<object>();
            foreach (var element in list)
            {
                results.Add(new { customid = element.Key, title = element.Value });
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEditNavbar(string PartialId, string CustomId, Guid htmlId)
        {
            ViewBag.PartialId = PartialId;
            ViewBag.CustomId = CustomId;
            ViewBag.htmlId = htmlId;
            return PartialView("PVEditNavbar");
        }
        public ActionResult LookUpHtml(Enums.PartialTypes type, string partialId)
        {
            Partials partials = new Partials();

            switch (type)
            {
                case Enums.PartialTypes.ContentManager:
                    {
                        partials.Html = ContentManagerComponent.Instance.ContentFacade.GetHtml(partialId.ToInt(), SessionParameters.Culture);
                        partials.Type = Enums.PartialTypes.ContentManager;
                        break;
                    }
                case Enums.PartialTypes.Modual:
                    {
                        partials = ContentManagerComponent.Instance.PartialsFacade.Get(partialId);
                        partials.Type = Enums.PartialTypes.Modual;
                        break;
                    }
            }
            return PartialView("PVViewPartialHtml", partials);
        }
        public ActionResult GetModifyPartialLoad(string PartialId, string CustomId, Guid htmlId)
        {
            var partialLoad = ContentManagerComponent.Instance.PartialLoadFacade.Get(PartialId, CustomId, htmlId);
            this.PrepareViewBags(partialLoad, PageMode.Edit);
            return PartialView("PVModifyPartialLoad", partialLoad);
        }

        public ActionResult ModifyPartialLoad(FormCollection collection)
        {

            try
            {
                var modelKey = this.GetModelKey(collection);
                var partialLoad = ContentManagerComponent.Instance.PartialLoadFacade.Get(modelKey);
                this.RadynTryUpdateModel(partialLoad, collection);

                if (ContentManagerComponent.Instance.PartialLoadFacade.Update(partialLoad))
                {
                    return Content("true");
                }
                return Content("false");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }
        }

        public ActionResult CustomeSwap(string partialId, string customId, Guid htmlId, int getorder)
        {
            try
            {
                return
                       Content(ContentManagerComponent.Instance.PartialLoadFacade.CustomeSwap(partialId, customId, htmlId, getorder)
                           ? "true"
                           : "false");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }

        }
        public ActionResult SwapPartial(string partialId, string customId, Guid htmlId, string type)
        {
            try
            {
                return
                        Content(ContentManagerComponent.Instance.PartialLoadFacade.SwapPartials(partialId, customId, htmlId, type)
                            ? "true"
                            : "false");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }

        }
        public ActionResult GetOperationPartialList(Enums.PartialTypes PartialTypeId, Guid? OperationId, string Culture)
        {
            if (string.IsNullOrEmpty(Culture))
                Culture = SessionParameters.Culture;

            List<Partials> customeIdpartials = null;
            switch (PartialTypeId)
            {
                case Enums.PartialTypes.ContentManager:
                    if (this.WebSite != null)
                        customeIdpartials =
                            ContentManagerComponent.Instance.WebDesignHtmlFacade.GetWebDesignContent(SessionParameters.CurrentWebSite.Id, Culture);
                    else
                        customeIdpartials = ContentManagerComponent.Instance.PartialsFacade.GetContentPartials(Culture);
                    break;
                case Enums.PartialTypes.Modual:
                    if (OperationId.HasValue)
                        customeIdpartials = ContentManagerComponent.Instance.PartialsFacade.GetOperationPartials((Guid)OperationId);
                    break;

            }

            return PartialView("PVOperationPartial", customeIdpartials);
        }

        public ActionResult ClearDesign(string partialId, string customId, Guid htmlId)
        {
            try
            {
                return
                    Content(ContentManagerComponent.Instance.PartialLoadFacade.DeletePartial(partialId, customId, htmlId)
                        ? "true"
                        : "false");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }


        }
        public ActionResult SaveDesign(string partialId, string customId, Guid htmlId, int? position)
        {


            try
            {
                if(string.IsNullOrEmpty(partialId)||string.IsNullOrEmpty(customId)) return Content("false");
                return Content(ContentManagerComponent.Instance.PartialLoadFacade.Insert(partialId, customId, htmlId, position) ? "true" : "false");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);

                return Content("false");
            }

        }




    }
}
