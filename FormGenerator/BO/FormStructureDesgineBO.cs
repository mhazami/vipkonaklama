using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using Radyn.FileManager;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.ModelView;
using Radyn.FormGenerator.Tools;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using Control = Radyn.FormGenerator.ControlFactory.Base.Control;


namespace Radyn.FormGenerator.BO
{
    internal class FormStructureDesgineBO : BusinessBase<FormStructure>
    {
        public bool ModifyStructure(IConnectionHandler formconnectionHandler, IConnectionHandler filemanagerconnectionHandler, FormStructure structure, string culture)
        {

            var fileTransactionalFacade = FileManagerComponent.Instance.FileTransactionalFacade(filemanagerconnectionHandler);
            new FormEvaluationBO().SetFormControlsWeight(formconnectionHandler, structure.Controls);
            var serialize = structure.Controls.Serialize();
           if (!string.IsNullOrEmpty(structure.StructureFileId))
            {
                var file = fileTransactionalFacade.Get(structure.StructureFileId.ToGuid());
                if (file == null) return false;
                file.Content = StringUtils.Zip(serialize);
                if (!fileTransactionalFacade.Update(file))
                    throw new Exception("خطایی در ذخیره فرم وجود دارد");
            }
            else
            {
                var file = new Radyn.FileManager.DataStructure.File
                {
                    Content = StringUtils.Zip(serialize),
                    FileName = "Structure",
                    Extension = "zip",
                    ContentType = "zip"
                };
                if (fileTransactionalFacade.InsertFile(file) == Guid.Empty)
                    throw new Exception("خطایی در ذخیره فایل وجود دارد");
                structure.StructureFileId = file.Id.ToString();
                structure.CurrentUICultureName = culture;
                new FormStructureBO().Update(formconnectionHandler, structure);

            }
            return true;
        }

        public bool UpdateControl(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnectionHandler, Guid formId, string culture, string Id, object obj, string validation, string items)
        {
            var form = new FormStructureBO().GetByCulture(connectionHandler, formId, culture);
            var control = form.Controls.FindControl(Id);
            if (control == null) return false;
            form.Controls.Remove(control);
            this.SetValidator(validation, (Control)obj, form.Controls, formId);
            this.FillListItems(items, obj);
            form.Controls.Add(obj);
            if (!this.ModifyStructure(connectionHandler, filemanagerconnectionHandler, form, culture))
                throw new Exception("خطایی در ذخیره فرم وجود دارد");
            return true;


        }
        public bool GenrateNewControl(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnectionHandler, Guid formId, string culture, object obj, int? order)
        {

            var form = new FormStructureBO().GetByCulture(connectionHandler, formId, culture);
            this.GenerateControls(formId, form.Controls, obj, order);
            form.Controls.Add(obj);
            if (!this.ModifyStructure(connectionHandler, filemanagerconnectionHandler, form, culture))
                throw new Exception("خطایی در ذخیره فرم وجود دارد");
            return true;


        }

