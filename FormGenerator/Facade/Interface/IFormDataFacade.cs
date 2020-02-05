using System;
using System.Collections.Generic;
using System.Data;
using Radyn.FormGenerator.DataStructure;
using Radyn.Framework;

namespace Radyn.FormGenerator.Facade.Interface
{
    public interface IFormDataFacade : IBaseFacade<FormData>
    {
        DataTable ReportFormDataForExcel(Guid formId, string culture);
        FormData GetFormData(string refId, string objectname, string culture = null);
        FormData GetFormData(Guid formId, string refId, string objectname, string culture = null);
        DataTable ReportFormData(string url, List<object> obj, string[] refcolumnNames);
        DataTable ReportFormData(string url, List<object> obj, string objectName, string[] refcolumnNames);
        DataTable ReportFormDataFromObj(string url, object obj, string[] refcolumnNames, bool filldata=true);
        DataTable ReportFormDataFromObj(string url, object obj, string objectName, string[] refcolumnNames, bool filldata = true);
        bool ModifyFormData(FormStructure formStructure);
        IEnumerable<string> Search(FormStructure formStructure);

        FormData GetWithoutGridFormData(FormStructure form, string refId, string objname, string culture);
        List<FormData> GetGridDataSource(FormStructure form, string objactname, string refId, string gridId, string culture);
    }
}
