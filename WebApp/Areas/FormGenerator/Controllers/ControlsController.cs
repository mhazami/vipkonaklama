using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Radyn.FormGenerator;
using Radyn.FormGenerator.ControlFactory.Controls;
using Radyn.FormGenerator.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.FormGenerator.Controllers
{
    public class ControlsController : WebDesignBaseController
    {

        #region Controls
   
        public ActionResult GenerateDrpDownList(string Id,Guid formId,string culture)
        {
            var dropDownList = new DropDownList();
            if (!string.IsNullOrEmpty(Id))
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId,culture);
                if (form == null) return Content("false");
                var firstOrDefault =
               form.Controls.FindControl(Id);
                if (firstOrDefault != null)
                    dropDownList = (DropDownList)firstOrDefault;
            }
            ViewBag.formId = formId;
            ViewBag.culture = culture;

            return PartialView("PVDrpDownList", dropDownList);
        }
        public ActionResult GenerateCheckBoxList(string Id, Guid formId,string culture)
        {
            var dropDownList = new CheckBoxList();
            if (!string.IsNullOrEmpty(Id))
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId,culture);
                if (form == null) return Content("false");
                var firstOrDefault =
               form.Controls.FindControl(Id);
                if (firstOrDefault != null)
                    dropDownList = (CheckBoxList)firstOrDefault;
            }
            ViewBag.CheckBoxTextAlign = new SelectList(
              EnumUtils.ConvertEnumToIEnumerableInLocalization<Align>().Select(
                  keyValuePair =>
                      new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Align>(),
                          keyValuePair.Value)), "Key", "Value");
            ViewBag.RepeatDirections = new SelectList(
              EnumUtils.ConvertEnumToIEnumerableInLocalization<RepeatDirection>().Select(
                  keyValuePair =>
                      new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<RepeatDirection>(),
                          keyValuePair.Value)), "Key", "Value");
            ViewBag.formId = formId;
            ViewBag.culture = culture;

            return PartialView("PVCheckboxList", dropDownList);
        }
        public ActionResult GenerateRadioButtonList(string Id, Guid formId,string culture)
        {
            var dropDownList = new RadioButtonList();
            if (!string.IsNullOrEmpty(Id))
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId,culture);
                if (form == null) return Content("false");
                var firstOrDefault =
               form.Controls.FindControl(Id);
                if (firstOrDefault != null)
                    dropDownList = (RadioButtonList)firstOrDefault;
            }
            ViewBag.RadioTextAlign = new SelectList(
              EnumUtils.ConvertEnumToIEnumerableInLocalization<Align>().Select(
                  keyValuePair =>
                      new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Align>(),
                          keyValuePair.Value)), "Key", "Value");
            ViewBag.RepeatDirections = new SelectList(
              EnumUtils.ConvertEnumToIEnumerableInLocalization<RepeatDirection>().Select(
                  keyValuePair =>
                      new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<RepeatDirection>(),
                          keyValuePair.Value)), "Key", "Value");
            ViewBag.formId = formId;
            ViewBag.culture = culture;

            return PartialView("PVRadioButtonList", dropDownList);
        }
        public ActionResult GenerateTextBox(string Id, Guid formId,string culture)
        {
            var textbox = new TextBox();
            if (!string.IsNullOrEmpty(Id))
            {

                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId,culture);
                if (form == null) return Content("false"); var firstOrDefault =
               form.Controls.FindControl(Id);
                if (firstOrDefault != null)
                    textbox = (TextBox)firstOrDefault;
            }
            ViewBag.TextModeValues = new SelectList(
               EnumUtils.ConvertEnumToIEnumerableInLocalization<TextMode>().Select(
                   keyValuePair =>
                       new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<TextMode>(),
                           keyValuePair.Value)), "Key", "Value");
            ViewBag.formId = formId;
            ViewBag.culture = culture;

            return PartialView("PVTextBox", textbox);
        }
        public ActionResult GenerateDateTimePicker(string Id, Guid formId,string culture)
        {
            var dateTimePicker = new DateTimePicker();
            if (!string.IsNullOrEmpty(Id))
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId,culture);
                if (form == null) return Content("false");
                var firstOrDefault =
               form.Controls.FindControl(Id);
                if (firstOrDefault != null)
                    dateTimePicker = (DateTimePicker)firstOrDefault;
            }
            ViewBag.Datetypes = new SelectList(
               EnumUtils.ConvertEnumToIEnumerableInLocalization<DatetypeEnum>().Select(
                   keyValuePair =>
                       new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<DatetypeEnum>(),
                           keyValuePair.Value)), "Key", "Value");
            ViewBag.formId = formId;
            ViewBag.culture = culture;

            return PartialView("PVDateTimePicker", dateTimePicker);
        }
        public ActionResult GenerateLabel(string Id, Guid formId,string culture)
        {
            var label = new Label();
            if (!string.IsNullOrEmpty(Id))
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId,culture);
                if (form == null) return Content("false");
                var firstOrDefault =
                    form.Controls.FindControl(Id);
                if (firstOrDefault != null)
                    label = (Label)firstOrDefault;
            }
            ViewBag.formId = formId;
            ViewBag.culture = culture;

            return PartialView("PVLabel", label);
        }
        public ActionResult GenerateCheckBox(string Id, Guid formId,string culture)
        {
            var checkBox = new CheckBox();
            if (!string.IsNullOrEmpty(Id))
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId,culture);
                if (form == null) return Content("false");
                var firstOrDefault =
               form.Controls.FindControl(Id);
                if (firstOrDefault != null)
                    checkBox = (CheckBox)firstOrDefault;
            }
            ViewBag.formId = formId;
            ViewBag.culture = culture;

            return PartialView("PVCheckBox", checkBox);
        }
        public ActionResult GenerateRadioButton(string Id, Guid formId,string culture)
        {
            var radioButton = new RadioButton();
            if (!string.IsNullOrEmpty(Id))
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId,culture);
                if (form == null) return Content("false");
                var firstOrDefault =
               form.Controls.FindControl(Id);
                if (firstOrDefault != null)
                    radioButton = (RadioButton)firstOrDefault;
            }
            ViewBag.formId = formId;
            ViewBag.culture = culture;

            return PartialView("PVRadioButton", radioButton);
        }
        public ActionResult GenerateFileUploader(string Id, Guid formId,string culture)
        {
            var fileUploader = new FileUploader();
            if (!string.IsNullOrEmpty(Id))
            {
                var form = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId,culture);
                if (form == null) return Content("false");
                var firstOrDefault =
               form.Controls.FindControl(Id);
                if (firstOrDefault != null)
                    fileUploader = (FileUploader)firstOrDefault;
            }
            ViewBag.formId = formId;
            ViewBag.culture = culture;
            return PartialView("PVFileUploader", fileUploader);
        }
        #endregion
        
        public ActionResult GenerateControl(string controltype, string Id,Guid formId,string culture)
        {
            if (string.IsNullOrEmpty(controltype)) return null;
            if (controltype == typeof(Label).Name)
                return RedirectToAction("GenerateLabel", new { Id = Id, formId = formId, culture =culture});
            if (controltype == typeof(TextBox).Name)
                return RedirectToAction("GenerateTextBox", new { Id = Id, formId = formId, culture = culture });
            if (controltype == typeof(DropDownList).Name)
                return RedirectToAction("GenerateDrpDownList", new { Id = Id, formId = formId, culture = culture });
            if (controltype == typeof(CheckBox).Name)
                return RedirectToAction("GenerateCheckBox", new { Id = Id, formId = formId, culture = culture });
            if (controltype == typeof(RadioButton).Name)
                return RedirectToAction("GenerateRadioButton", new { Id = Id, formId = formId, culture = culture });
            if (controltype == typeof(RadioButtonList).Name)
                return RedirectToAction("GenerateRadioButtonList", new { Id = Id, formId = formId, culture = culture });
            if (controltype == typeof(CheckBoxList).Name)
                return RedirectToAction("GenerateCheckBoxList", new { Id = Id, formId = formId, culture = culture });
            if (controltype == typeof(FileUploader).Name)
                return RedirectToAction("GenerateFileUploader", new { Id = Id, formId = formId, culture = culture });
            if (controltype == typeof(DateTimePicker).Name)
                return RedirectToAction("GenerateDateTimePicker", new { Id = Id, formId = formId, culture = culture });
            return null;
        }

       
    }
}
