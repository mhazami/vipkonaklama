using System;
using Radyn.Framework;

namespace Radyn.Advertisements.DataStructure
{
    [Serializable]
    [Schema("Advertisement")]
    public sealed class TariffClass : DataStructureBase<TariffClass>
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
        [DbType("nvarchar(50)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private Int32? _perVisit;
        [IsNullable]
        [DbType("int")]
        public Int32? PerVisit
        {
            get { return _perVisit; }
            set { base.SetPropertyValue("PerVisit", value); }
        }

        private Int32? _perClick;
        [IsNullable]
        [DbType("int")]
        public Int32? PerClick
        {
            get { return _perClick; }
            set { base.SetPropertyValue("PerClick", value); }
        }

        private Int32? _perDay;
        [IsNullable]
        [DbType("int")]
        public Int32? PerDay
        {
            get { return _perDay; }
            set { base.SetPropertyValue("PerDay", value); }
        }

        private decimal _price;
        [DbType("decimal")]
        public decimal Price
        {
            get { return _price; }
            set { base.SetPropertyValue("Price", value); }
        }

        private Int32 _sectionPositionId;
        [DbType("int")]
        public Int32 SectionPositionId
        {
            get
            {
                return this._sectionPositionId;
            }
            set
            {
                base.SetPropertyValue("SectionPositionId", value);
                if (AdvertisementSectionPosition == null)
                    this.AdvertisementSectionPosition = new SectionPosition { Id = value };
            }
        }

        [Assosiation(PropName = "SectionPositionId")]
        public SectionPosition AdvertisementSectionPosition { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
