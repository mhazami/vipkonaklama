using Radyn.FormGenerator.DataStructure;
using Radyn.Framework;
using System.Collections.Generic;

namespace Radyn.FormGenerator.Facade.Interface
{
    public interface IFormEvaluationFacade : IBaseFacade<FormEvaluation>
    {
        FormEvaluation GenerateEvaluation(FormEvaluation formEvaluation, bool withoutahp = false, bool newgenerate = false);
        bool ModifyEvaluation(FormEvaluation formEvaluation, string culture);
        bool ModifyEvaluation(List<FormEvaluation> formEvaluation, string culture);
        double GetWeight(Dictionary<string, float> dictionary);

        FormEvaluation GetByCulture(string id, string culture);



        double GetControlWeight(object control, object value);
        
    }
}
