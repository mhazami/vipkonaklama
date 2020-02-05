using System;
using Radyn.Framework;

namespace Radyn.Payment.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class Temp : DataStructureBase<Temp>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private decimal _amount;
        [DbType("decimal")]
        public decimal Amount
        {
            get { return _amount; }
            set { base.SetPropertyValue("Amount", value); }
        }

        private Guid? _payerId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? PayerId
        {
            get { return _payerId; }
            set { base.SetPropertyValue("PayerId", value); }
        }
        private DateTime _date;
        [DbType("datetime")]
        public DateTime Date
        {
            get { return _date; }
            set { base.SetPropertyValue("Date", value); }
        }

        private string _description;
        [IsNullable]
        [DbType("nvarchar(400)")]
        public string Description
        {
            get { return _description; }
            set { base.SetPropertyValue("Description", value); }
        }
        private string _payerTitle;
        [IsNullable]
        [DbType("nvarchar(150)")]
        public string PayerTitle
        {
            get { return _payerTitle; }
            set { base.SetPropertyValue("PayerTitle", value); }
        }
        private Guid? _transactionId;
        [DbType("uniqueidentifier")]
        public Guid? TransactionId
        {
            get { return _transactionId; }
            set { base.SetPropertyValue("TransactionId", value); }
        }
        private string _callBackUrl;
        [IsNullable]
        [DbType("varchar(250)")]
        public string CallBackUrl
        {
            get { return _callBackUrl; }
            set { base.SetPropertyValue("CallBackUrl", value); }
        }
        private Guid? _parentId;
        [DbType("uniqueidentifier")]
        public Guid? ParentId
        {
            get { return _parentId; }
            set { base.SetPropertyValue("ParentId", value); }
        }

        private byte _currencyType;
        [DbType("tinyint")]
        public byte CurrencyType
        {
            get { return _currencyType; }
            set { base.SetPropertyValue("CurrencyType", value); }
        }
        private string _additionalData;
        [IsNullable]
        [DbType("nvarchar(max)")]
        public string AdditionalData
        {
            get { return _additionalData; }
            set { base.SetPropertyValue("AdditionalData", value); }
        }
        private Int64 _trackYourOrderNum;
        [DbType("bigint")]
        [DisableAction(DisableInsert = true, DisableUpdate = true)]
        public Int64 TrackYourOrderNum
        {
            get { return _trackYourOrderNum; }
            set { base.SetPropertyValue("TrackYourOrderNum", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string PaymentUrl { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string PayType { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string MerchantId { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string TerminalId { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string TerminalUserName { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string TerminalPassword { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string CertificatePath { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string CertificatePassword { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string MerchantPublicKey { get; set; }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string MerchantPrivateKey { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public byte? BankId { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Amount.ToString(); }
        }
    }
}
