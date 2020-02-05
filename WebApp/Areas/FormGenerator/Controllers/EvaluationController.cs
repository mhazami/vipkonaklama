
using Radyn.FormGenerator;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.ControlFactory.Controls;
using Radyn.FormGenerator.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.FormGenerator.Controllers
{
    public class EvaluationController : WebDesignBaseController
    {
        // GET: FormGenerator/Evaluation
        public ActionResult LookupEvaluation(string Id, Guid formId,string culture)
        {
            var control = new Control();
            if (!string.IsNullOrEmpty(Id))
            {
                var controls = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId, culture);
                var firstOrDefault =
              controls.Controls.FirstOrDefault(
                    Object => ((Control)Object).Id == Id);
                if (firstOrDefault != null)
                    control = (Control)firstOrDefault;
            }
            ViewBag.FromScale = (control.GetType() == typeof(TextBox) || control.GetType() == typeof(DateTimePicker));
            var propertyInfo = control.GetType().GetProperty("Caption");
            var Caption = (string)(propertyInfo != null ? propertyInfo.GetValue(control, null) : "");
            var model = new KeyValuePair<string, string>(control.Id, Caption);
            ViewBag.Id = Id;
            ViewBag.Langid = culture;
            return View(model);
        }

        public ActionResult LookupEvaluationListControl(string Id, Guid formId, string culture)
        {
            var control = new Control();
            if (!string.IsNullOrEmpty(Id))
            {
                var controls = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId, culture);
                var firstOrDefault =
              controls.Controls.FirstOrDefault(
                    Object => ((Control)Object).Id == Id);
                if (firstOrDefault != null)
                    control = (Control)firstOrDefault;
            }
            ViewBag.FromScale = false;
            var model = new Dictionary<string, string>();
            if ((control.GetType() != typeof(DropDownList) && control.GetType() != typeof(CheckBoxList) && control.GetType() != typeof(RadioButtonList)))
                return View(model);
            var item = ((ListControl)control).EvaluationItems;
            foreach (var listItem in item)
            {
                model.Add(listItem.GetEvaluationItemId, listItem.Text);
            }
            return View(model);
        }

        public ActionResult GetEvaluationDesign(string Id, bool fromScale, string culture)
        {
            ViewBag.FromScale = fromScale;
            ViewBag.CalAHP = false;
            ViewBag.Langid = culture;
            var model = FormGeneratorComponent.Instance.FormEvaluationFacade.GetByCulture(Id,culture) ?? new FormEvaluation() { ControlId = Id };
            return PartialView("PVEvaluationDesign", model);
        }

        public ActionResult GenerateEvaluation(string Id, int? count, int? minvalue, int? maxvalue, string culture, bool newGenerate = false)
         {


            var model = FormGeneratorComponent.Instance.FormEvaluationFacade.GetByCulture(Id, culture);
            if (model == null || newGenerate)
            {
                model = new FormEvaluation()
                {
                    ControlId = Id,
                    OpinionCount = count,
                    MinScale = minvalue,
                    MaxScale = maxvalue
                };

            }
            ViewBag.newGenerate = newGenerate;
            ViewBag.Withoutahp = true;
            return PartialView("PVEvaluation", model);
        }

        public ActionResult SetEvaluation(FormCollection collection)
        {

            try
            {

                if (string.IsNullOrEmpty(collection["ControlId"]))
                {
                    ShowMessage("خطایی در ذخیره اطلاعات رخ داده است", "خطا", messageIcon: MessageIcon.Error);
                    return Content("false");
                }

                var controlsid = collection["ControlId"];
                var formEvaluation = new FormEvaluation
                {
                    ControlId = controlsid,
                    OpinionCount =
                        string.IsNullOrEmpty(collection["OpinionCount-" + controlsid])
                            ? (int?)null
                            : collection["OpinionCount-" + controlsid].ToInt(),
                    MaxScale =
                        string.IsNullOrEmpty(collection["MaxScale-" + controlsid])
                            ? (int?)null
                            : collection["MaxScale-" + controlsid].ToInt(),
                    MinScale =
                        string.IsNullOrEmpty(collection["MinScale-" + controlsid])
                            ? (int?)null
                            : collection["MinScale-" + controlsid].ToInt(),

                };
                var enumerable = collection.AllKeys.Where(x => x.StartsWith("EV-" + controlsid));
                foreach (var controlId in enumerable)
                {
                    var textBox = new TextBox
                    {
                        Id = controlId,
                        Name = controlId,
                        Caption = string.Format("نظر {0}", collection["Order-" + controlId]),
                        Order = collection["Order-" + controlId].ToInt()
                    };
                    formEvaluation.Controls.Add(textBox);
                }
                var dictionary = new Dictionary<string, double>();
                foreach (var controlId in enumerable)
                {
                    if (string.IsNullOrEmpty(controlId)) continue;
                    dictionary.Add(controlId, collection[controlId].ToFloat());
                    formEvaluation.GetFormControl.Add(controlId, collection[controlId]);
                }
//                formEvaluation.Weight = AHP.GetWeight(dictionary);
                var obj = new List<Object>();
                obj.Add(new { controlId = formEvaluation.ControlId, value = formEvaluation.Weight });
                if (FormGeneratorComponent.Instance.FormEvaluationFacade.ModifyEvaluation(formEvaluation,collection["LanguageId"]))
                {
                    ShowMessage("اطلاعات با موفقیت ثبت شد", "موفق", messageIcon: MessageIcon.Succeed);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                ShowMessage("خطایی در ذخیره اطلاعات رخ داده است", "خطا", messageIcon: MessageIcon.Error);
                return Json(obj, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exception)
            {
                ShowMessage("خطایی در ذخیره اطلاعات رخ داده است" + exception.Message, "خطا", messageIcon: MessageIcon.Error);
                return Content("false");

            }
        }

        public ActionResult SetEvaluationListControl(FormCollection collection)
        {

            try
            {

                var list = new List<FormEvaluation>();
                if (string.IsNullOrEmpty(collection["ControlId"]))
                {
                    ShowMessage("خطایی در ذخیره اطلاعات رخ داده است", "خطا", messageIcon: MessageIcon.Error);
                    return Content("false");
                }
                var dictionarymaster = new Dictionary<string, double>();
                var controlsid = collection["ControlId"].Split(',');
                foreach (var id in controlsid)
                {
                    var formEvaluation = new FormEvaluation
                    {
                        ControlId = id,
                        OpinionCount =
                            string.IsNullOrEmpty(collection["OpinionCount-" + id])
                                ? (int?)null
                                : collection["OpinionCount-" + id].ToInt(),
                        MaxScale =
                            string.IsNullOrEmpty(collection["MaxScale-" + id])
                                ? (int?)null
                                : collection["MaxScale-" + id].ToInt(),
                        MinScale =
                            string.IsNullOrEmpty(collection["MinScale-" + id])
                                ? (int?)null
                                : collection["MinScale-" + id].ToInt(),

                    };
                    var enumerable = collection.AllKeys.Where(x => x.StartsWith("EV-" + id));
                    foreach (var controlId in enumerable)
                    {
                        var textBox = new TextBox
                        {
                            Id = controlId,
                            Name = controlId,
                            Caption = string.Format("نظر {0}", collection["Order-" + controlId]),
                            Order = collection["Order-" + controlId].ToInt()
                        };
                        formEvaluation.Controls.Add(textBox);
                    }
                    var dictionary = new Dictionary<string, double>();
                    foreach (var controlId in enumerable)
                    {
                        if (string.IsNullOrEmpty(controlId)) continue;
                        dictionary.Add(controlId, collection[controlId].ToFloat());
                        formEvaluation.GetFormControl.Add(controlId, collection[controlId]);
                    }
//                    dictionarymaster.Add(id, AHP.GetWeight(dictionary));

                    list.Add(formEvaluation);
                }
//                var calculation = AHP.Calculation(dictionarymaster);
                var obj = new List<Object>();
//                foreach (var f in calculation)
//                {
//                    var firstOrDefault = list.FirstOrDefault(x => x.ControlId == f.Key);
//                    if (firstOrDefault == null) continue;
//                    firstOrDefault.Weight = Math.Round(f.Value, 6);
//                    obj.Add(new { controlId = firstOrDefault.ControlId, value = firstOrDefault.Weight });
//                }

                if (FormGeneratorComponent.Instance.FormEvaluationFacade.ModifyEvaluation(list, collection["LanguageId"]))
                {
                    ShowMessage("اطلاعات با موفقیت ثبت شد", "موفق", messageIcon: MessageIcon.Succeed);

                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                ShowMessage("خطایی در ذخیره اطلاعات رخ داده است", "خطا", messageIcon: MessageIcon.Error);
                return Json(obj, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exception)
            {
                ShowMessage("خطایی در ذخیره اطلاعات رخ داده است" + exception.Message, "خطا", messageIcon: MessageIcon.Error);
                return Content("false");

            }
        }
    }
}