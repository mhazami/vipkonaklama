using Radyn.Framework;
using System;

namespace Radyn.Payment.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class WebDesignAccount : DataStructureBase<WebDesignAccount>
    {
        private Guid _webId;
        [Key(false)]
        [DbType("uniqueidentifier")]
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

     

        private Int16 _accountId;
        [Key(false)]
        [DbType("smallint")]
        public Int16 AccountId
        {
            get { return _accountId; }
            set
            {
                base.SetPropertyValue("AccountId", value);
                if (PaymentAccount == null)
                    PaymentAccount = new Payment.DataStructure.Account() { Id = value };
            }
        }
        [Assosiation(PropName = "AccountId")]
        public Payment.DataStructure.Account PaymentAccount { get; set; }




        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField { get; }
    }
}
