using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Radyn.FormGenerator.ControlFactory.Base
{
    [Serializable, XmlRoot("TextControl"), DataContract(Name = "TextControl")]
    public class TextControl : Control
    {

      
        [XmlElement("Text"), DataMember(Name = "Text")]
        public string Text { get; set; }
        [XmlElement("Caption"), DataMember(Name = "Caption")]
        public string Caption { get; set; }

        public override void Generate()
        {
            base.Generate();
        }
    }
}
