using System;
using Radyn.Framework;


namespace Radyn.Payment.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class WebDesignDiscountType : DataStructureBase<WebDesignDiscountType>
    {
        private Guid _webId;
        [DbType("uniqueidentifier")]
        [Key(false)]
        public Guid WebId
        {
            get
            {
                return this._webId;
            }
            set
            {
                base.SetPropertyValue("WebId", value);
               
            }
        }

     

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
                if (WebSiteDiscountType == null)
                    this.WebSiteDiscountType = new Payment.DataStructure.DiscountType { Id = value };
            }
        }

        [Assosiation(PropName = "DiscountTypeId")]
        public Payment.DataStructure.DiscountType WebSiteDiscountType { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.WebSiteDiscountType.Title; }
        }
    }
}
