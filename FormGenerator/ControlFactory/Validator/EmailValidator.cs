using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml.Serialization;
using Radyn.FormGenerator.ControlFactory.Base;

namespace Radyn.FormGenerator.ControlFactory.Validator
{
    [Serializable, XmlRoot("Email"), DataContract(Name = "Email")]
    public class EmailValidator : ValidatorControl
    {
        public EmailValidator()
        {
            this.Type = this.GetType().Name;
        }
        public override KeyValuePair<bool, string> ValidateControl(Dictionary<string, object> keyValuePair)
        {
            if (!this.Enable) return new KeyValuePair<bool, string>(true, string.Empty);
            if (!keyValuePair.ContainsKey(this.ControlToValidate) || keyValuePair[this.ControlToValidate] == null ||
                Equals(keyValuePair[this.ControlToValidate], string.Empty))
                return new KeyValuePair<bool, string>(true, string.Empty);
            var regex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (!regex.IsMatch((string)keyValuePair[this.ControlToValidate]))
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
