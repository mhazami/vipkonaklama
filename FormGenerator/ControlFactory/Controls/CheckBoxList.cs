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
    [Serializable, XmlRoot("CheckBoxList"), DataContract(Name = "CheckBoxList")]
    public class CheckBoxList : ListControl
    {
        public CheckBoxList()
        {
            this.Type = this.GetType().Name;
            this.RepeatColumns = 1;
            this.IsMultiSelect = true;
            this.Caption = ControlType.CheckBoxList.GetDescriptionInLocalization();
        }
        [XmlElement("TextAlign"), DataMember(Name = "TextAlign")]
        public byte TextAlign { get; set; }

        [XmlElement("RepeatColumns"), DataMember(Name = "RepeatColumns")]
        public int RepeatColumns { get; set; }

        [XmlElement("RepeatDirection"), DataMember(Name = "RepeatDirection")]
        public byte RepeatDirection { get; set; }

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
            ////////////////////////// begin caption
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
            ////////end caption
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "table-row-item readonly");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            ////////////////////////// begin control
            if (this.RepeatColumns == 0) this.RepeatColumns = 1;
            var index = 0;
            SetMasterCss();
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Table.ToString());
            var ceiling = (int)Math.Ceiling((decimal)this.Items.Count / this.RepeatColumns);
            switch (this.RepeatDirection)
            {
                case (byte)FormGenerator.Tools.RepeatDirection.Horizontal:
                    {
                        for (int r = -1; r < ceiling; r++)
                        {
                            this.Writer.RenderBeginTag(HtmlTextWriterTag.Tr.ToString());
                            for (int c = 0; c < this.RepeatColumns; c++)
                            {
                                if (index < this.Items.Count)
                                {
                                    AddCheckbox(index);

                                }
                                index++;
                            }
                            this.Writer.RenderEndTag();
                        }
                        break;
                    }
                case (byte)FormGenerator.Tools.RepeatDirection.Vertical:
                    {
                        for (int i = 1; i < ceiling + 1; i++)
                        {
                            this.Writer.RenderBeginTag(HtmlTextWriterTag.Tr.ToString());
                            for (int j = 1; j < this.RepeatColumns + 1; j++)
                            {
                                if (j == 1) index = i - 1;
                                else index = i + ((j - 1) * ceiling) - 1;
                                if (index >= this.Items.Count) break;
                                AddCheckbox(index);
                                //this.Writer.RenderEndTag();

                            }
                            this.Writer.RenderEndTag();
                        }
                        break;
                    }

            }
            this.Writer.RenderEndTag();

            //////////////////////////// end control
            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();

            this.Writer.RenderEndTag();

        }

        private void AddCheckbox(int index)
        {

            switch (this.FormState)
            {
                case FormState.DataEntryMode:
                    if (this.Items[index].Enable)
                    {
                        var selected = this.Value != null && this.Value.ToString().Split(',').Any(x => x == this.Items[index].Value);
                        GetDesginCheckbox(selected, index);
                    }
                    break;
                case FormState.FirstLoadMode:
                    if (this.Items[index].Enable)
                        GetDesginCheckbox(this.Items[index].Selected, index, this.Items[index].Enable);
                    break;
                case FormState.ReportMode:
                    if (this.Items[index].Enable)
                    {
                        var selected = this.Value != null && this.Value.ToString().Split(',').Any(x => x == this.Items[index].Value);
                        GetDesginCheckbox(selected, index);
                    }
                    break;
                case FormState.DesgineMode:
                      GetDesginCheckbox(this.Items[index].Selected, index, this.Items[index].Enable);
                    break;
                case FormState.DetailsMode:
                    if (this.Items[index].Enable)
                    {
                        var selected = this.Value != null && this.Value.ToString().Split(',').Any(x => x == this.Items[index].Value);
                        GetDesginCheckbox(selected, index,false);
                    }
                    break;
            }


        }
        private void GetDesginCheckbox(bool selected, int index, bool desginEnbled = true)
        {
            if (selected)
            {
                
                this.DisplayValue += this.Items[index].Text;
            }
            if (this.TextAlign == (decimal)Align.Right)
            {
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Td.ToString());
                if (!string.IsNullOrEmpty(this.Items[index].Text))
                this.Writer.WriteEncodedText(this.Items[index].Text);
                this.Writer.RenderEndTag();
            }
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Td.ToString());
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), this.Name);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), this.Id + "-" + (index + 1));
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), this.Items[index].Value);
            if (selected)
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Checked.ToString(), "Checked");
            if (!this.Enable || !desginEnbled)
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Disabled.ToString(), "disabled");
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "checkbox");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            this.Writer.RenderEndTag();

            this.Writer.RenderEndTag();

            if (this.TextAlign == (decimal)Align.Left)
            {
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Td.ToString());
                if (!string.IsNullOrEmpty(this.Items[index].Text))
                this.Writer.WriteEncodedText(this.Items[index].Text);
                this.Writer.RenderEndTag();
            }
        }
    }
}
