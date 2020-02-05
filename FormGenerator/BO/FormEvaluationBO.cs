using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.ControlFactory.Controls;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.ModelView;
using Radyn.FormGenerator.Tools;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.FileManager;

namespace Radyn.FormGenerator.BO
{
    internal class FormEvaluationBO : BusinessBase<FormEvaluation>
    {
        public override FormEvaluation Get(IConnectionHandler connectionHandler, params object[] keys)
        {
            
            var structure = base.Get(connectionHandler, keys);
            if (structure == null) return null;
            if (!string.IsNullOrEmpty(structure.StructureFileId))
            {
                var file = FileManagerComponent.Instance.FileFacade.Get(structure.StructureFileId.ToGuid());
                if (file != null)
                    structure.Controls = Extentions.DeSerialize(StringUtils.Unzip(file.Content));

            }
            return structure;
        }

      
        public bool DeleteByControlId(IConnectionHandler connectionHandler, string controlId)
        {
            var evaluations = this.Where(connectionHandler, x => x.ControlId.Contains(controlId));
            foreach (var formEvaluation in evaluations)
            {
                base.Delete(connectionHandler, formEvaluation);
            }
            return true;

        }

        internal FormEvaluation GeneratEvaluationHtml(IConnectionHandler connectionHandler,  FormEvaluation formEvaluation, bool withoutahp, bool newgenerate)
        {
           List<KeyValuePair<string, object>> list=new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrEmpty(formEvaluation.DataFileId))
            {
                var file = FileManagerComponent.Instance.FileFacade.Get(formEvaluation.DataFileId);
                if (file != null) list = Extentions.GetControlData(StringUtils.Unzip(file.Content)).ToList();
            }
            
            if (!withoutahp)
            {
                foreach (var controlvalueModel in list)
                {

                    if (!formEvaluation.GetFormControl.ContainsKey(controlvalueModel.Key))
                        formEvaluation.GetFormControl.Add(controlvalueModel.Key, controlvalueModel.Value);
                }
                if (newgenerate)
                {
                    formEvaluation.Controls.Clear();
                    if (formEvaluation.MinScale != null && formEvaluation.MaxScale != null)
                        formEvaluation.OpinionCount =
                            (int)(formEvaluation.OpinionCount * (formEvaluation.MaxScale - formEvaluation.MinScale));
                    for (int i = 1; i <= formEvaluation.OpinionCount; i++)
                    {
                        var textBox = new TextBox
                        {
                            Id = "EV-" + formEvaluation.ControlId + "-" + i,
                            Name = "EV-" + formEvaluation.ControlId + "-" + i,
                            Order = i,
                        };
                        textBox.Caption = string.Format("نظر {0}", textBox.Order);
                        formEvaluation.Controls.Add(textBox);
                    }

                }
            }
            else
            {
                if (!list.Any())
                {
                    var textBox = new TextBox
                    {
                        Id = "EV-" + formEvaluation.ControlId + "-" + 1,
                        Name = "EV-" + formEvaluation.ControlId + "-" + 1,
                        Order = 1,
                        Caption = "نظر ",
                    };
                    formEvaluation.Controls.Add(textBox);
                }
                else
                {
                    foreach (var controlvalueModel in list)
                    {

                        if (!formEvaluation.GetFormControl.ContainsKey(controlvalueModel.Key))
                            formEvaluation.GetFormControl.Add(controlvalueModel.Key, controlvalueModel.Value);
                    }

                }
            }


