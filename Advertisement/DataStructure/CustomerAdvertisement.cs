using System;
using Radyn.Framework;

namespace Radyn.Advertisements.DataStructure
{
    [Serializable]
    [Schema("Advertisement")]
    public sealed class CustomerAdvertisement : DataStructureBase<CustomerAdvertisement>
    {
        private Guid _customerId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid CustomerId
        {
            get
            {
                return this._customerId;
            }
            set
            {
                base.SetPropertyValue("CustomerId", value);
                if (EnterpriseNode == null)
                    this.EnterpriseNode = new EnterpriseNode.DataStructure.EnterpriseNode { Id = value };
            }
        }

        [Assosiation(PropName = "CustomerId")]
        public EnterpriseNode.DataStructure.EnterpriseNode EnterpriseNode { get; set; }

        private Guid _advertisementId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid AdvertisementId
        {
            get
            {
                return this._advertisementId;
            }
            set
            {
                base.SetPropertyValue("AdvertisementId", value);
                if (Advertisement == null)
                    this.Advertisement = new Advertisement { Id = value };
            }
        }

        [Assosiation(PropName = "AdvertisementId")]
        public Advertisement Advertisement { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.EnterpriseNode.DescriptionField; }
        }
    }
}
