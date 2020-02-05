using System;
using Radyn.Framework;

namespace Radyn.Slider.DataStructure
{
    [Serializable]
    [Schema("Slider")]
    public sealed class SlideItem : DataStructureBase<SlideItem>
    {
        private Int16 _id;
        [Key(true)]
        [DbType("smallint")]
        public Int16 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Int16 _slideId;
        [DbType("smallint")]
        public Int16 SlideId
        {
            get
            {
                return this._slideId;
            }
            set
            {
                base.SetPropertyValue("SlideId", value);
                if (Slide == null)
                    this.Slide = new Slide { Id = value };
            }
        }

        [Assosiation(PropName = "SlideId")]
        public Slide Slide { get; set; }

        private string _title;
        [IsNullable]
        [DbType("nvarchar(400)")]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

        private string _navigateUrl;
        [IsNullable]
        [DbType("nvarchar(250)")]
        public string NavigateUrl
        {
            get { return _navigateUrl; }
            set { base.SetPropertyValue("NavigateUrl", value); }
        }

        private byte? _order;
        [IsNullable]
        [DbType("tinyint")]
        public byte? Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
        }



        private string _imageId;
        [DbType("varchar(50)")]
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        [MultiLanguage]
        public string ImageId
        {
            get { return _imageId; }
            set { base.SetPropertyValue("ImageId", value); }
        }



        private string _startDate;
        [IsNullable]
        [DbType("char(10)")]
        public string StartDate
        {
            get { return _startDate; }
            set { base.SetPropertyValue("StartDate", value); }
        }


        private string _finishDate;
        [IsNullable]
        [DbType("char(10)")]
        public string FinishDate
        {
            get { return _finishDate; }
            set { base.SetPropertyValue("FinishDate", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
