using Radyn.Framework;
using Radyn.Security.DataStructure;
using System;
using static Radyn.Reservation.Enum;

namespace Radyn.Reservation.DataStructure
{
    [Serializable]
    [Schema("Reservation")]
    public sealed class Order : DataStructureBase<Order>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }


        private int _orderId;
        [DbType("int")]
        [Unique(Group = "", IgnoreNull = true)]
        public int OrderId
        {
            get { return _orderId; }
            set { base.SetPropertyValue("OrderId", value); }
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


        private Guid? _roomId;
        [DbType("uniqueidentifier")]
        [IsNullable]
        public Guid? RoomId
        {
            get { return _roomId; }
            set { base.SetPropertyValue("RoomId", value); }
        }
        [Assosiation(PropName = "RoomId")]
        public Room Room { get; set; }

        private Guid? _customerId;
        [DbType("uniqueidentifier")]
        [IsNullable]
        public Guid? CustomerId
        {
            get { return _customerId; }
            set { base.SetPropertyValue("CustomerId", value); }
        }
        [Assosiation(PropName = "CustomerId")]
        public Customer Customer { get; set; }

        private Guid? _userId;
        [DbType("uniqueidentifier")]
        [IsNullable]
        public Guid? UserId
        {
            get { return _userId; }
            set { base.SetPropertyValue("UserId", value); }
        }
        [Assosiation(PropName = "UserId")]
        public User User { get; set; }


        private byte _personCount;
        [DbType("tinyint")]
        public byte PersonCount
        {
            get { return _personCount; }
            set { base.SetPropertyValue("PersonCount", value); }
        }


        private byte _dayCount;
        [DbType("tinyint")]
        [IsNullable]
        public byte DayCount
        {
            get { return _dayCount; }
            set { base.SetPropertyValue("DayCount", value); }
        }

        private byte _hourCount;
        [DbType("tinyint")]
        [IsNullable]
        public byte HourCount
        {
            get { return _hourCount; }
            set { base.SetPropertyValue("HourCount", value); }
        }


        private decimal _totalPrice;
        [DbType("decimal(18, 2)")]
        [IsNullable]
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { base.SetPropertyValue("TotalPrice", value); }
        }

        private byte _paymentStatus;
        [DbType("tinyint")]
        [IsNullable]
        public byte PaymentStatus
        {
            get { return _paymentStatus; }
            set { base.SetPropertyValue("PaymentStatus", value); }
        }

        private DateTime _orderDate;
        [DbType("datetime")]

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { base.SetPropertyValue("OrderDate", value); }
        }

        private DateTime _entryDate;
        [DbType("datetime")]
        public DateTime EntryDate
        {
            get { return _entryDate; }
            set { base.SetPropertyValue("EntryDate", value); }
        }

        private string _entryTime;
        [DbType("char(5)")]
        [IsNullable]
        public string EntryTime
        {
            get { return _entryTime; }
            set { base.SetPropertyValue("EntryTime", value); }
        }


        private DateTime _exitDate;
        [DbType("datetime")]
        public DateTime ExitDate
        {
            get { return _exitDate; }
            set { base.SetPropertyValue("ExitDate", value); }
        }

        private string _exitTime;
        [DbType("char(5)")]
        [IsNullable]
        public string ExitTime
        {
            get { return _exitTime; }
            set { base.SetPropertyValue("ExitTime", value); }
        }


        private string _description;
        [DbType("nvarchar(MAX)")]
        [IsNullable]
        public string Description
        {
            get { return _description; }
            set { base.SetPropertyValue("Description", value); }
        }

        private string _code;
        [DbType("nvarchar(7)")]
        [IsNullable]
        public string Code
        {
            get { return _code; }
            set { base.SetPropertyValue("Code", value); }
        }

        private PaymentType _paymentType;
        [DbType("tinyint")]
        [IsNullable]
        public PaymentType PaymentType
        {
            get { return _paymentType; }
            set { base.SetPropertyValue("PaymentType", value); }
        }

        private Guid _reserveTypeId;
        [DbType("uniqueidentifier")]
        public Guid ReserveTypeId
        {
            get { return _reserveTypeId; }
            set { base.SetPropertyValue("ReserveTypeId", value); }
        }
        [Assosiation(PropName = "ReserveTypeId")]
        public ReserveType ReserveType { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return $"{Customer.FirstName} {Customer.LastName}"; }
        }
    }
}