            return formEvaluation;
        }
        public FormEvaluation GetByCulture(IConnectionHandler connectionHandler, string id, string culture)
        {
            var structure = base.GetLanuageContent(connectionHandler, culture, id);
            if (structure == null) return null;
            structure.Controls = new Controls();
            if (!string.IsNullOrEmpty(structure.StructureFileId)) 
            {
                var file = FileManagerComponent.Instance.FileFacade.Get(structure.StructureFileId);
                if (file != null)
                    structure.Controls = Extentions.DeSerialize(StringUtils.Unzip(file.Content));
            }
           
            return structure;
        }
        internal bool ModifyEvaluation(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnectionHandler, FormEvaluation formEvaluation, string culture)
        {
            FileManager.Facade.Interface.IFileFacade fileTransactionalFacade = FileManagerComponent.Instance.FileTransactionalFacade(filemanagerconnectionHandler);
            var serialize = formEvaluation.Controls.Serialize();
            var formData = Extentions.GetFormData(formEvaluation.GetFormControl);
            var evaluation = this.GetByCulture(connectionHandler, formEvaluation.ControlId,culture);
            if (evaluation != null)
            {
                formEvaluation.StructureFileId = evaluation.StructureFileId;
                formEvaluation.DataFileId = evaluation.DataFileId;
                if (!this.Update(connectionHandler, formEvaluation))
                    throw new KnownException("خطایی در ذخیره فرم وجود دارد");
            }
            else
            {

                if (!this.Insert(connectionHandler, formEvaluation))
                    throw new KnownException("خطایی در ذخیره فرم وجود دارد");
            }
             if (!string.IsNullOrEmpty(formEvaluation.StructureFileId))
            {
                FileManager.DataStructure.File file = fileTransactionalFacade.Get(formEvaluation.StructureFileId.ToGuid());
                if (file != null)
                {
                    file.Content = StringUtils.Zip(serialize);
                    if (!fileTransactionalFacade.Update(file))
                    {
                        throw new Exception("خطایی در ذخیره فرم وجود دارد");
                    }
                }
              
            }
            else
            {
                FileManager.DataStructure.File file = new Radyn.FileManager.DataStructure.File
                {
                    Content = StringUtils.Zip(serialize),
                    FileName = "Structure",
                    Extension = "zip",
                    ContentType = "zip"
                };
                if (fileTransactionalFacade.InsertFile(file) == Guid.Empty)
                {
                    throw new Exception("خطایی در ذخیره فایل وجود دارد");
                }
                formEvaluation.StructureFileId = file.Id.ToString();
               
               

            }

            if (!string.IsNullOrEmpty(formEvaluation.DataFileId))
            {
                FileManager.DataStructure.File file = fileTransactionalFacade.Get(formEvaluation.DataFileId.ToGuid());
                if (file != null)
                {
                    file.Content = StringUtils.Zip(formData);
                    if (!fileTransactionalFacade.Update(file))
                        throw new Exception("خطایی در ذخیره فرم وجود دارد");
                }
            }
            else
            {
                FileManager.DataStructure.File file = new Radyn.FileManager.DataStructure.File
                {
                    Content = StringUtils.Zip(formData),
                    FileName = "StructureData",
                    Extension = "zip",
                    ContentType = "zip"
                };
                if (fileTransactionalFacade.InsertFile(file) == Guid.Empty)
                {
                    throw new Exception("خطایی در ذخیره فایل وجود دارد");
                }
                formEvaluation.DataFileId = file.Id.ToString();
               
                

            }
            formEvaluation.CurrentUICultureName = culture;
            this.Update(connectionHandler, formEvaluation);
            return true;







            

        }

       
        public void SetFormControlsWeight(IConnectionHandler connectionHandler, Controls controls)
        {
            var dictionary = new Dictionary<string, double>();
            foreach (var control in controls)
            {

                var formEvaluation = this.Get(connectionHandler, ((Control)control).Id);
                if (formEvaluation == null || formEvaluation.Weight == null) continue;
                dictionary.Add(formEvaluation.ControlId, (double)formEvaluation.Weight);

            }

//            var calculation = Evaluation.AHP.Calculation(dictionary);
//            foreach (var keyValuePair in calculation)
//            {
//                var findControl = controls.FindControl(keyValuePair.Key);
//                if (findControl == null) continue;
//                ((Control)findControl).WeightInForm = keyValuePair.Value;
//
//            }
        }

        public double GetControlWeight(IConnectionHandler connectionHandler, object control, object value)
        {
            if ((control.GetType() == typeof(DropDownList) || control.GetType() == typeof(CheckBoxList)) || control.GetType() == typeof(RadioButtonList))
            {
                var firstOrDefault = ((ListControl)control).EvaluationItems.FirstOrDefault(x => x.Value == value.ToString());
                if (firstOrDefault == null) return 0;
                var evaluation = this.Get(connectionHandler, firstOrDefault.GetEvaluationItemId);
                if (evaluation == null) return 0;
                return (evaluation.Weight ?? 0);
            }

            return value.ToString().Replace(".", "/").ToDouble();
            
        }
    }
}
