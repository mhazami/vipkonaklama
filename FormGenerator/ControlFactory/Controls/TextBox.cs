using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.UI;
using System.Xml.Serialization;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.ControlFactory.Validator;
using Radyn.FormGenerator.Tools;
using Radyn.Utility;

namespace Radyn.FormGenerator.ControlFactory.Controls
{
    [Serializable, XmlRoot("TextBox"), DataContract(Name = "TextBox")]
    public class TextBox : TextControl
    {
        public TextBox()
        {
            this.Type = this.GetType().Name;
            this.TextMode = (byte)Tools.TextMode.SingleLine;
            this.Caption = ControlType.TextBox.GetDescriptionInLocalization();

        }
        [XmlElement("TextMode"), DataMember(Name = "TextMode")]
        public byte TextMode { get; set; }

        [XmlElement("AllowPrint"), DataMember(Name = "AllowPrint")]
        public bool AllowPrint { get; set; }

        public List<ValidatorControl> Validators { get; set; }

        public override void Generate()
        {
            if (FormState == FormState.ReportMode && !this.AllowPrint) return;
            base.Generate();
            if (this.FormState == FormState.DesgineMode)
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "font-style:" + (this.Visible ? "normal;" : "italic;"));
            else this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "display:" + (this.Visible ? "inline;" : "none;"));
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item-input");
            //this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item-input");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            /////////////////////begin caption
            if (!string.IsNullOrEmpty(this.Caption) && FormState != FormState.ShowInGrid)
            {
            
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Label.ToString());
                if (Validators != null && Validators.Any(x => x.GetType() == typeof(RequiredFieldValidator)))
                {
                    this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "color:red;float:right");
                    this.Writer.RenderBeginTag(HtmlTextWriterTag.Small.ToString());
                    this.Writer.WriteEncodedText("*");
                    this.Writer.RenderEndTag();
                }


             
                this.Writer.Write(String.Format("{0} {1}", this.Caption, (this.WeightInForm.HasValue ? String.Format("({0})", Math.Round((double)this.WeightInForm, 2)) : "")));
                this.Writer.RenderEndTag();

            }
            //////////////////////////////////end caption
            ///////////////////////////////// begin Control
            if (this.Value != null)
                this.DisplayValue = this.Value;
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            switch (this.FormState)
            {
                case FormState.DataEntryMode:
                    GetDesgintextbox(this.Value);
                    break;
                case FormState.DesgineMode:
                    GetDesgintextbox(this.Text);
                    break;
                case FormState.FirstLoadMode:
                    GetDesgintextbox(this.Text);
                    break;
                case FormState.ReportMode:
                    GetDesgintextbox(this.Value);
                    break;
                case FormState.DetailsMode:
                    switch (this.TextMode)
                    {

                        case (byte)FormGenerator.Tools.TextMode.MultiLine:
                            {
                                AddLabel(this.Value);
                                break;
                            }
                        case (byte)FormGenerator.Tools.TextMode.Password:
                            {
                                AddLabel("***********");
                                break;
                            }
                        case (byte)FormGenerator.Tools.TextMode.SingleLine:
                            {
                                AddLabel(this.Value);
                                break;
                            }
                    }
                    break;
            }

            ///////////////////// end control

            this.Writer.RenderEndTag();



        }

        private void AddLabel(Object obj)
        {
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Label.ToString());
            if (obj != null)
                this.Writer.Write(obj.ToString());
            this.Writer.RenderEndTag();
        }

        private void GetDesgintextbox(object value)
        {
            SetMasterCss();
            SetBaseAttributeValues();

            switch (this.TextMode)
            {
                case (byte)FormGenerator.Tools.TextMode.MultiLine:
                    {
                        this.Writer.RenderBeginTag(HtmlTextWriterTag.Textarea.ToString());
                        if (value != null)
                            this.Writer.Write(value);
                        this.Writer.RenderEndTag();
                        break;
                    }
                case (byte)FormGenerator.Tools.TextMode.Password:
                    {
                        if (value != null)
                            this.Writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), value.ToString());
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "password");
                        this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                        this.Writer.RenderEndTag();
                        break;
                    }
                case (byte)FormGenerator.Tools.TextMode.SingleLine:
                    {
                        if (value != null)
                            this.Writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), value.ToString());
                        this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                        this.Writer.RenderEndTag();
                        break;
                    }
            }
        }
    }
}
