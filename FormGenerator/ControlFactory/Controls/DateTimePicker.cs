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
    [Serializable, XmlRoot("FileUploader"), DataContract(Name = "FileUploader")]
    public class DateTimePicker : FormGenerator.ControlFactory.Base.Control
    {

        public DateTimePicker()
        {
            this.Type = this.GetType().Name;
            this.Caption = ControlType.DateTimePicker.GetDescriptionInLocalization();

        }
        [XmlElement("Caption"), DataMember(Name = "Caption")]
        public string Caption { get; set; }

        [XmlElement("Datetype"), DataMember(Name = "Datetype")]
        public byte Datetype { get; set; }

        [XmlElement("ShowTime"), DataMember(Name = "ShowTime")]
        public bool ShowTime { get; set; }

        [XmlElement("AllowPrint"), DataMember(Name = "AllowPrint")]
        public bool AllowPrint { get; set; }


        public List<ValidatorControl> Validators { get; set; }

        public override void Generate()
        {
            if (FormState == FormState.ReportMode && !this.AllowPrint) return;
            base.Generate();
            this.DisplayValue = this.Value;
            if (FormState == FormState.DesgineMode)
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "font-style:" + (this.Visible ? "normal;" : "italic;"));
            else this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "display:" + (this.Visible ? "inline;" : "none;"));
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item-input");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            ///////////////////// begin caption
            if (!string.IsNullOrEmpty(this.Caption) && FormState != FormState.ShowInGrid)
            {
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "table-row-cap");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item");
                if (Validators != null && Validators.Any(x => x.GetType() == typeof(RequiredFieldValidator)))
                {
                    this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "color:red;float:right");
                    this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
                    this.Writer.WriteEncodedText("*");
                    this.Writer.RenderEndTag();
                }
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "float:right");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
                this.Writer.Write(String.Format("{0} {1}", this.Caption, (this.WeightInForm.HasValue ? String.Format("({0})", Math.Round((double)this.WeightInForm, 2)) : "")));
                this.Writer.RenderEndTag();

                this.Writer.RenderEndTag();
            }
            ////////////////////////end caption
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "table-row-item date-picker");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            //////////////////////////// begin control
            switch (FormState)
            {
                case FormState.DataEntryMode:
                    AddDateTimePicker();
                    this.Writer.RenderBeginTag(HtmlTextWriterTag.Script.ToString());
                    var str =
                        "Calendar.setup({inputField:'" + this.Id + "',button:'" + this.Id +
                        "',ifFormat:   '%Y/%m/%d',showsTime:" + (this.ShowTime ? "true" : "false")
                        + ((this.Datetype == (byte)DatetypeEnum.Persian) ? ",dateType:'jalali' " : "")
                        + ",timeFormat:'24',weekNumbers: false});";
                    this.Writer.Write(str);
                    this.Writer.RenderEndTag();
                    break;
                case FormState.ReportMode:
                    AddDateTimePicker();
                    this.Writer.RenderBeginTag(HtmlTextWriterTag.Script.ToString());
                    var value =
                        "Calendar.setup({inputField:'" + this.Id + "',button:'" + this.Id +
                        "',ifFormat:   '%Y/%m/%d',showsTime:" + (this.ShowTime ? "true" : "false")
                        + ((this.Datetype == (byte)DatetypeEnum.Persian) ? ",dateType:'jalali' " : "")
                        + ",timeFormat:'24',weekNumbers: false});";
                    this.Writer.Write(value);
                    this.Writer.RenderEndTag();
                    break;
                case FormState.DesgineMode:
                    AddDateTimePicker();
                    break;
                case FormState.FirstLoadMode:
                    AddDateTimePicker();
                    this.Writer.RenderBeginTag(HtmlTextWriterTag.Script.ToString());
                    var stre =
                        "Calendar.setup({inputField:'" + this.Id + "',button:'" + this.Id +
                        "',ifFormat:   '%Y/%m/%d',showsTime:" + (this.ShowTime ? "true" : "false")
                        + ((this.Datetype == (byte)DatetypeEnum.Persian) ? ",dateType:'jalali' " : "")
                        + ",timeFormat:'24',weekNumbers: false});";
                    this.Writer.Write(stre);
                    this.Writer.RenderEndTag();
                    break;
                
                case FormState.DetailsMode:
                     this.Writer.RenderBeginTag(HtmlTextWriterTag.Label.ToString());
                        if (this.Value != null)
                            this.Writer.Write(this.Value.ToString());
                        this.Writer.RenderEndTag();
                    break;
            }

            /////////////////////////// end control
            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();

            this.Writer.RenderEndTag();


        }

        private void AddDateTimePicker()
        {
          
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Span.ToString());
           if(this.Value!=null)
               this.Writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), this.Value.ToString());
            SetMasterCss();
            SetBaseAttributeValues();
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "text");
            this.Writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly.ToString(), "readonly");
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(),
                "width: 105px;float:"+((this.Datetype==(byte)DatetypeEnum.Persian)?"right":"left")+"; border: white solid 1px;font-weight:normal;");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();

            this.Writer.RenderBeginTag(HtmlTextWriterTag.Span.ToString());
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Src.ToString(), "/Content/Images/Calender.png");
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Onclick.ToString(),
                "document.getElementById('" + this.Id + "').value='';");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Img.ToString());
            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();



        }
    }
}
