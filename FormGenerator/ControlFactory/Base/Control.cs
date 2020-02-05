using System;
using System.Runtime.Serialization;
using System.Web.UI;
using System.Xml.Serialization;
using Radyn.FormGenerator.Tools;
using Radyn.Utility;

namespace Radyn.FormGenerator.ControlFactory.Base
{
    [Serializable, XmlRoot("Control"), DataContract(Name = "Control")]
    public class Control
    {
        public Control()
        {
            this.Enable = true;
            this.Visible = true;
        }

        [XmlElement("Id"), DataMember(Name = "Id")]
        public string Id { get; set; }
        [XmlElement("Name"), DataMember(Name = "Name")]
        public string Name { get; set; }
        [XmlElement("Enable"), DataMember(Name = "Enable")]
        public bool Enable { get; set; }
        [XmlElement("Visible"), DataMember(Name = "Visible")]
        public bool Visible { get; set; }
        [XmlElement("Height"), DataMember(Name = "Height")]
        public string Height { get; set; }
        [XmlElement("Width"), DataMember(Name = "Width")]
        public string Width { get; set; }
        [XmlElement("CssClass"), DataMember(Name = "CssClass")]
        public string CssClass { get; set; }
        [XmlElement("Type"), DataMember(Name = "Type")]

       

        public string Type { get; set; }
        [XmlElement("Description"), DataMember(Name = "Description")]
        public string Description { get; set; }
        [XmlElement("Order"), DataMember(Name = "Order")]
        public int Order { get; set; }

        [XmlElement("DisplayName"), DataMember(Name = "DisplayName")]
        public string DisplayName { get; set; }

        [XmlElement("ShowInGrid"), DataMember(Name = "ShowInGrid")]
        public bool ShowInGrid { get; set; }

        [XmlElement("GridId"), DataMember(Name = "GridId")]
        public string GridId { get; set; }

        [XmlElement("ShowOnlyAdmin"), DataMember(Name = "ShowOnlyAdmin")]
        public bool ShowOnlyAdmin { get; set; }

        [XmlElement("WeightInForm"), DataMember(Name = "WeightInForm")]
        public double? WeightInForm { get; set; }

        public Html32TextWriter Writer { get; set; }
        public Object Value{ get; set; }
        public Object DisplayValue{ get; set; }
        public Type DisplayValueType{ get; set; }
        public FormState FormState{ get; set; }

        public string GetCaption()
        {
            var displayName = this.DisplayName;
            if (this.GetType().GetProperty("Caption") != null)
                displayName = this.GetType().GetProperty("Caption").GetValue(this, null) != null
                    ? this.GetType().GetProperty("Caption").GetValue(this, null).ToString()
                    : this.DisplayName;
            return displayName;
        }

        internal virtual void SetBaseAttributeValues()
        {
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), this.Id);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), this.Name);
            if (!this.Enable)
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Disabled.ToString(), "disabled");
        }

        internal void SetMasterCss()
        {

            if (!String.IsNullOrEmpty(this.Description))
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Title.ToString(), this.Description);
            if (!String.IsNullOrEmpty(this.CssClass))
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), this.CssClass);
            var str = "display:" + (this.Visible ? "inline;" : "none;");
            if (!string.IsNullOrEmpty(this.Height))
            {
                if (!this.Height.Contains("%"))
                {
                    if (this.Height.ToInt() > 0)
                        str += "height:" + this.Height + "px;";
                }
                else str += "height:" + this.Height + ";";
            }
            if (!string.IsNullOrEmpty(this.Width))
            {
                if (!this.Width.Contains("%"))
                {
                    if (this.Width.ToInt() > 0)
                        str += "width:" + this.Width + "px;";
                }
                else str += "width:" + this.Width + ";";
            }
            if (!string.IsNullOrEmpty(str))
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), str);

        }

        internal virtual void Prepare()
        {
            this.SetBaseAttributeValues();
            this.SetMasterCss();
        }
       

        public virtual void Generate()
        {
            this.DisplayValueType = typeof(string);
            
        }
        
    }
}
