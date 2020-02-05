using System;
using Radyn.Framework;

namespace Radyn.Payment.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class DiscountTypeSection : DataStructureBase<DiscountTypeSection>
    {
        private Guid _discountTypeId;
        [Key(false)]
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
                {
                    if (DiscountType == null)
                        this.DiscountType = new DiscountType { Id = value };
                }
            }
        }

        [Assosiation(PropName = "DiscountTypeId")]
        public DiscountType DiscountType { get; set; }

        private string _moudelName;
        [Key(false)]
        [DbType("varchar(50)")]
        public string MoudelName
        {
            get { return _moudelName; }
            set { base.SetPropertyValue("MoudelName", value); }
        }

        private byte _section;
        [Key(false)]
        [DbType("tinyint")]
        public byte Section
        {
            get { return _section; }
            set { base.SetPropertyValue("Section", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Section.ToString(); }
        }
    }
}
