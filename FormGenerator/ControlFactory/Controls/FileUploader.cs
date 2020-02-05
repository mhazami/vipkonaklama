using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.UI;
using System.Xml.Serialization;
using Radyn.FileManager;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.ControlFactory.Validator;
using Radyn.FormGenerator.Tools;
using Radyn.Utility;

namespace Radyn.FormGenerator.ControlFactory.Controls
{
    [Serializable, XmlRoot("FileUploader"), DataContract(Name = "FileUploader")]
    public class FileUploader : Base.Control
    {
        public FileUploader()
        {
            this.Type = this.GetType().Name;
            this.Caption = ControlType.FileUploader.GetDescriptionInLocalization();

        }
        [XmlElement("Caption"), DataMember(Name = "Caption")]
        public string Caption { get; set; }

        [XmlElement("FileId"), DataMember(Name = "FileId")]
        public Guid FileId { get; set; }

        [XmlElement("IsImage"), DataMember(Name = "IsImage")]
        public bool IsImage { get; set; }

        [XmlElement("MaxSize"), DataMember(Name = "MaxSize")]
        public long MaxSize { get; set; }

        [XmlElement("AllowPrint"), DataMember(Name = "AllowPrint")]
        public bool AllowPrint { get; set; }

        public List<ValidatorControl> Validators { get; set; }

        public override void Generate()
        {
            if (FormState == FormState.ReportMode && !this.AllowPrint) return;
            base.Generate();
            this.DisplayValueType = typeof (byte[]);
            if (this.Value != null)
            {
                var value = FileManagerComponent.Instance.FileFacade.Get(this.Value.ToString().ToGuid());
                if (value != null) this.DisplayValue = value.Content;
            }
            if (this.FormState == FormState.DesgineMode)
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "font-style:" + (this.Visible ? "normal;" : "italic;"));
            else this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "display:" + (this.Visible ? "inline;" : "none;"));
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item-input");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            /////////////////////////// begin caption
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
            //////////////////////////////end caption
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "table-row-item width80percent");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "item");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            /////////////////////// begin control

            switch (this.FormState)
            {
                case FormState.DataEntryMode:
                    AddUploder();
                    AddDetails();
                    break;
                case FormState.ReportMode:
                    AddUploder();
                    AddDetails();
                    break;
                case FormState.FirstLoadMode:
                    AddUploder();
                    AddDetails();
                    break;
                case FormState.DesgineMode:
                    AddUploder();
                    break;
                case FormState.DetailsMode:
                    AddDetails();
                    break;
            }

            /////////////////////////////// end control
            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();

            this.Writer.RenderEndTag();



        }

        private void AddDetails()
        {
            if (this.Value!=null)
            {

                if (this.IsImage)
                {
                    this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "imagetag-" + this.Id);
                    this.Writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "imagetag-" + this.Id);
                    if (!string.IsNullOrEmpty(this.Height) && this.Height.ToInt() > 0)
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Height.ToString(), this.Height);
                    else this.Writer.AddAttribute(HtmlTextWriterAttribute.Height.ToString(), "70");
                    if (!string.IsNullOrEmpty(this.Width) && this.Width.ToInt() > 0)
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Width.ToString(), this.Width);
                    else
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Width.ToString(), "70");

                    this.Writer.AddAttribute(HtmlTextWriterAttribute.Src.ToString(), "/RadynFileHandler/" + this.Value);
                    this.Writer.RenderBeginTag(HtmlTextWriterTag.Img.ToString());
                    this.Writer.RenderEndTag();
                }
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "atag-" + this.Id);
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "atag-" + this.Id);
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Href.ToString(), "/RadynFileHandler/" + this.Value);
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Target.ToString(), "_blank");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.A.ToString());
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Src.ToString(), "/Content/Images/go-bottom.png");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Img.ToString());
                this.Writer.RenderEndTag();
                this.Writer.Write(Resources.FormGenerator.download);
                this.Writer.RenderEndTag();
            }
        }

        private void AddUploder()
        {
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "Uploader-" + this.Id);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "Uploader-" + this.Id);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "file");
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Onchange.ToString(), "Upload(this)");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            this.Writer.RenderEndTag();

            this.Writer.RenderBeginTag(HtmlTextWriterTag.Script.ToString());
            const string str = " function Upload(sender) {" +
                                 "var iframe = $(\"<iframe>\").hide();" +
                                 " var newForm = $(\"<FORM>\");" +
                                 " newForm.attr({ method: \"POST\", enctype: \"multipart/form-data\", action: \"/FormGenerator/UI/UploadFile\" });" +
                                 " var $this = $(sender), $clone = $this.clone();" +
                                 " $this.after($clone).appendTo($(newForm));" +
                                 " iframe.appendTo($(\"html\")).contents().find('body').html($(newForm)); newForm.submit();}";
            this.Writer.Write(str);
            this.Writer.RenderEndTag();

            if (this.MaxSize > 0)
            {
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), this.MaxSize.ToString());
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "MaxSize-" + this.Id);
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "MaxSize-" + this.Id);
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                this.Writer.RenderEndTag();

            }
            if (Value!=null)
            {
                SetBaseAttributeValues();
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), Value.ToString());
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                this.Writer.RenderEndTag();

                this.Writer.AddAttribute(HtmlTextWriterAttribute.Src.ToString(), "/Content/Images/Delete-Close.png");
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "cursor: pointer");
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "Delete-" + this.Id);
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "Delete-" + this.Id);
                var strjs = 
                    "document.getElementById('" + this.Id + "').value='';$('#Delete-" + this.Id + "').hide();$('#atag-" +
                    this.Id + "').hide();$('#imagetag-" + this.Id + "').hide();";
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Onclick.ToString(), strjs);
                    
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Img.ToString());
                this.Writer.RenderEndTag();
            }
        }
    }
}
