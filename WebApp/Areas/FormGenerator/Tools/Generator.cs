using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Radyn.FormGenerator;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.ControlFactory.Controls;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Facade.Interface;
using Radyn.FormGenerator.Tools;
using Radyn.Utility;
using Radyn.Web.Html;
using Radyn.Web.Mvc;
using Radyn.WebApp.AppCode.Security;
using Control = Radyn.FormGenerator.ControlFactory.Base.Control;

namespace Radyn.WebApp.Areas.FormGenerator.Tools
{


    public static class Generator
    {

        #region Form

        public static string GetControlImageUrl(string type)
        {
            var controlType = type.ToEnum<ControlType>();
            var str = "";
            switch (controlType)
            {
                case ControlType.CheckBox:
                    str = "/Areas/FormGenerator/Content/Images/ControlImages/checkbox.png";
                    break;
                case ControlType.CheckBoxList:
                    str = "/Areas/FormGenerator/Content/Images/ControlImages/checkbox-list.png";
                    break;
                case ControlType.RadioButton:
                    str = "/Areas/FormGenerator/Content/Images/ControlImages/radio.png";
                    break;
                case ControlType.RadioButtonList:
                    str = "/Areas/FormGenerator/Content/Images/ControlImages/radio-list.png";
                    break;
                case ControlType.DropDownList:
                    str = "/Areas/FormGenerator/Content/Images/ControlImages/dd-list.png";
                    break;
                case ControlType.TextBox:
                    str = "/Areas/FormGenerator/Content/Images/ControlImages/t-box.png";
                    break;
                case ControlType.FileUploader:
                    str = "/Areas/FormGenerator/Content/Images/ControlImages/file-upload.png";
                    break;
                case ControlType.DateTimePicker:
                    str = "/Areas/FormGenerator/Content/Images/ControlImages/calendar.png";
                    break;
                case ControlType.Label:
                    str = "/Areas/FormGenerator/Content/Images/ControlImages/caption.png";
                    break;

            }
            return str;
        }

        private static void GeneratorEvaluationControls(HtmlHelper helper, FormEvaluation formEvaluation)
        {
            if (formEvaluation == null) return;
            var stringWriter = new StringWriter();
            var writer = new Html32TextWriter(stringWriter);




            writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "fitper");
            writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());

            writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "FormId");
            writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "FormId");
            writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), formEvaluation.ControlId);
            writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            writer.RenderEndTag();


            foreach (Control controlvalueModel in formEvaluation.Controls)
            {
                controlvalueModel.Writer = writer;
                controlvalueModel.FormState = formEvaluation.FormState;
                if (formEvaluation.GetFormControl != null)
                    controlvalueModel.Value = formEvaluation.GetFormControl.ContainsKey(controlvalueModel.Id) ? formEvaluation.GetFormControl[controlvalueModel.Id] : null;
                controlvalueModel.Generate();


                writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "Order-" + controlvalueModel.Id);
                writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "Order-" + controlvalueModel.Id);
                writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), controlvalueModel.Order.ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                writer.RenderEndTag();
            }

            writer.RenderEndTag();
            var resourceScript = helper.ViewContext.GenerateResourceScript();
            var format = resourceScript + stringWriter;

            helper.ViewContext.Writer.Write(format);
        }
        public static void FormGenerator(this HtmlHelper helper, Guid formId, string objectname, bool isadmin = false, string refId = null, string gridId = null, Guid? gridRowdatarefId = null, FormState formState = FormState.DataEntryMode)
        {
            GeneratorControls(helper, formId, refId, objectname, isadmin, gridId, gridRowdatarefId, formState);
        }
        public static void FormEvaluation(this HtmlHelper helper, FormEvaluation formEvaluation, FormState formState = FormState.DataEntryMode, bool withoutahp = false, bool newgenerate = false)
        {
            var generateForm = FormGeneratorComponent.Instance.FormEvaluationFacade.GenerateEvaluation(formEvaluation, withoutahp, newgenerate);
            generateForm.FormState = formState;
            GeneratorEvaluationControls(helper, generateForm);

        }
        /// <summary>
        /// برای تولید فرم وابسته به URL
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="model"></param>
        /// <param name="objectname"></param>
        /// <param name="formState">وضعیت</param>
        /// <param name="url">آدرس</param>
        /// <param name="refId">آیدی</param>

        public static void FormGenerator(this HtmlHelper helper, string url, string objectname, string refId = null, FormState formState = FormState.DataEntryMode)
        {
            var generateForm = FormGeneratorComponent.Instance.GeneratorFacade.GenerateForm(url, objectname, refId, formState, SessionParameters.Culture, helper.ViewContext.TempData.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value));
            GeneratorControls(helper, generateForm);
        }

        public static void FormGenerator(this HtmlHelper helper, Guid formId, string objectname, string refId = null, FormState formState = FormState.DataEntryMode)
        {
            var generateForm = FormGeneratorComponent.Instance.GeneratorFacade.GenerateForm(formId, objectname, refId, formState, SessionParameters.Culture, helper.ViewContext.TempData.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value));
            GeneratorControls(helper, generateForm);
        }
        public static void FormSaveDataGenerator(this HtmlHelper helper, Guid formId, string objectname, string formurl = "/FormGenerator/UI/View", string formposturl = "/FormGenerator/UI/View")
        {
            var generateForm = FormGeneratorComponent.Instance.GeneratorFacade.GenerateForm(formId, objectname, null, FormState.DataEntryMode, SessionParameters.Culture, helper.ViewContext.TempData.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value));
            generateForm.PostFormDataUrl = formposturl;
            generateForm.FormUrl = formurl;
            GeneratorControls(helper, generateForm, true);
        }
        /// <summary>
        /// برای تولید فقط اطلاعات فرم
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="refId">ایدی</param>
        /// <param name="objectname"></param>
        public static void FormDataView(this HtmlHelper helper, string refId, string objectname)
        {
            var generateForm = FormGeneratorComponent.Instance.GeneratorFacade.ViewFormData(refId, objectname, SessionParameters.Culture);
            GeneratorControls(helper, generateForm);
        }
        /// <summary>
        /// برای تولید فقط Html فرم
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="formId">آیدی فرم</param>
        public static void FormHtmlGenerator(this HtmlHelper helper, Guid formId)
        {
            GeneratorControls(helper, FormGeneratorComponent.Instance.GeneratorFacade.GenerateForm(formId, FormState.FirstLoadMode, SessionParameters.Culture), false);
        }

        /// <summary>
        /// برای تولید فقط Html فرم
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static FormStructure GeneratorFormHtml(Guid formId, string culture)
        {
            return FormGeneratorComponent.Instance.GeneratorFacade.GenerateForm(formId, FormState.DesgineMode, culture);
        }

        public static void FormReportGenerator(this HtmlHelper helper, string url)
        {
            var generateForm = FormGeneratorComponent.Instance.GeneratorFacade.GenerateForm(url, FormState.ReportMode, SessionParameters.Culture);
            GeneratorControls(helper, generateForm);
        }
        public static void FormReportGenerator(this HtmlHelper helper, Guid formId)
        {
            var generateForm = FormGeneratorComponent.Instance.GeneratorFacade.GenerateForm(formId, FormState.ReportMode, SessionParameters.Culture);
            GeneratorControls(helper, generateForm);
        }
        /// <summary>
        /// تولید HTML  نهایی
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="formStructure">فرم تولید شده به همراه پارامتر ها</param>
        /// <param name="formurl"></param>
        /// <param name="postFormdataUrl"></param>
        /// 
        private static void GeneratorControls(HtmlHelper helper, Guid formId, string refId, string objname, bool isadmin = false, string gridId = null, Guid? gridRowdatarefId = null, FormState formState = FormState.DataEntryMode)
        {
            var formStructure = FormGeneratorComponent.Instance.FormStructureFacade.Get(formId);
            if (formStructure == null || !formStructure.Enable) return;
            formStructure.FormState = formState;
            var stringWriter = new StringWriter();
            var writer = new Html32TextWriter(stringWriter);

            var generatorFacade = FormGeneratorComponent.Instance.FormDataFacade;
            var generateSourceForm = FormGeneratorComponent.Instance.FormDataFacade.GetWithoutGridFormData(formStructure, refId, objname, SessionParameters.Culture);
            var data = generateSourceForm ??
                       new FormData() { Id = Guid.NewGuid(), ObjectName = objname, RefId = refId, StructureId = formStructure.Id };

            GeneratorForm(writer, generatorFacade, formStructure, data, isadmin, gridId, gridRowdatarefId);

            var resourceScript = helper.ViewContext.GenerateResourceScript();
            var format = resourceScript + stringWriter;

            helper.ViewContext.Writer.Write(format);
        }

        private static void GeneratorForm(Html32TextWriter writer, IFormDataFacade generatorFacade, FormStructure formStructure, FormData formData, bool isadmin, string gridId, Guid? gridRowdatarefId)
        {
            if (formStructure == null || formData == null) return;

            writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "fitper generator-control");
            writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());

            writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "RefId-" + formStructure.Id);
            writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "RefId-" + formStructure.Id);
            writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), formData.RefId);
            writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "DataId-" + formStructure.Id);
            writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "DataId-" + formStructure.Id);
            writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), formData.Id.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "ObjectName-" + formStructure.Id);
            writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "ObjectName-" + formStructure.Id);
            writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), formData.ObjectName);
            writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            writer.RenderEndTag();
            var list = formStructure.Controls;

            foreach (var variable in list)
            {

                var value = ((Control)variable).ShowOnlyAdmin;
                if (value && !isadmin) continue;
                if (!string.IsNullOrEmpty(((Control)variable).GridId)) continue;
                ((Control)variable).Writer = writer;
                ((Control)variable).FormState = formStructure.FormState;
                ((Control)variable).Value =
                      formData.GetFormControl.ContainsKey(((Control)variable).Id)
                          ? formData.GetFormControl[((Control)variable).Id]
                          : null;
                if (variable.GetType().Name == typeof(Grid).Name)
                {
                    var columns = formStructure.Controls.Where(x => ((Control)x).GridId == ((Grid)variable).Id).ToList();
                    if (columns.Any())
                    {
                        foreach (var column in columns)
                            ((Grid)variable).Columns.Add(column);

                        ((Grid)variable).DataSource = generatorFacade.GetGridDataSource(formStructure, formData.RefId, formData.ObjectName, ((Grid)variable).Id, SessionParameters.Culture);
                    }
                    if (gridRowdatarefId.HasValue && gridId == ((Grid)variable).Id)
                    {
                        var gridformData = ((Grid)variable).DataSource.FirstOrDefault(x => x.Id == gridRowdatarefId) ??
                                           new FormData() { Id = (Guid)gridRowdatarefId, StructureId = formStructure.Id, ObjectName = formData.ObjectName, RefId = formData.RefId };
                        ((Grid)variable).SelectedRowRefdata = gridformData;

                    }
                }
                 ((Control)variable).Generate();

            }
            if (
                list.Any(
                    x => string.IsNullOrEmpty(((Control)x).GridId) && x.GetType().BaseType != typeof(ValidatorControl)) && formStructure.FormState == FormState.DataEntryMode)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "fbfitpx");
                writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());

                writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "form-button");
                writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());

                writer.AddAttribute(HtmlTextWriterAttribute.Onclick.ToString(), "SaveData('" + formData.StructureId + "','" + formData.Id + "','" + formData.RefId + "');");
                writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "width: 100px;height:28px");
                writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), "ذخیره");
                writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "button");
                writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                writer.RenderEndTag();

                writer.RenderEndTag();
                writer.RenderEndTag();
            }



            writer.RenderEndTag();

        }

        private static void GeneratorControls(HtmlHelper helper, FormStructure formStructure, bool addcontainer = false)
        {
            if (formStructure == null) return;
            var stringWriter = new StringWriter();
            var writer = new Html32TextWriter(stringWriter);



            //DatePicker

            var firstOrDefault = formStructure.Controls.FirstOrDefault(x => x.GetType() == typeof(DateTimePicker));
            if (firstOrDefault != null)
            {
                if (helper != null)
                {
                    helper.Radyn().ResourceManager().Add("/Scripts/Radyn/calendar.js", ResourceType.JSFile);
                    helper.Radyn().ResourceManager().Add("/Content/calendar.css", ResourceType.CssFile);
                    if (((DateTimePicker)firstOrDefault).Datetype == (byte)DatetypeEnum.Gregorian)
                        helper.Radyn().ResourceManager().Add("/Scripts/Radyn/calendar-en.js", ResourceType.JSFile);
                    writer.Write(helper.ViewContext.GenerateResourceScript());
                }
            }
            //DatePicker

            writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "fit");
            writer.AddAttribute("section", formStructure.RefId);
            writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());

            writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "FormId");
            writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "FormId");
            writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), formStructure.Id.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "RefId");
            writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "RefId");
            writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), formStructure.RefId);
            writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");

            writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            writer.RenderEndTag();



            writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "ObjectName");
            writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "ObjectName");
            writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), formStructure.ObjectName);
            writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            writer.RenderEndTag();

            foreach (Control control in formStructure.Controls)
            {
                var info = control.GetType().GetProperties().FirstOrDefault(x => x.PropertyType == typeof(List<ValidatorControl>));
                if (info != null)
                {
                    var validator = formStructure.Controls.FindValidationControls(control.Id);
                    info.SetValue(control, validator, null);
                }
                control.Writer = writer;
                control.FormState = formStructure.FormState;
                if (formStructure.GetFormControl != null)
                    control.Value = formStructure.GetFormControl.ContainsKey(control.Id) ? formStructure.GetFormControl[control.Id] : null;
                control.Generate();
            }

            if ((formStructure.FormState == FormState.FirstLoadMode || formStructure.FormState == FormState.DataEntryMode) && !string.IsNullOrEmpty(formStructure.PostFormDataUrl) && !string.IsNullOrEmpty(formStructure.FormUrl))
            {
                var str = " <script type=\"text/javascript\">";
                str += "  function SaveData() {" +
                       "$.post(\"" + formStructure.PostFormDataUrl + "\", $('form[action^=\"" + formStructure.FormUrl + "\"]').serialize(), function (data) {" +
                       "if (data != \"false\") {ShowRadynMessage();}});ShowRadynMessage();}";
                str += "</script>";
                writer.Write(str);
                writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "button-div");
                writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), formStructure.Id + "-btnSave");
                writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), formStructure.Id + "-btnSave");
                writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), Resources.Common.Save);
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick.ToString(), "SaveData();");
                writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "button");
                writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                writer.RenderEndTag();

                writer.RenderEndTag();
            }
            writer.RenderEndTag();
            var resourceScript = helper.ViewContext.GenerateResourceScript();
            var format = resourceScript + stringWriter;
            if (formStructure.ContainerId.HasValue && formStructure.Container != null)
            {
                var htmltext = formStructure.Container.Html;
                var html = htmltext.Replace("{Body}", resourceScript + stringWriter);
                format = html.Replace("{title}", formStructure.Name);
            }
            //if (addcontainer&&SessionParameters.CurrentCongress!=null&& SessionParameters.CurrentCongress.Configuration!=null&&SessionParameters.CurrentCongress.Configuration.Container!=null)
            //{

            //    if (SessionParameters.CurrentCongress.Configuration.Container.Html != null)
            //    {
            //        var htmltext = SessionParameters.CurrentCongress.Configuration.Container.Html;
            //        var html = htmltext.Replace("{Body}", resourceScript + stringWriter);
            //        format = html.Replace("{title}", formStructure.Name);
            //    }
            //}
            helper.ViewContext.Writer.Write(format);
        }

        #endregion

        #region postdata
        /// <summary>
        /// دریافت اطلاعات از فرمی که در آن استفاده شده است
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static FormStructure PostForFormGenerator(this Controller controller, FormCollection collection)
        {
            var messageStack = new List<string>();
            var formStructure = new FormStructure();
            if (string.IsNullOrEmpty(collection["FormId"])) return formStructure;
            formStructure = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(collection["FormId"].ToGuid(), SessionParameters.Culture);
            if (formStructure == null) return null;
            formStructure.CurrentUICultureName = SessionParameters.Culture;
            formStructure.RefId = collection["RefId"];
            formStructure.ObjectName = collection["ObjectName"];
            var enumerable = collection.AllKeys.Where(x => x.StartsWith("FG-" + formStructure.Id + "-"));
            foreach (var controlId in enumerable)
            {
                if (string.IsNullOrEmpty(controlId)) continue;
                formStructure.GetFormControl.Add(controlId, collection[controlId]);
            }
            foreach (Control control in formStructure.Controls.Where(x => x.GetType().BaseType != typeof(ValidatorControl)))
            {
                if (control.GetType() == typeof(FileUploader))
                {
                    if (Web.HttpContext.Current.Session[control.Id] != null)
                    {
                        var file = (HttpPostedFileBase)Web.HttpContext.Current.Session[control.Id];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (!formStructure.GetFormControl.ContainsKey(control.Id))
                                formStructure.GetFormControl.Add(control.Id, file);
                            else
                                formStructure.GetFormControl[control.Id] = file;

                            var value1 = collection["MaxSize-" + control.Id];
                            if (string.IsNullOrEmpty(value1) || value1.ToLong() <= 0) continue;
                            var i = file.ContentLength / 1024;
                            if (i > value1.ToLong())
                                messageStack.Add(string.Format("حجم فایل{0} بیش از اندازه مجاز میباشد)",
                                    ((FileUploader)control).Caption));
                        }
                    }
                }
                else controller.TempData[control.Id] = collection[control.Id];
                Validation(formStructure, control.Id, ref messageStack);

            }
            var messageBody = messageStack.Aggregate("", (current, item) => current + Tag.Li(item));
            formStructure.FillErrors = messageBody;
            return formStructure;
        }
        /// <summary>
        /// اعتبار سنجی کنترل
        /// </summary>
        /// <param name="formStructure"></param>
        /// <param name="controlId"></param>
        /// <param name="messageStack"></param>
        private static void Validation(FormStructure formStructure, string controlId, ref List<string> messageStack)
        {
            var validation = formStructure.Controls.Where(x => x.GetType().BaseType == typeof(ValidatorControl) && ((ValidatorControl)x).ControlToValidate == controlId);
            foreach (ValidatorControl validatorControl in validation)
            {
                var validateControl = validatorControl.ValidateControl(formStructure.GetFormControl);
                if (!validateControl.Key) messageStack.Add(validateControl.Value);
            }
        }
        /// <summary>
        /// فقط مشاهده فرم در حالت جزئیات معمولا برای فرم های تماس با ما استفاده میشود
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string GetDetailForm(this Controller controller, FormCollection collection)
        {
            var postFormData = controller.PostForFormGenerator(collection);
            postFormData.FormState = FormState.DetailsMode;
            var generateForm = FormGeneratorComponent.Instance.GeneratorFacade.GenerateForm(postFormData);
            if(generateForm==null)return String.Empty;
            var str = "";
            foreach (var controlvalueModel in generateForm.GetFormControl)
            {
                str += "<div dir='rtl'>";
                str += " <span>" + controlvalueModel.Value + "</span><br/>";
                str += "</div>";
            }
            return str;
        }
        /// <summary>
        /// حذف session های فرم
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="formId"></param>
        public static void ClearFormGeneratorData(this Controller controller, Guid formId)
        {
            controller.TempData.Clear();
            var formStructure = FormGeneratorComponent.Instance.FormStructureFacade.GetByCulture(formId, SessionParameters.Culture);
            if (formStructure == null) return;
            foreach (
                Control control in formStructure.Controls.Where(x => x.GetType().Name == typeof(FileUploader).Name))
            {
                if (Web.HttpContext.Current.Session[control.Id] != null)
                    Web.HttpContext.Current.Session.Remove(control.Id);
            }

        }
        #endregion


    }
}