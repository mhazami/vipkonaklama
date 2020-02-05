using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;

namespace Radyn.Payment.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class DiscountType : DataStructureBase<DiscountType>
    {
        public DiscountType()
        {
            DiscountTypeAutoCodes = new List<DiscountTypeAutoCode>();
        }

        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }



        private bool _isPercent;
        [DbType("bit")]
        public bool IsPercent
        {
            get { return _isPercent; }
            set { base.SetPropertyValue("IsPercent", value); }
        }

        private string _title;
        [DbType("nvarchar(150)")]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _remark;
        [IsNullable]
        [DbType("nvarchar(500)")]
        public string Remark
        {
            get { return _remark; }
            set { base.SetPropertyValue("Remark", value); }
        }

        private bool _forceAttach;
        [DbType("bit")]
        public bool ForceAttach
        {
            get { return _forceAttach; }
            set { base.SetPropertyValue("ForceAttach", value); }
        }

        private bool _isExternal;
        [DbType("bit")]
        public bool IsExternal
        {
            get { return _isExternal; }
            set { base.SetPropertyValue("IsExternal", value); }
        }



        private string _startDate;
        [DbType("char(10)")]
        public string StartDate
        {
            get { return _startDate; }
            set { base.SetPropertyValue("StartDate", value); }
        }

        private string _endDate;
        [DbType("char(10)")]
        public string EndDate
        {
            get { return _endDate; }
            set { base.SetPropertyValue("EndDate", value); }
        }

        private string _code;
        [DbType("varchar(20)")]
        public string Code
        {
            get { return _code; }
            set { base.SetPropertyValue("Code", value); }
        }
        private short? _capacity;
        [DbType("smallint")]
        public short? Capacity
        {
            get { return _capacity; }
            set { base.SetPropertyValue("Capacity", value); }
        }
        private short? _remainCapacity;
        [DbType("smallint")]
        public short? RemainCapacity
        {
            get { return _remainCapacity; }
            set { base.SetPropertyValue("RemainCapacity", value); }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<DiscountTypeAutoCode> DiscountTypeAutoCodes { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public bool IsAutoCode { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public bool ForceCode { get; set; }

        private string _currencyType;
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        [MultiLanguage]
        public string CurrencyType
        {
            get { return string.IsNullOrEmpty(_currencyType) ? "0" : _currencyType; }
            set { _currencyType = value; }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        [MultiLanguage]
        public string ValidValue { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string UserCode { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public HttpPostedFileBase UserAttachedFile { get; set; }



        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
