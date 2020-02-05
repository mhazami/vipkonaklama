using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.FormGenerator;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.ModelView;
using Radyn.FormGenerator.Tools;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;


namespace Radyn.WebApp.Areas.FormGenerator.Controllers
{
    public class DesignFormController : WebDesignBaseController
    {
      
        public ActionResult LookupFormDesign(Guid formId)
        {
            var form = FormGeneratorComponent.Instance.FormStructureFacade.Get(formId);
            if (form == null) return Content("false");
            var validList = CommonComponent.Instance.LanguageFacade.GetValidList();
            var isdefault = validList.FirstOrDefault(x=>x.IsDefault);
            ViewBag.Cultures = new SelectList(validList, "Id", "DisplayName", isdefault !=null? isdefault.Id:null);
            ViewBag.Types = EnumUtils.ConvertEnumToIEnumerableInLocalization<ControlType>();
            return View(form);
        }
        public ActionResult GeneratFormHtml(Guid formId, string culture)
        {


            var list = FormGeneratorComponent.Instance.FormStructureDesgineFacade.GenerateFormControlWithHtml(formId, culture);
            ViewBag.culture = culture;
            ViewBag.formId = formId;
            return PartialView("PartialViewFormModify", list);
        }
        public ActionResult GenrateNewControl(Guid formId, string culture, string controltype, int? order)
        {
            try
            {
                 if (string.IsNullOrEmpty(controltype)) return Content("false");
                var loadFrom =  FormGeneratorComponent.Instance.FormStructureFacade.GetTypes();
                if (loadFrom == null) return Content("false");
                var type = loadFrom.FirstOrDefault(x => x.Name == controltype);
                if (type == null) return Content("false");
                var obj = Activator.CreateInstance(type);
                this.RadynTryUpdateModel(obj);
                return
                    Json( FormGeneratorComponent.Instance.FormStructureDesgineFacade.GenrateNewControl(formId,culture,obj,order)
                        ? new { Id = ((Control)obj).Id, Result = true }
                        : new { Id = ((Control)obj).Id, Result = false }, JsonRequestBehavior.AllowGet);
               
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Json(new { Id = "", Result = false }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult CustomeSwap(Guid formId, string Id, string culture, int getorder)
        {
            return Content(! FormGeneratorComponent.Instance.FormStructureDesgineFacade.CustomeSwap(formId,culture, Id, getorder) ? "false" : "true");
        }

        #region Form

        public ActionResult LookUpControlPanel(Guid formId, string Id, string culture, string controltype)
        {
            ViewBag.culture = culture;
            ViewBag.controltype = controltype;
            ViewBag.Id = Id;
            return View(formId);
        }
        [HttpPost]
        public ActionResult LookUpControlPanel(FormCollection collection)
        {
            try
            {
                var langId = collection["LangId"];
                if (string.IsNullOrEmpty(langId)) return Content("false");
                var Id = collection["Id"];
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(collection["formId"].ToGuid(), langId);
                if (form == null) return Content("false");
                var firstOrDefault = form.Controls.FirstOrDefault(ip => ((Control)ip).Id.Equals(Id));
                if (firstOrDefault == null) return Content("false");
                this.RadynTryUpdateModel(firstOrDefault);
                return Content(FormGeneratorComponent.Instance.FormStructureDesgineFacade.UpdateControl(collection["formId"].ToGuid(), langId, Id, firstOrDefault, collection["ValidatorValue-" + Id], collection["TextItems-" + Id]) ? "true" : "false");



            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }
        }

       
     
        public ActionResult DeleteControl(Guid formId, string Id, string culture)
        {

            return Content(!FormGeneratorComponent.Instance.FormStructureDesgineFacade.DeleteControl(formId,culture, Id) ? "false" : "true");
        }
        public ActionResult SwapControl(Guid formId, string Id, string type, string culture)
        {
            return Content(!FormGeneratorComponent.Instance.FormStructureDesgineFacade.SwapControl(formId,culture, Id, type) ? "false" : "true");
        }

        #endregion

        #region ListItem

        public ActionResult GetListItemCount(string value)
        {
            var deSerializeListItem = Extentions.DeSerializeListItem(Utils.ConvertHtmlToString(StringUtils.Decrypt(value).Decompress()));
            return Json(new { CountItem = deSerializeListItem.Count, Added = deSerializeListItem.Count > 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListItemControlPanel(string jsonvalue, string value, bool multiselect = false)
        {
            ListItem item = null;
            if (!string.IsNullOrEmpty(value))
            {
                var list = Extentions.DeSerializeListItem(Utils.ConvertHtmlToString(jsonvalue.Decompress()));
                var firstOrDefault = list.FirstOrDefault(x => x.Value == value);
                if (firstOrDefault != null) item = firstOrDefault;
            }
            else item = new ListItem(){Enable = true};
            ViewBag.jsonvalue = jsonvalue;
            ViewBag.multiselect = multiselect;
            return PartialView("PartialViewListItemControls", item);
        }
        [HttpPost]
        public ActionResult GetListItemControlPanel(FormCollection collection)
        {
            try
            {
                var multiselect = collection["multiselect"].ToBool();
                var list = Extentions.DeSerializeListItem(Utils.ConvertHtmlToString(collection["jsonvalue"].Decompress()));
                var listItem = new ListItem();
                this.RadynTryUpdateModel(listItem);
                if (string.IsNullOrEmpty(listItem.Value))
                {
                    if (!string.IsNullOrEmpty(listItem.Text))
                        listItem.Value = listItem.Text;
                    else
                    {
                        ShowMessage(Resources.FormGenerator.YouCanSelectOnlyOneItemSelected, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                        return Json(new { Result = false, Value = "" }, JsonRequestBehavior.AllowGet);

                    }
                }
                var firstOrDefault = list.FirstOrDefault(ip => ip.Value.Equals(listItem.Value));
                if (firstOrDefault == null)
                {
                    if (listItem.Selected && !multiselect)
                    {
                        if (list.Any(x => x.Selected && x.Value != listItem.Value))
                        {
                            ShowMessage(Resources.FormGenerator.YouCanSelectOnlyOneItemSelected, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Error);
                            return Json(new { Result = false, Value = "" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    listItem.Order = list.Count == 0 ? 1 : list.Max(x => x.Order) + 1;
                    list.Add(listItem);
                }
                else
                {
                    this.RadynTryUpdateModel(firstOrDefault);

                    if (firstOrDefault.Selected && !multiselect)
                    {
                        if (list.Any(x => x.Selected && x.Value != firstOrDefault.Value))
                        {
                            ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Error);
                            return Content("");
                        }
                    }
                }
                return Json(new { Result = true, Value = list.SerializeListItem().Compress() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Json(new { Result = false, Value = "" }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult DeleteListItemControl(string jsonvalue, string value)
        {

            if (string.IsNullOrEmpty(value)) return Json(new { Result = true, Value = "" }, JsonRequestBehavior.AllowGet);
            var list = Extentions.DeSerializeListItem(Utils.ConvertHtmlToString(jsonvalue.Decompress()));
            var firstOrDefault = list.FirstOrDefault(x => x.Value == value);
            if (firstOrDefault != null)
            {
                list.Remove(firstOrDefault);
            }
            return Json(new { Result = true, Value = list.SerializeListItem().Compress() }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LookupListItemDesign(string value, bool multiselect = false)
        {
            ViewBag.jsonvalue =string.IsNullOrEmpty(value)?string.Empty: StringUtils.Decrypt(value);
            ViewBag.multiselect = multiselect;
            return View();
        }

        public ActionResult GeneratListItemHtml(string jsonvalue)
        {
            var list = Extentions.DeSerializeListItem(Utils.ConvertHtmlToString(jsonvalue.Decompress()));
            ViewBag.jsonvalue = jsonvalue;
            return PartialView("PartialViewListItemModify", list);
        }

        public ActionResult SwapListItemControl(string jsonvalue, string Id, string type)
        {
            var list = Extentions.DeSerializeListItem(Utils.ConvertHtmlToString(jsonvalue.Decompress()));
            var firstOrDefault = list.FirstOrDefault(organizationIp => organizationIp.Value.Equals(Id));
            if (firstOrDefault == null) return Content("");
            switch (type)
            {
                case "Up":
                    var orDefaultdown = list.OrderByDescending(x => x.Order).FirstOrDefault(control => control.Order < firstOrDefault.Order);
                    if (orDefaultdown == null) return Json(new { Result = false, Value = "" }, JsonRequestBehavior.AllowGet);
                    var orderdown = firstOrDefault.Order;
                    firstOrDefault.Order = orDefaultdown.Order;
                    orDefaultdown.Order = orderdown;
                    break;
                case "Down":
                    var orDefault = list.OrderBy(x => x.Order).FirstOrDefault(control => control.Order > firstOrDefault.Order);
                    if (orDefault == null) return Json(new { Result = false, Value = "" }, JsonRequestBehavior.AllowGet);
                    var order = (firstOrDefault).Order;
                    firstOrDefault.Order = orDefault.Order;
                    orDefault.Order = order;
                    break;
            }
            return Json(new { Result = true, Value = list.SerializeListItem().Compress() }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Vaidator

        public ActionResult GetValidatorControlCount(string value)
        {
            var list = string.IsNullOrEmpty(value) ? new List<object>() : Extentions.DeSerialize(Utils.ConvertHtmlToString(StringUtils.Decrypt(value).Decompress()));
            return Json(new { CountItem = list.Count, Added = list.Count > 0 }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetVaidatorControlPanel(string controltype, string Id, string jsonvalue, Guid formId)
        {

            ViewBag.Id = Id;
            ViewBag.controltype = controltype;
            ViewBag.formId = formId;
            ViewBag.jsonvalue = jsonvalue;
            ViewBag.ValidatorTypes =
              new SelectList(
                   EnumUtils.ConvertEnumToIEnumerableInLocalization<ValidateType>(), "Key", "Value");
            return PartialView("PartialViewValidatorControls");
        }
        [HttpPost]
        public ActionResult GetVaidatorControlPanel(FormCollection collection)
        {
            try
            {


                var formId = collection["formId"];
                if (string.IsNullOrEmpty(formId)) return Json(new { Result = false, Value = "" }, JsonRequestBehavior.AllowGet);
                var controls = new Controls();
                controls.AddRange(Extentions.DeSerialize(Utils.ConvertHtmlToString(collection["jsonvalue"].Decompress())));
                var Id = collection["Id"];
                if (!string.IsNullOrEmpty(Id))
                {
                    var firstOrDefault = controls.FindControl(Id);
                    if (firstOrDefault == null) return Json(new { Result = false, Value = "" }, JsonRequestBehavior.AllowGet);
                    this.RadynTryUpdateModel(firstOrDefault);
                    if (((ValidatorControl)firstOrDefault).Enable)
                    {
                        if (controls.Any(x =>
                                    x.GetType().Name == firstOrDefault.GetType().Name &&
                                    ((ValidatorControl)x).Id != ((ValidatorControl)firstOrDefault).Id &&
                                    ((ValidatorControl)x).Enable))
                        {
                            ShowMessage(Resources.FormGenerator.ThisValidationTypalreadyAdded,
                                Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                            return Json(new { Result = false, Value = "" }, JsonRequestBehavior.AllowGet);
                        }
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(collection["ValidatorTypeId"])) return Content("false");
                    var loadFrom = FormGeneratorComponent.Instance.FormStructureFacade.GetValidatorTypes();
                    if (loadFrom == null) return Json(new { Result = false, Value = "" }, JsonRequestBehavior.AllowGet);
                    var type = loadFrom.FirstOrDefault(x => x.Name == collection["ValidatorTypeId"]);
                    if (type == null) return Json(new { Result = false, Value = "" }, JsonRequestBehavior.AllowGet);
                    var obj = Activator.CreateInstance(type);
                    this.RadynTryUpdateModel(obj);
                    FormGeneratorComponent.Instance.FormStructureDesgineFacade.SetValidatorIdAndName(controls, obj, formId.ToGuid());
                    if (((ValidatorControl)obj).Enable)
                    {
                        if (controls.Any(x => x.GetType().Name == obj.GetType().Name && ((ValidatorControl)x).Id != ((ValidatorControl)obj).Id && ((ValidatorControl)x).Enable))
                        {
                            ShowMessage(Resources.FormGenerator.ThisValidationTypalreadyAdded, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                            return Json(new { Result = false, Value = "" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    controls.Add(obj);
                }

                return Json(new { Result = true, Value = controls.Serialize().Compress() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Json(new { Result = false, Value = "" }, JsonRequestBehavior.AllowGet);

            }
        }
        
        public ActionResult DeleteVaidatorControl(string jsonvalue, string Id)
        {
            if (string.IsNullOrEmpty(Id)) return Json(new { Result = true, Value = "" }, JsonRequestBehavior.AllowGet);
            var list = Extentions.DeSerialize(Utils.ConvertHtmlToString(jsonvalue.Decompress()));
            var firstOrDefault = list.FirstOrDefault(x => ((Control)x).Id == Id);
            if (firstOrDefault != null) list.Remove(firstOrDefault);
            return Json(new { Result = true, Value = list.Serialize().Compress() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookupValidatorDesign(string value, Guid formId)
        {
            ViewBag.jsonvalue = string.IsNullOrEmpty(value)?String.Empty: StringUtils.Decrypt(value);
            ViewBag.formId = formId;
            return View();
        }

        public ActionResult GeneratVaidatorHtml(string jsonvalue, Guid formId)
        {
            var list = Extentions.DeSerialize(Utils.ConvertHtmlToString(jsonvalue.Decompress()));
            ViewBag.jsonvalue = jsonvalue;
            ViewBag.formId = formId;
            return PartialView("PartialViewValidatorModify", list);
        }

        #endregion

    }
}
