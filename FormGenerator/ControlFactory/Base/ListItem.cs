using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Radyn.FormGenerator.ControlFactory.Base
{
    [Serializable, XmlRoot("ListItem"), DataContract(Name = "ListItem")]
    public class ListItem
    {
        [XmlElement("Text"), DataMember(Name = "Text")]
        public string Text { get; set; }
        [XmlElement("Value"), DataMember(Name = "Value")]
        public string Value { get; set; }
        [XmlElement("Selected"), DataMember(Name = "Selected")]
        public bool Selected { get; set; }
        [XmlElement("Enable"), DataMember(Name = "Enable")]
        public bool Enable { get; set; }
        [XmlElement("Order"), DataMember(Name = "Order")]

        public string GetEvaluationItemId { get; set; }
        public int Order { get; set; }
    }
}
