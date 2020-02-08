using Radyn.Framework;
using System;

namespace Radyn.Reservation.DataStructure
{
    [Serializable]
    [Schema("Reservation")]
    public sealed class ReserveType : DataStructureBase<ReserveType>
    {
        public ReserveType()
        {
            IsDaily = false;
            Enabled = false;
        }

        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _startTime;
        [DbType("char(5)")]
        [IsNullable]
        public string StartTime
        {
            get { return _startTime; }
            set { base.SetPropertyValue("StartTime", value); }
        }

        private string _title;
        [DbType("nvarchar(MAX)")]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _endTime;
        [DbType("char(5)")]
        [IsNullable]
        public string EndTime
        {
            get { return _endTime; }
            set { base.SetPropertyValue("EndTime", value); }
        }

        private bool _isDaily;
        [DbType("bit")]
        [IsNullable]
        public bool IsDaily
        {
            get { return _isDaily; }
            set { base.SetPropertyValue("IsDaily", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        [IsNullable]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }


        private byte _order;
        [DbType("tinyint")]
        [IsNullable]
        public byte Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
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


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField { get { return this.Title; } }
    }
}
