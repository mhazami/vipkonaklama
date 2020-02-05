using System;
using Radyn.Framework;

namespace Radyn.Advertisements.DataStructure
{
    [Serializable]
    [Schema("Advertisement")]
    public sealed class History : DataStructureBase<History>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Guid _advertisementId;
        [DbType("uniqueidentifier")]
        public Guid AdvertisementId
        {
            get
            {
                return this._advertisementId;
            }
            set
            {
                base.SetPropertyValue("AdvertisementId", value);
                 if (Advertisement == null)
                    this.Advertisement = new Advertisements.DataStructure.Advertisement { Id = value };
            }
        }

        [Assosiation(PropName = "AdvertisementId")]
        public Advertisements.DataStructure.Advertisement Advertisement { get; set; }

        private string _fromDate;
        [DbType("char(10)")]
        public string FromDate
        {
            get { return _fromDate; }
            set { base.SetPropertyValue("FromDate", value); }
        }

        private string _toDate;
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

        private Int32 _visiteCount;
        [DbType("int")]
        public Int32 VisiteCount
        {
            get { return _visiteCount; }
            set { base.SetPropertyValue("VisiteCount", value); }
        }

        private decimal _price;
        [DbType("decimal")]
        public decimal Price
        {
            get { return _price; }
            set { base.SetPropertyValue("Price", value); }
        }

        private string _description;
        [DbType("nvarchar(100)")]
        public string Description
        {
            get { return _description; }
            set { base.SetPropertyValue("Description", value); }
        }

        private Guid _tarrifClassHistoryId;
        [DbType("uniqueidentifier")]
        public Guid TarrifClassHistoryId
        {
            get
            {
                return this._tarrifClassHistoryId;
            }
            set
            {
                base.SetPropertyValue("TarrifClassHistoryId", value);
                this.TariffClassHistory = new TariffClassHistory { Id = value };
            }
        }

        [Assosiation(PropName = "TarrifClassHistoryId")]
        public TariffClassHistory TariffClassHistory { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Description; }
        }
    }
}
