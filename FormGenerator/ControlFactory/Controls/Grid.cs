using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.UI;
using System.Xml.Serialization;
using Radyn.FormGenerator.ControlFactory.Base;
using Radyn.FormGenerator.DataStructure;
using Radyn.FormGenerator.Tools;
using Control = Radyn.FormGenerator.ControlFactory.Base.Control;

namespace Radyn.FormGenerator.ControlFactory.Controls
{
    [Serializable, XmlRoot("Grid"), DataContract(Name = "Grid")]
    public class Grid : TabularControl
    {
        public Grid()
        {
            this.Type = this.GetType().Name;



        }
      

        [XmlElement("AllowPrint"), DataMember(Name = "AllowPrint")]
        public bool AllowPrint { get; set; }

        [XmlElement("Caption"), DataMember(Name = "Caption")]
        public string Caption { get; set; }


        public FormData SelectedRowRefdata { get; set; }

        public List<ValidatorControl> Validators { get; set; }

        private ModelView.Controls _columns;
        public ModelView.Controls Columns
        {
            get
            {
                if (this._columns == null)
                    this._columns = new ModelView.Controls();
                return _columns;
            }
            set { _columns = value; }
        }

        private List<FormData> _datasource;
        public List<FormData> DataSource
        {
            get
            {
                if (this._datasource == null)
                    this._datasource = new List<FormData>();
                return _datasource;
            }
            set { _datasource = value; }
        }
        public override void Generate()
        {
            if (FormState == FormState.ReportMode && !this.AllowPrint) return;
            base.Generate();

            if (this.FormState == FormState.DesgineMode)
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "font-style:" + (this.Visible ? "normal;" : "italic;"));
            else this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "display:" + (this.Visible ? "inline;" : "none;"));


