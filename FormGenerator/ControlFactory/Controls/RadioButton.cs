using System;
using System.Runtime.Serialization;
using System.Web.UI;
using System.Xml.Serialization;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.Tools;
using Radyn.Utility;

namespace Radyn.FormGenerator.ControlFactory.Controls
{
    [Serializable, XmlRoot("RadioButton"), DataContract(Name = "RadioButton")]
    public class RadioButton : BoolControl
    {
        public RadioButton()
        {
            this.Type = this.GetType().Name;
            this.Caption = ControlType.RadioButton.GetDescriptionInLocalization();

        }
        [XmlElement("GroupName"), DataMember(Name = "GroupName")]
        public string GroupName { get; set; }

        public override void Generate()
        {
            if (FormState == FormState.ReportMode && !this.AllowPrint) return;
            base.Generate();
            if (this.FormState == FormState.DesgineMode)
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "font-style:" + (this.Visible ? "normal;" : "italic;"));
            else this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "display:" + (this.Visible ? "inline;" : "none;"));
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item-input");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            ///////////////////////////////// begin caption
            if (!string.IsNullOrEmpty(this.Caption) && FormState != FormState.ShowInGrid)
            {
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "table-row-cap");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
                this.Writer.Write(String.Format("{0} {1}", this.Caption, (this.WeightInForm.HasValue ? String.Format("({0})", Math.Round((double)this.WeightInForm, 2)) : "")));
                this.Writer.RenderEndTag();
                this.Writer.RenderEndTag();
            }
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "table-row-item readonly");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            /////////////////// end caprion
            /////////////////// begin control
            SetMasterCss();
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Table.ToString());
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Tr.ToString());
            if (this.TextAlign == (byte)Align.Right)
            {
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Td.ToString());
                if(!string.IsNullOrEmpty(this.Text))
                this.Writer.WriteEncodedText(this.Text);
                this.Writer.RenderEndTag();
            }
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Td.ToString());
            AddRadio();
            this.Writer.RenderEndTag();
            if (this.TextAlign == (byte)Align.Left)
            {
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Td.ToString());
                if (!string.IsNullOrEmpty(this.Text))
                this.Writer.WriteEncodedText(this.Text);
                this.Writer.RenderEndTag();
            }

            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();

            //////////////// end control
            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();

            this.Writer.RenderEndTag();




        }

        private void AddRadio()
        {
            switch (this.FormState)
            {
                case FormState.DataEntryMode:
                    if (this.Value != null && this.Value.ToString() == "on")
                    {
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Checked.ToString(), "checked");
                        this.DisplayValue = true;
                    }
                    else
                        this.DisplayValue = false;
                    break;
                case FormState.DesgineMode:
                    if (this.Checked)
                    {
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Checked.ToString(), "checked");
                        this.DisplayValue = true;
                    }
                    else
                        this.DisplayValue = false;
                    break;
                case FormState.FirstLoadMode:
                    if (this.Checked)
                    {
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Checked.ToString(), "checked");
                        this.DisplayValue = true;
                    }
                    else
                        this.DisplayValue = false;
                    break;
                case FormState.ReportMode:
                    if (this.Value != null && this.Value.ToString() == "on")
                    {
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Checked.ToString(), "checked");
                        this.DisplayValue = true;
                    }
                    else
                        this.DisplayValue = false;
                    break;
                case FormState.DetailsMode:
                    if (this.Value != null && this.Value.ToString() == "on")
                    {
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Checked.ToString(), "checked");
                        this.DisplayValue = true;
                    }
                    else
                        this.DisplayValue = false;
                    this.Writer.AddAttribute(HtmlTextWriterAttribute.Disabled.ToString(), "disabled");
                    break;
            }
            SetBaseAttributeValues();
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "radio");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            this.Writer.RenderEndTag();
        }
    }
}
