using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.UI;
using System.Xml.Serialization;
using Radyn.FormGenerator.ControlFactory.Base;

namespace Radyn.FormGenerator.ControlFactory.Validator
{
    [Serializable, XmlRoot("RequiredFieldValidator"), DataContract(Name = "RequiredFieldValidator")]
    public class RequiredFieldValidator : ValidatorControl
    {
        public RequiredFieldValidator()
        {
            this.Type = this.GetType().Name;
        }
        public override KeyValuePair<bool, string> ValidateControl(Dictionary<string, object> keyValuePair)
        {

            if (!this.Enable) return new KeyValuePair<bool, string>(true, string.Empty);
            if (!keyValuePair.ContainsKey(this.ControlToValidate) || keyValuePair[this.ControlToValidate] == null ||
                Equals(keyValuePair[this.ControlToValidate], string.Empty))
                return new KeyValuePair<bool, string>(false, this.ErrorMessage);
            return base.ValidateControl(keyValuePair);
        }
        public override void Generate()
        {
            base.Generate();
            this.SetValidatorBaseAttributeValues();
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Span.ToString());
            this.Writer.RenderEndTag();


        }
    }
}
