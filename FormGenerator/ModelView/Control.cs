using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.FormGenerator.ControlFactory.Base;

namespace Radyn.FormGenerator.ModelView
{
    [Serializable]
    public class Controls : List<Object>
    {

        public Object FindControl(string Id)
        {
            return this.FirstOrDefault(x => ((Control)x).Id == Id);
        }
        public Controls GetWithoutValidations()
        {
            var controls = new Controls();
            controls.AddRange(this.Where(x => x.GetType().BaseType != typeof(ValidatorControl)).ToList());
            return controls;
        }
        public Controls GetValidations()
        {
            var controls = new Controls();
            controls.AddRange(this.Where(x => x.GetType().BaseType == typeof(ValidatorControl)).ToList());
            return controls;
        }
        public Object FindValidationControl(string Id)
        {
            return this.FirstOrDefault(
                x =>
                    x.GetType().BaseType == typeof(ValidatorControl) &&
                    ((ValidatorControl)x).ControlToValidate == Id);
        }
        public List<ValidatorControl> FindValidationControls(string Id)
        {
            return this.Where(
                        x =>
                            x.GetType().BaseType == typeof(ValidatorControl) &&
                            ((ValidatorControl)x).ControlToValidate == Id)
                        .Cast<ValidatorControl>().ToList();
        }
    }
}