            this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "float:right");
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "fitper");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());

            this.Writer.RenderBeginTag(HtmlTextWriterTag.Fieldset.ToString());
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Legend.ToString());
            this.Writer.Write(String.Format("{0} {1}", this.Caption, (this.WeightInForm.HasValue ? String.Format("({0})", Math.Round((double)this.WeightInForm, 2)) : "")));
            this.Writer.RenderEndTag();

            /////////////////////////////// begin control

            this.SetMasterCss();
            this.SetBaseAttributeValues();
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Table.ToString());

            this.Writer.RenderBeginTag(HtmlTextWriterTag.Tbody.ToString());

            var orderedEnumerable = this.Columns.Cast<Control>().Where(x => x.ShowInGrid).OrderBy(x => x.Order).ToList();




            this.Writer.RenderBeginTag(HtmlTextWriterTag.Tr.ToString());
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Td.ToString());


            if (this.DataSource != null && DataSource.Any())
            {
                this.Rows = new List<GridRow>();
                this.Writer.AddAttribute("class", "listHeader");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Tr.ToString());


                this.Writer.AddAttribute("scope", "col");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Th.ToString());
                this.Writer.Write("ردیف");
                this.Writer.RenderEndTag();

                foreach (var gridColumn in orderedEnumerable)
                {
                    this.Writer.AddAttribute("scope", "col");
                    this.Writer.RenderBeginTag(HtmlTextWriterTag.Th.ToString());
                    var propertyInfo = gridColumn.GetType().GetProperty("Caption");
                    this.Writer.Write(propertyInfo != null ? propertyInfo.GetValue(gridColumn, null) : "");
                    this.Writer.RenderEndTag();

                }
                if (FormState != FormState.DetailsMode)
                {

                    this.Writer.AddAttribute("scope", "col");
                    this.Writer.RenderBeginTag(HtmlTextWriterTag.Th.ToString());
                    this.Writer.Write("ویرایش");
                    this.Writer.RenderEndTag();

                    this.Writer.AddAttribute("scope", "col");
                    this.Writer.RenderBeginTag(HtmlTextWriterTag.Th.ToString());
                    this.Writer.Write("حذف");
                    this.Writer.RenderEndTag();
                }

                this.Writer.RenderEndTag();



                for (int i = 0; i < this.DataSource.Count; i++)
                {
                    this.Writer.AddAttribute("class", i % 2 == 0 ? "alternating-rowstyle-grid" : "rowstyle-grid");
                    this.Writer.RenderBeginTag(HtmlTextWriterTag.Tr.ToString());
                    var gridRow = new GridRow() { RowIndex = i, RowKey = DataSource[i].Id.ToString() };
                    this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "width:10px;");
                    this.Writer.RenderBeginTag(HtmlTextWriterTag.Td.ToString());
                    this.Writer.Write(i+1);
                    this.Writer.RenderEndTag();

                    for (int j = 0; j < orderedEnumerable.Count; j++)
                    {
                        var control = orderedEnumerable[j];
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "width:" + control.Width + "px;");
                        this.Writer.RenderBeginTag(HtmlTextWriterTag.Td.ToString());
                        control.Writer = this.Writer;
                        control.FormState = FormState.ShowInGrid;
                        if (this.DataSource[i].GetFormControl.ContainsKey(control.Id))
                            control.Value = this.DataSource[i].GetFormControl[control.Id];
                        control.Generate();
                        this.Writer.RenderEndTag();
                        gridRow.Cells.Add(control);

                    }
                    this.Rows.Add(gridRow);

                    if (FormState != FormState.DetailsMode)
                    {

                        this.Writer.RenderBeginTag(HtmlTextWriterTag.Td.ToString());
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "BtnEditRow-" + this.Id);
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Onclick.ToString(), "EditRow(\"" + this.Id + "\",\"" + this.DataSource[i].Id + "\")");
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "cursor: pointer");
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Src.ToString(), "/App_Themes/Default/Images/grid-edit-ico.png");
                        this.Writer.RenderBeginTag(HtmlTextWriterTag.Img.ToString());
                        this.Writer.RenderEndTag();
                        this.Writer.RenderEndTag();



                        this.Writer.RenderBeginTag(HtmlTextWriterTag.Td.ToString());
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "cursor: pointer");
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "BtnRemoveRow-" + this.Id);
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Onclick.ToString(), "RemoveRow(\"" + this.DataSource[i].Id + "\")");
                        this.Writer.AddAttribute(HtmlTextWriterAttribute.Src.ToString(),  "/App_Themes/Default/Images/close.png");
                        this.Writer.RenderBeginTag(HtmlTextWriterTag.Img.ToString());
                        this.Writer.RenderEndTag();
                        this.Writer.RenderEndTag();
                    }
                    this.Writer.RenderEndTag();

                }




            }
            else
            {
                
                this.Writer.Write("رکوردی برای نمایش وجود ندارد.......");
               

            }

            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();


            ////
            if (FormState != FormState.DetailsMode)
            {
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Tr.ToString());
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Td.ToString());
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "fitper mtop");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());

                this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "float: none;");
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "form-button");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());

                this.Writer.AddAttribute(HtmlTextWriterAttribute.Onclick.ToString(), "AddRow(\"" + this.Id + "\");");
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Style.ToString(), "width: 100px;height:28px");
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), "ایجاد");
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(),"BtnAddrow-"+this.Id);
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "button");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                this.Writer.RenderEndTag();

                this.Writer.RenderEndTag();
                this.Writer.RenderEndTag();


                this.Writer.RenderEndTag();
                this.Writer.RenderEndTag();
            }
            /////


            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();



            ///////////////////////////// end control
            if (SelectedRowRefdata != null)
                GeneratorGridRow( SelectedRowRefdata);


            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();






        }



        private void GeneratorGridRow( FormData formData)
        {


            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "fitper");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());

            this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "GridId-" + formData.Id);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "GridId-" + formData.Id);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), this.Id);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            this.Writer.RenderEndTag();

            this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "DataId-" + formData.StructureId);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Name.ToString(), "DataId-" + formData.StructureId);
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), formData.Id.ToString());
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "hidden");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            this.Writer.RenderEndTag();


            foreach (var controlvalueModel in Columns)
            {
                ((Control)controlvalueModel).Writer = this.Writer;
                ((Control)controlvalueModel).FormState = this.FormState;
                ((Control)controlvalueModel).Value = formData.GetFormControl.ContainsKey(((Control)controlvalueModel).Id) ? formData.GetFormControl[((Control)controlvalueModel).Id] : null;
                ((Control)controlvalueModel).Generate();
            }

            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "fbfitpx");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());

            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "form-button");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Onclick.ToString(), "GenerateControl();");
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), "لغو");
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "button");
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
            this.Writer.RenderEndTag();

            this.Writer.RenderEndTag();

            if (this.FormState == FormState.DataEntryMode)
            {
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Class.ToString(), "form-button");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Div.ToString());

                this.Writer.AddAttribute(HtmlTextWriterAttribute.Onclick.ToString(),
                    "SetPostedFormidAndPostData('" + formData.StructureId + "','" + formData.Id + "');");

                this.Writer.AddAttribute(HtmlTextWriterAttribute.Id.ToString(), "BtnSaveGridId-" + this.Id);
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Value.ToString(), "ذخیره");
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Type.ToString(), "button");
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Input.ToString());
                this.Writer.RenderEndTag();

                this.Writer.RenderEndTag();

            }
        

            this.Writer.RenderEndTag();

            this.Writer.RenderEndTag();




        }
    }
}
