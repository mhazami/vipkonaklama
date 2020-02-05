using System;
using Radyn.Framework;

namespace Radyn.Payment.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class DiscountTypeAutoCode : DataStructureBase<DiscountTypeAutoCode>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Guid _discountTypeId;
        [DbType("uniqueidentifier")]
        public Guid DiscountTypeId
        {
            get
            {
                return this._discountTypeId;
            }
            set
            {
                base.SetPropertyValue("DiscountTypeId", value);
                if (DiscountType == null)
                    this.DiscountType = new DiscountType { Id = value };
            }
        }

        [Assosiation(PropName = "DiscountTypeId")]
        public DiscountType DiscountType { get; set; }

        private string _code;
        [DbType("nvarchar(20)")]
        public string Code
        {
            get { return _code; }
            set { base.SetPropertyValue("Code", value); }
        }

        private bool _used;
        [DbType("bit")]
        public bool Used
        {
            get { return _used; }
            set { base.SetPropertyValue("Used", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Code; }
        }
    }
}
