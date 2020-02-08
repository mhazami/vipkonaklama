using System;
using Radyn.Framework;

namespace Radyn.Reservation.DataStructure
{
    [Serializable]
    [Schema("Reservation")]
    public sealed class HotelFloor : DataStructureBase<HotelFloor>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _name;
        [DbType("nvarchar(150)")]
        public string Name
        {
            get { return _name; }
            set { base.SetPropertyValue("Name", value); }
        }

        private Guid _hotelId;
        [DbType("uniqueidentifier")]
        public Guid HotelId
        {
            get
            {
                return this._hotelId;
            }
            set
            {
                base.SetPropertyValue("HotelId", value);
                this.Hotel = new Hotel { Id = value };
            }
        }
        [Assosiation(PropName = "HotelId")]
        public Hotel Hotel { get; set; }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Name; }
        }
    }

}