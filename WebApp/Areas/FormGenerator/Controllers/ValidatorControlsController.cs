using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Radyn.FormGenerator;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.ControlFactory.Validator;
using Radyn.FormGenerator.ModelView;
using Radyn.FormGenerator.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.FormGenerator.Controllers
{
    public class ValidatorControlsController : WebDesignBaseController
    {

        #region BaseControls
        public ActionResult BaseValidatorGenerateControl(string Id, string jsonvalue)
        {
            var textControl = new Control() { Enable = true, Visible = false };
            if (!string.IsNullOrEmpty(Id))
            {
                var list = Extentions.DeSerialize(Utils.ConvertHtmlToString(jsonvalue.Decompress()));
                var firstOrDefault =
               list.FindControl(Id);
                if (firstOrDefault != null)
                    textControl = (Control)firstOrDefault;
            }
            return PartialView("PVBaseValidatorGenerateControl", textControl);
        }
        public ActionResult GetValidatorControl(string Id, string jsonvalue)
        {
            var control = new ValidatorControl();
            if (!string.IsNullOrEmpty(Id))
            {
                var list = Extentions.DeSerialize(Utils.ConvertHtmlToString(jsonvalue.Decompress()));
                var firstOrDefault = list.FindControl(Id);
                if (firstOrDefault != null)
                    control = (ValidatorControl)firstOrDefault;
            }
            ViewBag.Id = Id;
            ViewBag.jsonvalue = jsonvalue;
            return PartialView("PVValidatorControl", control);
        }

        public ActionResult GetRequiredFieldValidator(string Id, string jsonvalue)
        {
            var requiredFieldValidator = new RequiredFieldValidator();
            if (!string.IsNullOrEmpty(Id))
            {
                var list = Extentions.DeSerialize(Utils.ConvertHtmlToString(jsonvalue.Decompress()));
                var firstOrDefault = list.FindControl(Id);
                if (firstOrDefault != null)
                    requiredFieldValidator = (RequiredFieldValidator)firstOrDefault;
            }
            ViewBag.Id = Id;
            ViewBag.jsonvalue = jsonvalue;
            return PartialView("PVRequiredFieldValidator", requiredFieldValidator);
        }
        public ActionResult GetEmailValidator(string Id, string jsonvalue)
        {
            var requiredFieldValidator = new EmailValidator();
            if (!string.IsNullOrEmpty(Id))
            {
                var list = Extentions.DeSerialize(Utils.ConvertHtmlToString(jsonvalue.Decompress()));
                var firstOrDefault = list.FindControl(Id);
                if (firstOrDefault != null)
                    requiredFieldValidator = (EmailValidator)firstOrDefault;
            }
            ViewBag.Id = Id;
            ViewBag.jsonvalue = jsonvalue;
           
            return PartialView("PVEmailValidator", requiredFieldValidator);
        }
        public ActionResult GetCompareValidator(string Id, string jsonvalue)
        {
            var requiredFieldValidator = new CompareValidator();
            if (!string.IsNullOrEmpty(Id))
            {
                var list = Extentions.DeSerialize(Utils.ConvertHtmlToString(jsonvalue.Decompress()));
                var firstOrDefault = list.FindControl(Id);
                if (firstOrDefault != null)
                    requiredFieldValidator = (CompareValidator)firstOrDefault;
            }
            ViewBag.Id = Id;
            ViewBag.jsonvalue = jsonvalue;
            ViewBag.ValidationDataTypes = new SelectList(
              EnumUtils.ConvertEnumToIEnumerableInLocalization<ValidationDataType>().Select(
                  keyValuePair =>
                      new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<ValidationDataType>(),
                          keyValuePair.Value)), "Key", "Value");
            ViewBag.ValidationOperators = new SelectList(
              EnumUtils.ConvertEnumToIEnumerableInLocalization<ValidationOperator>().Select(
                  keyValuePair =>
                      new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<ValidationOperator>(),
                          keyValuePair.Value)), "Key", "Value");
            return PartialView("PVCompareValidator", requiredFieldValidator);
        }

        public ActionResult GenerateValidator(string Id, Guid formId, string culture)
        {
            var objects = new Controls();
            if (!string.IsNullOrEmpty(Id))
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId, culture);
                if (form == null) return Content("false");
                objects.AddRange(form.Controls.FindValidationControls(Id));
            }
            ViewBag.Id = Id;
            ViewBag.formId = formId;
            ViewBag.ValidatorItems = objects.Count > 0 ? StringUtils.Encrypt(objects.Serialize().Compress()) : null;
            return PartialView("PVValidator");
        }
        public ActionResult GenerateValidatorControl(string controltype, string Id, string jsonvalue)
        {
            if (string.IsNullOrEmpty(controltype)) return null;
            if (controltype == typeof(RequiredFieldValidator).Name)
                return RedirectToAction("GetRequiredFieldValidator", new { Id = Id, jsonvalue = jsonvalue });
            if (controltype == typeof(EmailValidator).Name)
                return RedirectToAction("GetEmailValidator", new { Id = Id, jsonvalue = jsonvalue });
            if (controltype == typeof(CompareValidator).Name)
                return RedirectToAction("GetCompareValidator", new { Id = Id, jsonvalue = jsonvalue });
            return null;
        }

        #endregion




    }
}
