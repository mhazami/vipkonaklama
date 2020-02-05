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
    [Serializable, XmlRoot("DropDownList"), DataContract(Name = "DropDownList")]
    public class DropDownList : ListControl
    {
        public DropDownList()
        {
            this.Type = this.GetType().Name;
            this.IsMultiSelect = false;
            this.Caption = ControlType.DropDownList.GetDescriptionInLocalization();

        }

        public List<ValidatorControl> Validators { get; set; }


        public override void Generate()
        {
            if (FormState == FormState.ReportMode && !this.AllowPrint) return;
            base.Generate();

            if (this.FormState == FormState.DesgineMode)
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "font-style:" + (this.Visible ? "normal;" : "italic;"));
            else this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "display:" + (this.Visible ? "inline;" : "none;"));
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item-input");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            //////////////// begin caption
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
            ////////////////////// end caption
         
            /////////////////////////////// begin control




            this.SetMasterCss();
            this.SetBaseAttributeValues();
            if (this.FormState == FormState.DetailsMode)
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Disabled.ToString(), "Disabled");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Select.ToString());
            this.Items.Insert(0, new ListItem() { Enable = true, Text = "", Value = null });
            foreach (var listItem in this.Items.Where(x => x.Enable))
            {
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), listItem.Value);

                switch (this.FormState)
                {
                    case FormState.DataEntryMode:
                        if (this.Value != null && this.Value.ToString() == listItem.Value)
                        {
                            this.Writer.AddAttribute(HtmlTextWriterAttribute.Selected.ToString(), "Selected");
                            this.DisplayValue = listItem.Text;
                        }
                        break;
                    case FormState.DetailsMode:
                        if (this.Value != null && this.Value.ToString() == listItem.Value)
                        {
                            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "table-c-fit ");
                            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
                            this.Writer.RenderBeginTag(HtmlTextWriterTag.Span.ToString());
                            this.Writer.AddAttribute(HtmlTextWriterAttribute.Selected.ToString(), "Selected");
                            this.DisplayValue = listItem.Text;
                            this.Writer.RenderEndTag();
                        }
                        break;
                    case FormState.ReportMode:
                        if (this.Value != null && this.Value.ToString() == listItem.Value)
                        {
                            this.Writer.AddAttribute(HtmlTextWriterAttribute.Selected.ToString(), "Selected");
                            this.DisplayValue = listItem.Text;
                        }
                        break;
                    case FormState.FirstLoadMode:
                        if (listItem.Selected)
                        {
                            this.Writer.AddAttribute(HtmlTextWriterAttribute.Selected.ToString(), "Selected");
                            this.DisplayValue = listItem.Text;
                        }
                        if (!listItem.Enable)
                            this.Writer.AddAttribute(HtmlTextWriterAttribute.Disabled.ToString(), "disabled");
                        break;
                    case FormState.DesgineMode:
                        if (listItem.Selected)
                        {
                            this.Writer.AddAttribute(HtmlTextWriterAttribute.Selected.ToString(), "Selected");
                            this.DisplayValue = listItem.Text;
                        }
                        if (!listItem.Enable)
                            this.Writer.AddAttribute(HtmlTextWriterAttribute.Disabled.ToString(), "disabled");
                        break;

                }

                this.Writer.RenderBeginTag(HtmlTextWriterTag.Option.ToString());
                this.Writer.WriteEncodedText(listItem.Text);
                this.Writer.RenderEndTag();
            }
            this.Writer.RenderEndTag();



            ///////////////////////////// end control
        

            this.Writer.RenderEndTag();





        }
    }
}
