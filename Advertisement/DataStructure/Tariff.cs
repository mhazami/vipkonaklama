using System;
using Radyn.Framework;

namespace Radyn.Advertisements.DataStructure
{
    [Serializable]
    [Schema("Advertisement")]
    public sealed class Tariff : DataStructureBase<Tariff>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Guid _tariffClassId;
        [DbType("uniqueidentifier")]
        public Guid TariffClassId
        {
            get
            {
                return this._tariffClassId;
            }
            set
            {
                base.SetPropertyValue("TariffClassId", value);
                if (TariffClass == null)
                    this.TariffClass = new TariffClass { Id = value };
            }
        }

        [Assosiation(PropName = "TariffClassId")]
        public TariffClass TariffClass { get; set; }

        private Int32? _dayCount;
        [IsNullable]
        [DbType("int")]
        public Int32? DayCount
        {
            get { return _dayCount; }
            set { base.SetPropertyValue("DayCount", value); }
        }

        private Int32? _clickCount;
        [IsNullable]
        [DbType("int")]
        public Int32? ClickCount
        {
            get { return _clickCount; }
            set { base.SetPropertyValue("ClickCount", value); }
        }

        private Int32? _visitCount;
        [IsNullable]
        [DbType("int")]
        public Int32? VisitCount
        {
            get { return _visitCount; }
            set { base.SetPropertyValue("VisitCount", value); }
        }

        private string _description;
        [IsNullable]
        [DbType("nvarchar(100)")]
        public string Description
        {
            get { return _description; }
            set { base.SetPropertyValue("Description", value); }
        }

        private string _title;
        [DbType("nvarchar(50)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private decimal _price;
        [DbType("decimal")]
        public decimal Price
        {
            get { return _price; }
            set { base.SetPropertyValue("Price", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
