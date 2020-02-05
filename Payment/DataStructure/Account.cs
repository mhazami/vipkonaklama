using System;
using Radyn.Common.DataStructure;
using Radyn.Framework;

namespace Radyn.Payment.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class Account : DataStructureBase<Account>
    {
        private Int16 _id;
        [Key(true)]
        [DbType("smallint")]
        public Int16 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Int32 _bankId;
        [DbType("int")]
        public Int32 BankId
        {
            get
            {
                return this._bankId;
            }
            set
            {
                base.SetPropertyValue("BankId", value);
                if (Bank == null)
                    this.Bank = new Bank { Id = value };
            }
        }

        [Assosiation(PropName = "BankId")]
        public Bank Bank { get; set; }

        private string _accountNo;
        [DbType("nvarchar(100)")]
        public string AccountNo
        {
            get { return _accountNo; }
            set { base.SetPropertyValue("AccountNo", value); }
        }

        private bool _isExternal;
        [DbType("bit")]
        public bool IsExternal
        {
            get { return _isExternal; }
            set { base.SetPropertyValue("IsExternal", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string Fulltitle
        {
            get
            {
                return this.AccountNo + " " + "(" + this.Bank.Title + ")";
            }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.AccountNo; }
        }
    }
}
