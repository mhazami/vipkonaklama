using System;
using System.Collections.Generic;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Tools;

namespace Radyn.FormGenerator.Facade.Interface
{
    public interface IGeneratorFacade
    {
        FormStructure GenerateForm(FormStructure formStructure);
        
        FormStructure GenerateForm(Guid fromId, FormState formState, string culture);

        FormStructure GenerateForm(string url, FormState formState, string culture);

        FormStructure GenerateForm(Guid formId, string objactname, string refId, FormState formState, string culture, Dictionary<string, Object> dictionary);
     
        FormStructure GenerateForm(string url, string objactname, string refId, FormState formState, string culture, Dictionary<string, Object> dictionary);

        FormStructure ViewFormData(string refId, string objactname, string culture);

        FormStructure GetUserFormReport(FormStructure form,string culture);


    
    }
}
