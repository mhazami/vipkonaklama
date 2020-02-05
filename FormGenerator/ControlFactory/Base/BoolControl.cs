using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Radyn.FormGenerator.ControlFactory.Base
{
    [Serializable, XmlRoot("BoolControl"), DataContract(Name = "BoolControl")]
    public class    BoolControl : Control
    {
        public BoolControl()
        {
           
        }
        [XmlElement("Checked"), DataMember(Name = "Checked")]
        public bool Checked { get; set; }
        [XmlElement("TextAlign"), DataMember(Name = "TextAlign")]
        public byte TextAlign { get; set; }
        [XmlElement("Text"), DataMember(Name = "Text")]
        public string Text { get; set; }
        [XmlElement("Caption"), DataMember(Name = "Caption")]
        public string Caption { get; set; }
        [XmlElement("AllowPrint"), DataMember(Name = "AllowPrint")]
        public bool AllowPrint { get; set; }


        public override void Generate()
        {
            this.DisplayValueType = typeof(bool);
            this.DisplayValue = this.Checked ? "بله" : "خیر";
            base.Generate();
        }
    }
}
