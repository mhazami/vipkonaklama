using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Radyn.FormGenerator.DataStructure
{
    [Serializable, XmlRoot("UIControl"), DataContract(Name = "UIControl")]
    public class UIControl
    {
        [XmlElement("Name"), DataMember(Name = "Name")]
        public string Name { get; set; }
        [XmlElement("Caption"), DataMember(Name = "Caption")]
        public string Caption { get; set; }
        [XmlElement("Type"), DataMember(Name = "Type")]
        public string Type { get; set; }
    }

    public enum ControlType
    {
        [Description("TextBox")]
        TextBox,
        [Description("Label")]
        Label,
        [Description("DropDownList")]
        DropDownList,
        [Description("CheckBox")]
        CheckBox,
    }
}
