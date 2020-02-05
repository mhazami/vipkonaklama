using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Radyn.FormGenerator.ControlFactory.Base
{
    [Serializable, XmlRoot("ValidatorControl"), DataContract(Name = "ValidatorControl")]
    public class ValidatorControl:Control
    {
        public ValidatorControl()
        {
            this.Visible = false;
        }

        [XmlElement("ErrorMessage"), DataMember(Name = "ErrorMessage")]
        public string ErrorMessage { get; set; }

       
            
        [XmlElement("ControlToValidate"), DataMember(Name = "ControlToValidate")]
        public string ControlToValidate { get; set; }


        [XmlElement("Group"), DataMember(Name = "Group")]
        public string Group { get; set; }

        public virtual KeyValuePair<bool,string> ValidateControl(Dictionary<string, object> keyValuePair)
        {
            return new KeyValuePair<bool, string>(true, string.Empty);
        }
        public virtual void SetValidatorBaseAttributeValues()
        {
            this.SetBaseAttributeValues();
            this.Writer.AddAttribute("group", this.Group);
            this.Writer.AddAttribute("ErrorMessage", this.ErrorMessage);
            this.Writer.AddAttribute("ControlToValidate", this.ControlToValidate);
            this.Writer.AddAttribute("style", "display:none");
        }
       
        
    }
}
