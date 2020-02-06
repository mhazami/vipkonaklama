using Radyn.Framework;
using System;

namespace Radyn.Reservation.DataStructure
{
    [Serializable]
    [Schema("Reservation")]
    public sealed class RoomType : DataStructureBase<RoomType>
    {
        public RoomType()
        {
            Enable = false;
        }

        private byte _id;
        [Key(true)]
        [DbType("tinyint")]
        public byte Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [DbType("nvarchar(50)")]
        [IsNullable]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private bool _enable;
        [DbType("bit")]
        public bool Enable
        {
            get { return _enable; }
            set { base.SetPropertyValue("Enable", value); }
        }

        private byte _personCapacity;
        [DbType("tinyint")]
        public byte PersonCapacity
        {
            get { return _personCapacity; }
            set { base.SetPropertyValue("PersonCapacity", value); }
        }

        private Guid _picId;
        [DbType("uniqueidentifier")]
        public Guid PicId
        {
            get { return _picId; }
            set { base.SetPropertyValue("PicId", value); }
        }

        private string _description;
        [DbType("nvarchar(300)")]
        [MultiLanguage]
        public string Description
        {
            get { return _description; }
            set { base.SetPropertyValue("Description", value); }
        }


        private string _displayText;
        [DbType("nvarchar(100)")]
        [MultiLanguage]
        [IsNullable]
        public string DisplayText
        {
            get { return _displayText; }
            set { base.SetPropertyValue("DisplayText", value); }
        }


        private byte _order;
        [DbType("tinyint")]
        public byte Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField { get { return this.Title; } }
    }
}