        public bool CustomeSwap(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnectionHandler, Guid formId, string culture, string Id, int getorder)
        {

            var form = new FormStructureBO().GetByCulture(connectionHandler, formId, culture);

            var control = (Control)form.Controls.FindControl(Id);
            if (control == null) return false;
            var enumerable = form.Controls.Where(x => x.GetType().BaseType != typeof(ValidatorControl)).Cast<Control>();
            var orDefault = enumerable.FirstOrDefault(control1 => control1.Order >= getorder);
            if (orDefault != null)
            {
                var controlorder = orDefault.Order;
                foreach (Control variable in enumerable.Where(control1 => control1.Order >= getorder))
                {
                    variable.Order++;
                    var downvalidators = form.Controls.FindValidationControls(variable.Id);
                    foreach (var validator in downvalidators)
                    {
                        validator.Order++;
                    }
                }
                control.Order = controlorder;
                var list = form.Controls.FindValidationControls(control.Id);
                foreach (var validator in list)
                {
                    validator.Order = controlorder;
                }
            }
            if (!this.ModifyStructure(connectionHandler, filemanagerconnectionHandler, form, culture))
                throw new Exception("خطایی در ذخیره فرم وجود دارد");



            return true;
        }
        public bool SwapControl(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnectionHandler, Guid formId, string culture, string Id, string type)
        {
            var form = new FormStructureBO().GetByCulture(connectionHandler, formId, culture);

            var item = (Control)form.Controls.FindControl(Id);
            if (item == null) return false;
            var enumerable = form.Controls.GetWithoutValidations();
            switch (type)
            {
                case "Up":
                    var orDefaultdown =
                        enumerable.OrderByDescending(x => ((Control)x).Order)
                            .FirstOrDefault(control => ((Control)control).Order < item.Order);
                    if (orDefaultdown == null) return false;
                    var orderdown = item.Order;
                    item.Order = ((Control)orDefaultdown).Order;
                    ((Control)orDefaultdown).Order = orderdown;
                    var upvalidators =
                        form.Controls.FindValidationControls(((Control)orDefaultdown).Id);
                    var validatorControls =
                        form.Controls.FindValidationControls(item.Id);
                    foreach (var validatorControl in upvalidators)
                        validatorControl.Order = ((Control)orDefaultdown).Order;
                    foreach (var validatorControl in validatorControls)
                        validatorControl.Order = item.Order;
                    break;
                case "Down":
                    var orDefault =
                        enumerable.FirstOrDefault(control => ((Control)control).Order > item.Order);
                    if (orDefault == null) return false;
                    var order = item.Order;
                    item.Order = ((Control)orDefault).Order;
                    ((Control)orDefault).Order = order;
                    var controls =
                        form.Controls.FindValidationControls(((Control)orDefault).Id);
                    var downvalidators =
                        form.Controls.FindValidationControls(item.Id);
                    foreach (var validator in downvalidators)
                        validator.Order = item.Order;
                    foreach (var validator in controls)
                        validator.Order = ((Control)orDefault).Order;
                    break;
            }
            if (!this.ModifyStructure(connectionHandler, filemanagerconnectionHandler, form, culture))
                throw new Exception("خطایی در ذخیره فرم وجود دارد");
            return true;


        }

        public bool DeleteControl(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnectionHandler, Guid formId, string culture, string Id)
        {
            var form = new FormStructureBO().GetByCulture(connectionHandler, formId, culture);
            var item = (Control)form.Controls.FindControl(Id);
            if (item == null) return false;
            form.Controls.Remove(item);
            new FormEvaluationBO().DeleteByControlId(connectionHandler, item.Id);
            var order = item.Order;
           
            var firstOrDefault = form.Controls.FindValidationControl(Id);
            if (firstOrDefault != null) form.Controls.Remove(firstOrDefault);
            var enumerable = form.Controls.GetWithoutValidations();
            foreach (Control variable in enumerable.Where(control => ((Control)control).Order > order))
            {
                variable.Order--;
                var downvalidators =
                    form.Controls.FindValidationControls(variable.Id);
                foreach (var validator in downvalidators)
                    validator.Order--;
            }
            if (!this.ModifyStructure(connectionHandler, filemanagerconnectionHandler, form, culture))
                throw new Exception("خطایی در ذخیره فرم وجود دارد");
            return true;


        }

        public void FillListItems(string value, object obj)
        {
            if (obj.GetType().BaseType == typeof(ListControl))
                ((ListControl)obj).Items = Extentions.DeSerializeListItem(Utils.ConvertHtmlToString(StringUtils.Decrypt(value).Decompress()));
        }
        public void SetValidator(string value, Control control, Controls controls, Guid formId)
        {
            var controls1 = new Controls();
            if (!string.IsNullOrEmpty(value))
                controls1.AddRange(Extentions.DeSerialize(Utils.ConvertHtmlToString(StringUtils.Decrypt(value).Decompress())));
            var enumerable = controls1.Where(x => ((ValidatorControl)x).ControlToValidate == control.Id);
            var allvalidations = controls.GetValidations();
            foreach (ValidatorControl validator in enumerable)
            {
                var firstOrDefault = controls.FindControl(validator.Id);
                if (firstOrDefault != null)
                {
                    controls.Remove(firstOrDefault);
                    validator.Order = control.Order;
                    controls.Add(validator);
                }
            }
            var validators = allvalidations.FindValidationControls(control.Id);
            foreach (ValidatorControl validatorControl in validators)
            {
                var firstOrDefault = controls1.FindControl(validatorControl.Id);
                if (firstOrDefault == null)
                    controls.Remove(validatorControl);
            }
            var validatorlist = controls1.Where(x => ((ValidatorControl)x).ControlToValidate == String.Empty);
            foreach (ValidatorControl validator in validatorlist)
            {
                SetValidatorIdAndName(allvalidations, validator, formId);
                validator.ControlToValidate = control.Id;
                validator.Order = control.Order;
                controls.Add(validator);
            }


        }

