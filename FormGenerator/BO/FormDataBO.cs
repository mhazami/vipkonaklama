using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Radyn.FileManager;
using Radyn.FormGenerator.ControlFactory.Controls;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Tools;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using Label = Radyn.FormGenerator.ControlFactory.Controls.Label;
using Control = Radyn.FormGenerator.ControlFactory.Base.Control;

namespace Radyn.FormGenerator.BO
{
    internal class FormDataBO : BusinessBase<FormData>
    {
        public void AddDataTableColumn(IConnectionHandler connectionHandler, object obj, FormStructure formStructure, ref DataTable table)
        {
            foreach (var property in obj.GetType().GetProperties())
            {
                if (property.Name == "Dynamic" || property.PropertyType == typeof(ObjectState)) continue;
                var column = new DataColumn();
                var propertyType = property.PropertyType;
                if (propertyType.IsGenericType &&
                    propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyType = propertyType.GetGenericArguments()[0];
                    column.AllowDBNull = true;
                }
                column.DataType = propertyType;
                column.ColumnName = property.Name;
                if (!table.Columns.Contains(property.Name))
                    table.Columns.Add(column);
            }
            if (formStructure == null) return;
            new FormStructureBO().DeSerializeForm(ref formStructure);
            var generatorBo = new GeneratorBO();
            formStructure.FormState = FormState.ReportMode;
            var generateForm = generatorBo.GenerateForm(connectionHandler,formStructure);
            if (generateForm == null) return;
            foreach (Control control in generateForm.Controls)
            {
                if (control == null || control.DisplayName == null) continue;
                if (!table.Columns.Contains(control.DisplayName))
                    table.Columns.Add(control.DisplayName, control.DisplayValue != null ? control.DisplayValueType : typeof(string));
            }
        }
        public void AddDataTableRows(IConnectionHandler connectionHandler, object obj, FormStructure formStructure, string objectName, string[] refcolumnNames, ref DataTable table)
        {
            var row = table.NewRow();
            foreach (var property in obj.GetType().GetProperties())
            {
                if (property.Name == "Dynamic" || property.PropertyType == typeof(ObjectState)) continue;
                var propertyType = property.PropertyType;
                if (propertyType.IsGenericType &&
                    propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    propertyType = propertyType.GetGenericArguments()[0];
                if (property.GetValue(obj, null) != null)
                    row[property.Name] = System.Convert.ChangeType(property.GetValue(obj, null), propertyType);
            }
            if (formStructure != null)
            {
                new FormStructureBO().DeSerializeForm(ref formStructure);
                if (refcolumnNames != null)
                {
                    var str = "";
                    foreach (var refcolumnName in refcolumnNames)
                    {
                        var value = obj.GetType().GetProperty(refcolumnName).GetValue(obj, null);
                        if (value == null) continue;
                        if (!string.IsNullOrEmpty(str)) str += ",";
                        str += value;
                    }

                    formStructure.RefId = str;
                }
                formStructure.ObjectName = objectName;
                formStructure.FormState = FormState.ReportMode;
                var generateForm = new GeneratorBO().GenerateForm(connectionHandler, formStructure);
                if (generateForm != null)
                {
                    var stringWriter = new StringWriter();
                    var writer = new Html32TextWriter(stringWriter);
                    foreach (var control in generateForm.Controls)
                    {
                        if (control == null) continue;
                        var key = (Control)control;
                        key.Writer = writer;
                        if (key.DisplayName == null) continue;
                        row[key.DisplayName] = key.DisplayValue != null ? System.Convert.ChangeType(key.DisplayValue, key.DisplayValueType) : null;
                        if (row[key.DisplayName] != null && generateForm.GetFormControl.Count > 0 && string.IsNullOrEmpty(row[key.DisplayName].ToString()))
                            key.Value = generateForm.GetFormControl.FirstOrDefault(c => c.Key == key.Id).Value;
                        key.Generate();
                        row[key.DisplayName] = key.DisplayValue;


                    }
                }


            }
            table.Rows.Add(row);
        }



        public DataTable ReportFormDataForExcel(IConnectionHandler connectionHandler, Guid formId, string culture)
        {
            try
            {
                var table = new DataTable();

                var generatorBo = new GeneratorBO();
                var byCulture = new FormStructureBO().Get(connectionHandler, formId);
                byCulture.FormState = FormState.ReportMode;
                var formStructure = generatorBo.GenerateForm(connectionHandler,byCulture);
                if (formStructure == null) return table;
                byCulture = formStructure;
                foreach (Control control in byCulture.Controls)
                {
                    if (control == null) continue;
                    if (control.GetType() == typeof(Label) || control.GetType() == typeof(FileUploader)) continue;
                    var columnName = control.GetCaption();
                    if (!table.Columns.Contains(columnName))
                        table.Columns.Add(columnName, control.DisplayValue != null ? control.DisplayValueType : typeof(string));
                }
                if (culture == "fa-IR")
                {
                    var ordinal = table.Columns.Count - 1;

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        table.Columns[0].SetOrdinal(ordinal);
                        ordinal--;
                    }
                }
                var @where = Where(connectionHandler, x => x.StructureId == formId);
                foreach (var formData in @where)
                {
                    var row = table.NewRow();
                    var stringWriter = new StringWriter();
                    var writer = new Html32TextWriter(stringWriter);
                    var list = Extentions.GetControlData(formData.Data);
                    foreach (Control control in formStructure.Controls)
                    {
                        if (control.GetType() == typeof(Label) || control.GetType() == typeof(FileUploader)) continue;
                        control.Writer = writer;
                        control.FormState = FormState.DetailsMode;
                        if (list != null)
                            control.Value = list.ContainsKey(control.Id) ? list[control.Id] : null;
                        control.Generate();
                        var columnName = control.GetCaption();
                        row[columnName] = control.DisplayValue != null ? control.DisplayValue.ToString() : string.Empty;


                    }
                    table.Rows.Add(row);
                }


                return table;
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
        public override bool Insert(IConnectionHandler connectionHandler, FormData obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            obj.SaveDate = DateTime.Now;
            obj.RefId = obj.RefId.ToLower();
            return base.Insert(connectionHandler, obj);
        }

        public FormData GetFormData(IConnectionHandler connectionHandler, string refId, string objectname, string culture = null)
        {
            if (string.IsNullOrEmpty(refId) || string.IsNullOrEmpty(objectname)) return null;
            var firstOrDefault = this.FirstOrDefault(connectionHandler,
                x => x.RefId == refId & x.ObjectName.ToLower() == objectname.ToLower());
            if (firstOrDefault == null || string.IsNullOrEmpty(culture)) return firstOrDefault;
            this.GetLanuageContent(connectionHandler, culture, firstOrDefault);
            return firstOrDefault;
        }

        public FormData GetFormData(IConnectionHandler connectionHandler, Guid formId, string refId, string objectname, string culture = null)
        {
            if (string.IsNullOrEmpty(refId) || string.IsNullOrEmpty(objectname)) return null;
            var firstOrDefault = this.FirstOrDefault(connectionHandler,
                x => x.RefId == refId & x.ObjectName.ToLower() == objectname.ToLower() && x.StructureId == formId);
            if (firstOrDefault == null || string.IsNullOrEmpty(culture)) return firstOrDefault;
            this.GetLanuageContent(connectionHandler, culture, firstOrDefault);
            return firstOrDefault;
        }

        public bool Insert(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnectionHandler, FormStructure formPostModel)
        {
            var fileTransactionalFacade = FileManagerComponent.Instance.FileTransactionalFacade(filemanagerconnectionHandler);
            foreach (var source in formPostModel.GetFormControl.Where(x => x.Key.Contains(typeof(FileUpload).Name)).ToList())
            {
                if (source.Value != null && !Equals(source.Value, "") && source.Value.GetType() == typeof(HttpPostedFileWrapper))
                    formPostModel.GetFormControl[source.Key] = fileTransactionalFacade.Insert((HttpPostedFileBase)source.Value).ToString();
            }
            var formData = new FormData
            {
                RefId = formPostModel.RefId,
                StructureId = formPostModel.Id,
                ObjectName = formPostModel.ObjectName,
                Data = Extentions.GetFormData(formPostModel.GetFormControl)
            };
            formData.CurrentUICultureName = formPostModel.CurrentUICultureName;
            if (!this.Insert(connectionHandler, formData))
                throw new Exception("خطایی در ذخیره اطلاعات فرم وجود دارد");
            return true;
        }
        public bool Update(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnectionHandler, FormStructure formPostModel, FormData frm)
        {
            var fileTransactionalFacade = FileManagerComponent.Instance.FileTransactionalFacade(filemanagerconnectionHandler);
            var controlData = Extentions.GetControlData(frm.Data);
            foreach (var source in formPostModel.GetFormControl.Where(x => x.Key.Contains(typeof(FileUpload).Name)).ToList())
            {
                var keyValuePair = new KeyValuePair<string, object>();
                if (controlData != null)
                    keyValuePair = controlData.FirstOrDefault(x => x.Key == source.Key);
                if (source.Value != null && !Equals(source.Value, ""))
                {
                    if (source.Value.GetType() == typeof(HttpPostedFileWrapper))
                    {
                        if (keyValuePair.Value != null && !Equals(keyValuePair.Value, ""))
                            fileTransactionalFacade.Delete(((string)keyValuePair.Value).ToGuid());
                        formPostModel.GetFormControl[source.Key] = fileTransactionalFacade.Insert((HttpPostedFileBase)source.Value).ToString();
                    }

                }
                else
                {
                    if (keyValuePair.Value != null && !Equals(keyValuePair.Value, ""))
                        fileTransactionalFacade.Delete(((string)keyValuePair.Value).ToGuid());
                }

            }
            frm.CurrentUICultureName = formPostModel.CurrentUICultureName;
            frm.StructureId = formPostModel.Id;
            frm.Data = Extentions.GetFormData(formPostModel.GetFormControl);
            if (!this.Update(connectionHandler, frm))
                throw new Exception("خطایی در ویرایش اطلاعات فرم وجود دارد");

            return true;
        }
        public FormData GetWithoutGridFormData(IConnectionHandler connectionHandler, FormStructure formStructure, string refId, string objname, string culture)
        {
            if (!string.IsNullOrEmpty(refId))
            {

                var controls = formStructure.Controls.Where(x => x.GetType().Name != typeof(ControlFactory.Controls.Grid).Name && string.IsNullOrEmpty(((Control)x).GridId)).ToList();
                var formDatas = new FormDataBO().Where(connectionHandler, x => x.RefId.ToLower() == refId.ToLower() && x.ObjectName.ToLower() == objname.ToLower() && x.StructureId == formStructure.Id);
                if (formDatas == null || !formDatas.Any()) return null;
                new FormDataBO().GetLanuageContent(connectionHandler, culture, formDatas);
                FormData setformData = null;
                foreach (var formData in formDatas)
                {


                    var list = Extentions.GetControlData(formData.Data);
                    foreach (var control in controls)
                    {
                        if (!list.ContainsKey(((Control)control).Id)) continue;
                        if (!formData.GetFormControl.ContainsKey(((Control)control).Id))
                            formData.GetFormControl.Add(((Control)control).Id, list[((Control)control).Id]);
                        setformData = formData;
                    }
                    if (setformData != null) return setformData;

                }
            }
            return null;
        }

        public List<FormData> GetGridDataSource(IConnectionHandler connectionHandler, FormStructure formStructure, string objactname, string refId, string gridId, string culture)
        {
            var datas = new List<FormData>();

            if (!string.IsNullOrEmpty(refId))
            {

                var controls = formStructure.Controls.Where(x => x.GetType().Name != typeof(Grid).Name && ((Control)x).GridId == gridId).ToList();
                var formDatas = new FormDataBO().Where(connectionHandler,
                    x =>
                        x.RefId.ToLower() == refId.ToLower() && x.ObjectName.ToLower() == objactname.ToLower() &&
                        x.StructureId == formStructure.Id);
                if (formDatas == null || !formDatas.Any()) return null;
                new FormDataBO().GetLanuageContent(connectionHandler, culture, formDatas);
                foreach (var formData in formDatas)
                {

                    var list = Extentions.GetControlData(formData.Data);
                    bool add = false;
                    foreach (var control in controls)
                    {
                        if (!list.ContainsKey(((Control)control).Id)) continue;
                        add = true;
                        if (!formData.GetFormControl.ContainsKey(((Control)control).Id))
                            formData.GetFormControl.Add(((Control)control).Id, list[((Control)control).Id]);
                    }
                    if (add)
                        datas.Add(formData);

                }
            }
            return datas;
        }
    }
}
