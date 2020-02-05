using System;
using Radyn.Framework;

namespace Radyn.Payment.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class TempDiscount : DataStructureBase<TempDiscount>
    {

        private Guid _id;
        [DbType("uniqueidentifier")]
        [Key(false)]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }


        private Guid _tempId;
        [DbType("uniqueidentifier")]
        public Guid TempId
        {
            get
            {
                return this._tempId;
            }
            set
            {
                base.SetPropertyValue("TempId", value);
                if (Temp == null)
                    this.Temp = new Temp { Id = value };
            }
        }

        [Assosiation(PropName = "TempId")]
        public Temp Temp { get; set; }

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
        public bool Added { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.DiscountType.Title; }
        }
    }
}
