using System;
using Radyn.Framework;

namespace Radyn.Advertisements.DataStructure
{
    [Serializable]
    [Schema("Advertisement")]
    public sealed class Advertisement : DataStructureBase<Advertisement>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [IsNullable]
        [DbType("nvarchar(50)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private Int32 _positionId;
        [DbType("int")]
        public Int32 PositionId
        {
            get
            {
                return this._positionId;
            }
            set
            {
                base.SetPropertyValue("PositionId", value);
                if (SectionPosition == null)
                    this.SectionPosition = new SectionPosition { Id = value };
            }
        }

        [Assosiation(PropName = "PositionId")]
        public SectionPosition SectionPosition { get; set; }

        private string _fromDate;
        [IsNullable]
        [DbType("char(10)")]
        public string FromDate
        {
            get { return _fromDate; }
            set { base.SetPropertyValue("FromDate", value); }
        }

        private string _toDate;
        [IsNullable]
        [DbType("char(10)")]
        public string ToDate
        {
            get { return _toDate; }
            set { base.SetPropertyValue("ToDate", value); }
        }

        private Int32 _clickCount;
        [DbType("int")]
        public Int32 ClickCount
        {
            get { return _clickCount; }
            set { base.SetPropertyValue("ClickCount", value); }
        }

        private Int32 _visitCount;
        [DbType("int")]
        public Int32 VisitCount
        {
            get { return _visitCount; }
            set { base.SetPropertyValue("VisitCount", value); }
        }

        private Int32 _typeId;
        [DbType("int")]
        public Int32 TypeId
        {
            get
            {
                return this._typeId;
            }
            set
            {
                base.SetPropertyValue("TypeId", value);
                if (AdvertisementType == null)
                    this.AdvertisementType = new AdvertisementType { Id = value };
            }
        }

        [Assosiation(PropName = "TypeId")]
        public AdvertisementType AdvertisementType { get; set; }

        private Guid? _fileId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? FileId
        {
            get { return _fileId; }
            set { base.SetPropertyValue("FileId", value); }
        }

        private string _navigateUrl;
        [DbType("nvarchar(400)")]
        public string NavigateUrl
        {
            get { return _navigateUrl; }
            set { base.SetPropertyValue("NavigateUrl", value); }
        }

        private bool _enable;
        [DbType("bit")]
        public bool Enable
        {
            get { return _enable; }
            set { base.SetPropertyValue("Enable", value); }
        }

        private Int16 _height;
        [DbType("smallint")]
        public Int16 Height
        {
            get { return _height; }
            set { base.SetPropertyValue("Height", value); }
        }

        private Int16 _width;
        [DbType("smallint")]
        public Int16 width
        {
            get { return _width; }
            set { base.SetPropertyValue("width", value); }
        }

        private string _text;
        [IsNullable]
        [DbType("ntext")]
        public string Text
        {
            get { return _text; }
            set { base.SetPropertyValue("Text", value); }
        }

        private Int32? _timeout;
        [IsNullable]
        [DbType("int")]
        public Int32? Timeout
        {
            get { return _timeout; }
            set { base.SetPropertyValue("Timeout", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
