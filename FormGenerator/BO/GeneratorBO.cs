using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Tools;
using Radyn.Framework.DbHelper;

namespace Radyn.FormGenerator.BO
{
    public class GeneratorBO
    {
        internal FormStructure GenerateForm(FormStructure formStructure, string culture = null)
        {
            if (formStructure == null || !formStructure.Enable) return null;
            if (!string.IsNullOrEmpty(formStructure.RefId) && formStructure.GetFormControl != null)
            {
                var firstOrDefault = FormGeneratorComponent.Instance.FormDataFacade.GetFormData(formStructure.Id,formStructure.RefId,formStructure.ObjectName, culture);
                if (firstOrDefault != null)
                {
                    if (firstOrDefault.StructureId == formStructure.Id)
                    {
                        var list = Extentions.GetControlData(firstOrDefault.Data);
                        foreach (var controlvalueModel in list)
                        {
                           
                            if (!formStructure.GetFormControl.ContainsKey(controlvalueModel.Key))
                                formStructure.GetFormControl.Add(controlvalueModel.Key, controlvalueModel.Value);
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(formStructure.RefId) && formStructure.FormState == FormState.DataEntryMode)
                formStructure.FormState = FormState.FirstLoadMode;
            return formStructure;


        }
        internal FormStructure GenerateForm(IConnectionHandler connectionHandler,FormStructure formStructure, string culture = null)
        {
            if (formStructure == null || !formStructure.Enable) return null;
            if (!string.IsNullOrEmpty(formStructure.RefId) && formStructure.GetFormControl != null)
            {
                var firstOrDefault = new FormDataBO().GetFormData(connectionHandler,formStructure.Id, formStructure.RefId, formStructure.ObjectName, culture);
                if (firstOrDefault != null)
                {
                    if (firstOrDefault.StructureId == formStructure.Id)
                    {
                        var list = Extentions.GetControlData(firstOrDefault.Data);
                        foreach (var controlvalueModel in list)
                        {

                            if (!formStructure.GetFormControl.ContainsKey(controlvalueModel.Key))
                                formStructure.GetFormControl.Add(controlvalueModel.Key, controlvalueModel.Value);
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(formStructure.RefId) && formStructure.FormState == FormState.DataEntryMode)
                formStructure.FormState = FormState.FirstLoadMode;
            return formStructure;


        }

    }
}
