using System;
using System.Collections.Generic;
using Radyn.FormGenerator.DataStructure;
using Radyn.Framework;

namespace Radyn.FormGenerator.Facade.Interface
{
    public interface IFormStructureFacade : IBaseFacade<FormStructure>
    {
        IEnumerable<Type> GetTypes();
        FormStructure GetByCulture(Guid id, string culture);
        IEnumerable<Type> GetValidatorTypes();
        Dictionary<string, double> GetFormControlsWeightInform(FormStructure formStructure);

        Dictionary<string, double> GetFormControlsWeight(FormStructure formStructure);
        
    }
}
