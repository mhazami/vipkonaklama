using Radyn.FormGenerator.ModelView;
using Radyn.FormGenerator.Tools;
using Radyn.Framework;
using System;
using System.Collections.Generic;

namespace Radyn.FormGenerator.DataStructure
{
    [Serializable]
    [Schema("FormGenerator")]
    public sealed partial class FormEvaluation : DataStructureBase<FormEvaluation>
    {

        private string _controlId;
        [Key(false)]
        [DbType("varchar(300)")]
        public string ControlId
        {
            get { return _controlId; }
            set { base.SetPropertyValue("ControlId", value); }
        }




        [MultiLanguage]
        public string StructureFileId { get; set; }

        [MultiLanguage]
        public string DataFileId { get; set; }

        private double? _weight;
        [DbType("float")]
        [IsNullable]
        public double? Weight
        {
            get { return _weight; }
            set { base.SetPropertyValue("Weight", value); }
        }

        private int? _opinionCount;
        [DbType("int")]
        [IsNullable]
        public int? OpinionCount
        {
            get { return _opinionCount; }
            set { base.SetPropertyValue("OpinionCount", value); }
        }

        private int? _minScale;
        [DbType("int")]
        [IsNullable]
        public int? MinScale
        {
            get { return _minScale; }
            set { base.SetPropertyValue("MinScale", value); }
        }
        private int? _maxScale;
        [DbType("int")]
        [IsNullable]
        public int? MaxScale
        {
            get { return _maxScale; }
            set { base.SetPropertyValue("MaxScale", value); }
        }


        private Dictionary<string, Object> _getFormControl;
        [DisableAction(DiableAllAction = true)]

        public Dictionary<string, Object> GetFormControl
        {
            get
            {
                if (this._getFormControl == null)
                    this._getFormControl = new Dictionary<string, Object>();
                return _getFormControl;
            }
            set { _getFormControl = value; }
        }

        private Controls _controls;
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public Controls Controls
        {
            get
            {
                if (this._controls == null)
                    this._controls = new Controls();
                return _controls;
            }
            set { _controls = value; }
        }
        [DisableAction(DiableAllAction = true)]

        public FormState FormState { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true, DisableTrack = true)]

        public override string DescriptionField
        {
            get { return this.ControlId; }
        }
    }
}
