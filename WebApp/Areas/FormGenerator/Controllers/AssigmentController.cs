using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Radyn.FormGenerator;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Tools;
using Radyn.Security;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.FormGenerator.Controllers
{
    public class AssigmentController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetFormUrls(Guid Id)
        {
            var list = FormGeneratorComponent.Instance.FormAssigmentFacade.Select(x=>x.Url,x=>x.FormStructureId==Id);
            ViewBag.formId = Id;
            return PartialView("PartialViewFormUrls", list);
        }
         [RadynAuthorize]
        public ActionResult GetFormAssigment(Guid formId)
        {
            if (SessionParameters.UserOperation == null) return null;
            ViewBag.Oprations = new SelectList(SessionParameters.UserOperation, "Id", "Title");
            ViewBag.Status = new SelectList(
               EnumUtils.ConvertEnumToIEnumerable<FormState>().Select(
                   keyValuePair =>
                       new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<FormState>(),
                           keyValuePair.Value)), "Key", "Value");
            ViewBag.formId = formId;
            return PartialView("PartialViewFormAssigment");
        }
        [HttpPost]
        public ActionResult GetFormAssigment(FormCollection collection)
        {
            try
            {
                if (string.IsNullOrEmpty(collection["FormId"])) return Content("false");
                var list = new List<FormAssigment>();
                if (!string.IsNullOrEmpty(collection["UrlList"]))
                {
                    var strings = collection["UrlList"].Split('#');
                    foreach (var assigment in strings)
                    {
                        var formAssigment = new FormAssigment();
                        this.RadynTryUpdateModel(formAssigment);
                        formAssigment.Url = assigment;
                        list.Add(formAssigment);

                    }
                }
                if (!string.IsNullOrEmpty(collection["ManualUrl"]))
                {
                    var formAssigment = new FormAssigment();
                    this.RadynTryUpdateModel(formAssigment);
                    formAssigment.Url = collection["ManualUrl"];
                    list.Add(formAssigment);
                }
                if (FormGeneratorComponent.Instance.FormAssigmentFacade.Insert(collection["FormId"].ToGuid(), list))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return Content("true");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");

            }
        }
        public ActionResult DeletFormFromUrl(Guid Id, string url)
        {

            try
            {
                var formAssigment = FormGeneratorComponent.Instance.FormAssigmentFacade.Get(Id, url);
                if (formAssigment != null)
                {
                    if (FormGeneratorComponent.Instance.FormAssigmentFacade.Delete(Id, url))
                    {
                        ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                        return Content("true");
                    }
                    ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                    return Content("false");
                }
                return Content("false");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");

            }
        }
        public ActionResult GetForms()
        {
            var list = FormGeneratorComponent.Instance.FormStructureFacade.GetAll();
            var outlist = new List<object>();
            foreach (var listItem in list)
            {
                outlist.Add(new { Id = listItem.Id, Title = listItem.Name });
            }
            return Json(outlist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOprationMenu(Guid formId, Guid oprationId)
        {
            var addedlist = FormGeneratorComponent.Instance.FormAssigmentFacade.Select(x=>x.Url,x => x.FormStructureId == formId);
            var list = SecurityComponent.Instance.MenuFacade.SearchAddedInOpration(oprationId,"");
            var outlist = list.Where(menu => addedlist.All(x => x.ToLower() != menu.Url.ToLower())).ToList();
            return PartialView("PVSearchMenuResult", outlist);
        }

    }
}
