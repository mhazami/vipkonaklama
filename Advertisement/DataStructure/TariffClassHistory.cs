using System;
using Radyn.Framework;

namespace Radyn.Advertisements.DataStructure
{
    [Serializable]
    [Schema("Advertisement")]
    public sealed class TariffClassHistory : DataStructureBase<TariffClassHistory>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Int32 _positionId;
        [DbType("int")]
        public Int32 PositionId
        {
            get { return _positionId; }
            set { base.SetPropertyValue("PositionId", value); }
        }

        private Int32 _perDay;
        [DbType("int")]
        public Int32 PerDay
        {
            get { return _perDay; }
            set { base.SetPropertyValue("PerDay", value); }
        }

        private Int32 _perClick;
        [DbType("int")]
        public Int32 PerClick
        {
            get { return _perClick; }
            set { base.SetPropertyValue("PerClick", value); }
        }

        private Int32 _perVisite;
        [DbType("int")]
        public Int32 PerVisite
        {
            get { return _perVisite; }
            set { base.SetPropertyValue("PerVisite", value); }
        }

        private decimal _price;
        [DbType("decimal")]
        public decimal Price
        {
            get { return _price; }
            set { base.SetPropertyValue("Price", value); }
        }

        private string _historyDate;
        [DbType("char(10)")]
        public string HistoryDate
        {
            get { return _historyDate; }
            set { base.SetPropertyValue("HistoryDate", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Price.ToString(); }
        }
    }
}
