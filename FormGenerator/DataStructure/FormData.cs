using System;
using System.Collections.Generic;
using Radyn.FormGenerator.ModelView;
using Radyn.Framework;

namespace Radyn.FormGenerator.DataStructure
{
    [Serializable]
    [Schema("FormGenerator")]
    public sealed class FormData : DataStructureBase<FormData>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

    


        private Guid _structureId;
        [DbType("uniqueidentifier")]
        public Guid StructureId
        {
            get
            {
                return this._structureId;
            }
            set
            {
                base.SetPropertyValue("StructureId", value);
                if (FromStructure == null)
                    this.FromStructure = new FormStructure { Id = value };
            }
        }

        [Assosiation(PropName = "StructureId")]
        //[Assosiation]
        public FormStructure FromStructure { get; set; }

        private string _data;
        [IsNullable]
        [DbType("ntext")]
        [MultiLanguage]
        public string Data
        {
            get { return _data; }
            set { base.SetPropertyValue("Data", value); }
        }


        private string _objectName;
        [IsNullable]
        [DbType("varchar(100)")]
        public string ObjectName
        {
            get { return _objectName; }
            set { base.SetPropertyValue("ObjectName", value); }
        }

        private DateTime _saveDate;
        [DbType("datetime")]
        public DateTime SaveDate
        {
            get { return _saveDate; }
            set { base.SetPropertyValue("SaveDate", value); }
        }
        private string _refId;
        [DbType("varchar(100)")]
        public string RefId
        {
            get { return _refId; }
            set { base.SetPropertyValue("RefId", value); }
        }
        private Guid? _dataFileId;
        [MultiLanguage]
        [DbType("uniqueidentifier")]
        public Guid? DataFileId
        {
            get
            {
                return this._dataFileId;
            }
            set
            {
                base.SetPropertyValue("DataFileId", value);

            }
        }



        private Dictionary<string, Object> _getFormControl;
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        //[DiableTrack]
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
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<FormDataReport> FormDataReports { get; set; }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return string.Empty; }
        }
    }
}
