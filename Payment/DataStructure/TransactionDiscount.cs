using System;
using Radyn.Framework;

namespace Radyn.Payment.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class TransactionDiscount : DataStructureBase<TransactionDiscount>
    {
        private Guid _id;
        [DbType("uniqueidentifier")]
        [Key(false)]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }


        private Guid _transactionId;
        [DbType("uniqueidentifier")]
        public Guid TransactionId
        {
            get
            {
                return this._transactionId;
            }
            set
            {
                base.SetPropertyValue("TransactionId", value);
                if (Transaction == null)
                    this.Transaction = new Transaction { Id = value };
            }
        }

        [Assosiation(PropName = "TransactionId")]
        public Transaction Transaction { get; set; }

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

        private Guid? _attachId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? AttachId
        {
            get { return _attachId; }
            set { base.SetPropertyValue("AttachId", value); }
        }



      

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.DiscountType.Title; }
        }
    }
}
