using System;
using System.Collections.Generic;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.ModelView;
using Radyn.Framework;

namespace Radyn.FormGenerator.Facade.Interface
{
    public interface IFormStructureDesgineFacade : IBaseFacade<FormStructure>
    {
        bool UpdateControl(Guid formId, string culture, string Id, object obj, string validation, string items);
        bool GenrateNewControl(Guid formId, string culture, object obj, int? order);
        bool SwapControl(Guid formId,string culture, string Id, string type);
        bool CustomeSwap(Guid formId, string culture, string Id, int getorder);
        bool DeleteControl(Guid formId,string culture, string Id );
        void SetValidatorIdAndName(Controls list, object obj, Guid formId);
        void SetValidator(string value, Control control, Controls controls, Guid formId);
        void FillListItems(string value, object obj);

        bool ModifyStructure(FormStructure obj, string culture);
        Dictionary<Object, string> GenerateFormControlWithHtml(Guid formId, string culture);
       
    }
}
