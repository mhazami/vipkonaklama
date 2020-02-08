using Radyn.Framework;
using Radyn.Security.DataStructure;
using System;

namespace Radyn.Reservation.DataStructure
{
    [Serializable]
    [Schema("Reservation")]
    public sealed class UserHotelAccess : DataStructureBase<UserHotelAccess>
    {
        private Guid _userId;
        [DbType("uniqueidentifier")]
        public Guid UserId
        {
            get
            {
                return this._userId;
            }
            set
            {
                base.SetPropertyValue("UserId", value);
                this.User = new User { Id = value };
            }
        }
        [Assosiation(PropName = "UserId")]
        public User User { get; set; }

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

        private Guid? _officeId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? OfficeId
        {
            get
            {
                return this._officeId;
            }
            set
            {
                base.SetPropertyValue("OfficeId", value);
                if (value.HasValue)
                    this.HotelOffice = new HotelOffice { Id = value.Value };
                else
                    this.HotelOffice = null;
            }
        }
        [Assosiation(PropName = "OfficeId")]
        public HotelOffice HotelOffice { get; set; }

        private Guid? _roomId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? RoomId
        {
            get
            {
                return this._roomId;
            }
            set
            {
                base.SetPropertyValue("RoomId", value);
                if (value.HasValue)
                    this.Room = new Room { Id = value.Value };
                else
                    this.Room = null;
            }
        }
        [Assosiation(PropName = "RoomId")]
        public Room Room { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return string.Empty; }
        }
    }

}