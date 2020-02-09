using System;
using Radyn.Framework;

namespace Radyn.Reservation.DataStructure
{
    [Serializable]
    [Schema("Reservation")]
    public sealed class OfficeRoom : DataStructureBase<OfficeRoom>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Guid _officeId;
        [DbType("uniqueidentifier")]
        public Guid OfficeId
        {
            get
            {
                return this._officeId;
            }
            set
            {
                base.SetPropertyValue("OfficeId", value);
                this.HotelOffice = new HotelOffice { Id = value };
            }
        }
        [Assosiation(PropName = "OfficeId")]
        public HotelOffice HotelOffice { get; set; }

        private Guid _roomId;
        [DbType("uniqueidentifier")]
        public Guid RoomId
        {
            get
            {
                return this._roomId;
            }
            set
            {
                base.SetPropertyValue("RoomId", value);
                this.Room = new Room { Id = value };
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