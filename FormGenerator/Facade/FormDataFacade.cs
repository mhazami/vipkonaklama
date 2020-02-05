using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Radyn.FormGenerator.BO;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Facade.Interface;
using Radyn.FormGenerator.Tools;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FormGenerator.Facade
{
    internal sealed class FormDataFacade : FormGeneratorBaseFacade<FormData>, IFormDataFacade
    {
        internal FormDataFacade()
        {
        }

        internal FormDataFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }
        public FormData GetWithoutGridFormData(FormStructure form, string refId, string objname, string culture)
        {
            try
            {

                return new FormDataBO().GetWithoutGridFormData(ConnectionHandler, form, refId, objname, culture);

            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw;
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public List<FormData> GetGridDataSource(FormStructure form, string objactname, string refId, string gridId, string culture)
        {
            try
            {

                return new FormDataBO().GetGridDataSource(ConnectionHandler, form, objactname, refId, gridId, culture);

            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw;
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public FormData GetFormData(Guid formId, string refId, string objectname, string culture = null)
        {
            try
            {
                return new FormDataBO().GetFormData(this.ConnectionHandler, formId, refId, objectname, culture);
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


        public FormData GetFormData(string refId, string objectname, string culture = null)
        {
            try
            {
                return new FormDataBO().GetFormData(this.ConnectionHandler, refId, objectname,culture);
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
        public DataTable ReportFormDataForExcel(Guid formId, string culture)
        {
            try
            {
                var result = new FormDataBO().ReportFormDataForExcel(this.ConnectionHandler, formId,culture);
                return result;
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

     
        public DataTable ReportFormData(string url, List<Object> obj, string[] refcolumnName)
        {
            try
            {
                var table = new DataTable();
                if (string.IsNullOrEmpty(url)) url = HttpContext.Current.Request.Url.AbsolutePath;
                var byUrl = FormGeneratorComponent.Instance.FormAssigmentFacade.GetByUrl(url);
                if (obj != null && obj.Count > 0)
                {
                    var formStructure= byUrl != null?new FormStructureBO().Get(ConnectionHandler,byUrl.FormStructureId) :null;
                    var item = obj[0];
                    new FormDataBO().AddDataTableColumn(this.ConnectionHandler, item,
                        formStructure, ref table);
                    foreach (var rr in obj)
                    {
                        new FormDataBO().AddDataTableRows(this.ConnectionHandler, rr,
                            formStructure, item.GetType().Name, refcolumnName,
                            ref table);
                    }
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

        public DataTable ReportFormData(string url, List<object> obj, string objectName, string[] refcolumnNames)
        {
            try
            {
                var table = new DataTable();
                if (string.IsNullOrEmpty(url)) url = HttpContext.Current.Request.Url.AbsolutePath;
                var byUrl = FormGeneratorComponent.Instance.FormAssigmentFacade.GetByUrl(url);
                if (obj != null && obj.Count > 0)
                {
                    var formStructure = byUrl != null ? new FormStructureBO().Get(ConnectionHandler, byUrl.FormStructureId) : null;
                    var item = obj[0];
                    new FormDataBO().AddDataTableColumn(this.ConnectionHandler, item,
                        formStructure, ref table);
                    foreach (var rr in obj)
                    {
                        new FormDataBO().AddDataTableRows(this.ConnectionHandler, rr,
                            formStructure, objectName, refcolumnNames, ref table);
                    }
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


       

        public DataTable ReportFormDataFromObj(string url, object obj, string[] refcolumnName, bool filldata = true)
        {
            try
            {
                var table = new DataTable();

                if (string.IsNullOrEmpty(url)) url = HttpContext.Current.Request.Url.AbsolutePath;
                var byUrl = FormGeneratorComponent.Instance.FormAssigmentFacade.GetByUrl(url);
                if (obj != null)
                {
                    var formStructure = byUrl != null ? new FormStructureBO().Get(ConnectionHandler, byUrl.FormStructureId) : null;
                    new FormDataBO().AddDataTableColumn(this.ConnectionHandler, obj,
                        formStructure, ref table);
                    if (filldata)
                        new FormDataBO().AddDataTableRows(this.ConnectionHandler, obj,
                            formStructure, obj.GetType().Name, refcolumnName,
                            ref table);

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

        public DataTable ReportFormDataFromObj(string url, object obj, string objectName, string[] refcolumnNames,
            bool filldata = true)
        {
            try
            {
                var table = new DataTable();
                if (string.IsNullOrEmpty(url)) url = HttpContext.Current.Request.Url.AbsolutePath;
                var byUrl = FormGeneratorComponent.Instance.FormAssigmentFacade.GetByUrl(url);
                if (obj != null)
                {
                    var formStructure = byUrl != null ? new FormStructureBO().Get(ConnectionHandler, byUrl.FormStructureId) : null;
                    new FormDataBO().AddDataTableColumn(this.ConnectionHandler, obj,
                        formStructure, ref table);
                    if (filldata)
                        new FormDataBO().AddDataTableRows(this.ConnectionHandler, obj,
                            formStructure, objectName, refcolumnNames, ref table);

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

        public IEnumerable<string> Search(FormStructure formStructure)
        {
            try
            {
                var outlist = new List<string>();
                if (formStructure == null) return outlist;
                var list = new FormDataBO().Where(this.ConnectionHandler, x=>x.StructureId==formStructure.Id);
                var dictionary = formStructure.GetFormControl;
                foreach (var model in list)
                {
                    var deSerialize = Extentions.GetControlData(model.Data);
                    foreach (var key in dictionary.Keys)
                    {

                        if (!deSerialize.ContainsKey(key)) continue;
                        var value = dictionary[key];
                        var sourcevalue = deSerialize[key];
                        if (string.IsNullOrEmpty(sourcevalue.ToString()) || string.IsNullOrEmpty(value.ToString()))
                            continue;
                        if (key.Contains("TextBox"))
                        {
                            if (sourcevalue.ToString().ToLower().Contains(value.ToString().ToLower()))
                            {
                                if (outlist.Any(x => x == model.RefId)) continue;
                                outlist.Add(model.RefId);
                            }
                        }
                        else if (sourcevalue.ToString() == value.ToString())
                        {
                            if (outlist.Any(x => x == model.RefId)) continue;
                            outlist.Add(model.RefId);
                        }
                    }
                }
                return outlist;
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
       
        public bool ModifyFormData(FormStructure formStructure)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var formDataBo = new FormDataBO();
                var frm = formDataBo.GetFormData(this.ConnectionHandler, formStructure.Id, formStructure.RefId, formStructure.ObjectName);
                if (frm == null)
                {
                    if (formStructure.Id == Guid.Empty) return true;
                    if (!formDataBo.Insert(this.ConnectionHandler, this.FileManagerConnection, formStructure))
                        return false;
                }
                else
                {
                    if (!formDataBo.Update(this.ConnectionHandler, this.FileManagerConnection, formStructure, frm))
                        return false;
                }
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

      


    }


}
