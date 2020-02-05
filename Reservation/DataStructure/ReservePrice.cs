using Radyn.Framework;
using System;
using static Radyn.Reservation.Enum;

namespace Radyn.Reservation.DataStructure
{
    [Serializable]
    [Schema("Reservation")]
    public sealed class ReservePrice : DataStructureBase<ReservePrice>
    {
        public ReservePrice()
        {
            
        }
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
        [MultiLanguage]
        [IsNullable]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }


        private DayType _dayType;
        [DbType("tinyint")]
        public DayType DayType
        {
            get { return _dayType; }
            set { base.SetPropertyValue("DayType", value); }
        }

        private decimal _perDayPrice;
        [DbType("decimal(18,2)")]
        public decimal PerDayPrice
        {
            get { return _perDayPrice; }
            set { base.SetPropertyValue("PerDayPrice", value); }
        }

        private TimeSheet _fromHour;
        [DbType("tinyint")]
        public TimeSheet FromHour
        {
            get { return _fromHour; }
            set { base.SetPropertyValue("FromHour", value); }
        }

        private TimeSheet _toHour;
        [DbType("tinyint")]
        public TimeSheet ToHour
        {
            get { return _toHour; }
            set { base.SetPropertyValue("ToHour", value); }
        }

        private byte _roomTypeId;
        [DbType("tinyint")]
        public byte RoomTypeId
        {
            get { return _roomTypeId; }
            set { base.SetPropertyValue("RoomTypeId", value); }
        }
        [Assosiation(PropName = "RoomTypeId")]
        public RoomType RoomType { get; set; }


        private ReserveType _reserveType;
        [DbType("tinyint")]
        [IsNullable]
        public ReserveType ReserveType
        {
            get { return _reserveType; }
            set { base.SetPropertyValue("ReserveType", value); }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField { get { return string.Empty; } }
    }
}
