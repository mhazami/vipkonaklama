using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.UI;
using System.Xml.Serialization;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.Utility;

namespace Radyn.FormGenerator.ControlFactory.Validator
{
    [Serializable, XmlRoot("RengValidator"), DataContract(Name = "RengValidator")]
    public class RengValidator : ValidatorControl
    {
        public RengValidator()
        {
            this.Type = this.GetType().Name;
        }
        public override KeyValuePair<bool, string> ValidateControl(Dictionary<string, object> keyValuePair)
        {
            if (!this.Enable) return new KeyValuePair<bool, string>(true, string.Empty);
            if (!keyValuePair.ContainsKey(this.ControlToValidate) || keyValuePair[this.ControlToValidate] == null ||
                Equals(keyValuePair[this.ControlToValidate], string.Empty))
                return new KeyValuePair<bool, string>(true, string.Empty);
            var value = keyValuePair[this.ControlToValidate].ToString().ToInt();
            if (value > MaxValue && value < MinValue)
                return new KeyValuePair<bool, string>(false, this.ErrorMessage);
            return base.ValidateControl(keyValuePair);
        }
        [XmlElement("MinValue"), DataMember(Name = "MinValue")]
        public int MinValue { get; set; }

        [XmlElement("MaxValue"), DataMember(Name = "MaxValue")]
        public byte MaxValue { get; set; }
        public override void Generate()
        {
            base.Generate();
            this.SetValidatorBaseAttributeValues();
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Span.ToString());

            this.Writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Id, "MinValue-" + this.Id);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Name, "MinValue-" + this.Id);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Value, this.MinValue.ToString());
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            this.Writer.RenderEndTag();

            this.Writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Id, "MaxValue-" + this.Id);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Name, "MaxValue-" + this.Id);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Value, this.MinValue.ToString());
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            this.Writer.RenderEndTag();

            this.Writer.RenderEndTag();


        }
    }
}
