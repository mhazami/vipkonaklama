using Radyn.Framework;
using System;

namespace Radyn.Reservation.DataStructure
{
    [Serializable]
    [Schema("Reservation")]
    public sealed class Room : DataStructureBase<Room>
    {
        public Room()
        {
            Idle = false;
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

        private bool _idle;
        [DbType("bit")]
        public bool Idle
        {
            get { return _idle; }
            set { base.SetPropertyValue("Idle", value); }
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


        private Guid _FloorId;
        [DbType("uniqueidentifier")]
        public Guid FloorId
        {
            get
            {
                return this._FloorId;
            }
            set
            {
                base.SetPropertyValue("FloorId", value);
                this.HotelFloor = new HotelFloor { Id = value };
            }
        }
        [Assosiation(PropName = "FloorId")]
        public HotelFloor HotelFloor { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField { get { return this.Title; } }
    }
}
