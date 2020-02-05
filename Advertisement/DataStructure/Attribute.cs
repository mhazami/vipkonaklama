using System;
using Radyn.Framework;

namespace Radyn.Advertisements.DataStructure
{
    [Serializable]
    [Schema("Advertisement")]
    public sealed class Attribute : DataStructureBase<Attribute>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get
            {
                return this._id;
            }
            set
            {
                base.SetPropertyValue("Id", value);
                if (Advertisement == null)
                    this.Advertisement = new Advertisement { Id = value };
            }
        }

        [Assosiation(PropName = "Id")]
        public Advertisement Advertisement { get; set; }

        private Guid _tariffId;
        [DbType("uniqueidentifier")]
        public Guid TariffId
        {
            get
            {
                return this._tariffId;
            }
            set
            {
                base.SetPropertyValue("TariffId", value);
                if (Tariff == null)
                    this.Tariff = new Tariff { Id = value };
            }
        }

        [Assosiation(PropName = "TariffId")]
        public Tariff Tariff { get; set; }

        private decimal _price;
        [DbType("decimal")]
        public decimal Price
        {
            get { return _price; }
            set { base.SetPropertyValue("Price", value); }
        }

        private bool _orientation;
        [DbType("bit")]
        public bool Orientation
        {
            get { return _orientation; }
            set { base.SetPropertyValue("Orientation", value); }
        }

        private Guid _advertisemntId;
        [DbType("uniqueidentifier")]
        public Guid AdvertisemntId
        {
            get
            {
                return this._advertisemntId;
            }
            set
            {
                base.SetPropertyValue("AdvertisemntId", value);
                if (OneAdvertisement == null)
                    this.OneAdvertisement = new Advertisement { Id = value };
            }
        }

        [Assosiation(PropName = "AdvertisemntId")]
        public Advertisement OneAdvertisement { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Price.ToString(); }
        }
    }
}