        public void SetValidatorIdAndName(Controls list, object obj, Guid formId)
        {
            var str = "validate-FG-" + formId + "-" + obj.GetType().Name + "-";
            var tlist = list.Where(x => ((ValidatorControl)x).Id.StartsWith(str));
            var max = 0;
            foreach (var t in tlist)
            {
                var substring = ((ValidatorControl)t).Id.Substring(str.Length, ((ValidatorControl)t).Id.Length - str.Length).ToInt();
                if (max < substring)
                    max = substring;
            }
            max++;
            ((ValidatorControl)obj).Id = str + max;
            ((ValidatorControl)obj).Name = ((ValidatorControl)obj).Id;

        }

        private void SetIdAndName(Controls list, object obj, Guid formId)
        {
            var str = "FG-" + formId + "-" + obj.GetType().Name + "-";
            var tlist = list.Where(x => ((Control)x).Id.StartsWith(str));
            var max = 0;
            foreach (var t in tlist)
            {
                var substring = ((Control)t).Id.Substring(str.Length, ((Control)t).Id.Length - str.Length).ToInt();
                if (max < substring)
                    max = substring;
            }
            max++;
            ((Control)obj).Id = str + max;
            ((Control)obj).Name = ((Control)obj).Id;
            ((Control)obj).DisplayName = obj.GetType().Name + "-" + max;

        }

        public void GenerateControls(Guid formId, Controls controllist, object obj, int? order)
        {
            SetIdAndName(controllist, obj, formId);
            var controls = controllist.GetWithoutValidations().Cast<Control>();
            if (order == null)
                ((Control)obj).Order = !controls.Any() ? 1 : controls.Max(x => x.Order) + 1;
            else
            {
                ((Control)obj).Order = (int)(order + 1);
                var orDefault = controls.FirstOrDefault(control => control.Order >= ((Control)obj).Order);
                if (orDefault != null)
                {
                    foreach (Control variable in controls.Where(control => control.Order >= ((Control)obj).Order))
                    {
                        variable.Order++;
                        var downvalidators =
                            controllist.FindValidationControls(variable.Id);
                        foreach (var validator in downvalidators)
                        {
                            validator.Order++;
                        }
                    }
                }
                var list =
                        controllist.FindValidationControls(((Control)obj).Id);
                foreach (var validator in list)
                {
                    validator.Order = ((Control)obj).Order;
                }
            }

        }
        public Dictionary<object, string> GenerateFormControlWithHtml(IConnectionHandler connectionHandler, Guid formId, string culture)
        {
            var dictionary = new Dictionary<object, string>();
            var byGridId = new FormStructureBO().GetByCulture(connectionHandler, formId, culture);
            foreach (var control in byGridId.Controls.GetWithoutValidations())
            {
                WriteControlInDictiononary(ref dictionary, (Control)control, byGridId.Controls);
            }
            return dictionary;
        }
        private static void WriteControlInDictiononary(ref Dictionary<Object, string> dictionary, Control control, Controls controls)
        {
            var stringWriter = new StringWriter();
            var writer = new Html32TextWriter(stringWriter);
            control.Writer = writer;
            control.FormState = FormState.DesgineMode;
            var info = control.GetType().GetProperties().FirstOrDefault(x => x.PropertyType == typeof(List<ValidatorControl>));
            if (info != null)
            {
                var validator = controls.FindValidationControls(control.Id);
                info.SetValue(control, validator, null);
            }
            control.Generate();
            dictionary.Add(control, stringWriter.ToString());

        }




    }
}
