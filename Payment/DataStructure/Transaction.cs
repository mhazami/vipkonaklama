using System;
using Radyn.EnterpriseNode.Tools;
using Radyn.Framework;
using Radyn.Payment.Tools;
using Radyn.Utility;
using Enums = Radyn.Payment.Tools.Enums;

namespace Radyn.Payment.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class Transaction : DataStructureBase<Transaction>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _refId;
        [IsNullable]
        [DbType("varchar(50)")]
        public string RefId
        {
            get { return _refId; }
            set { base.SetPropertyValue("RefId", value); }
        }

        private long _invoiceId;
        [DbType("bigint")]
        public long InvoiceId
        {
            get { return _invoiceId; }
            set { base.SetPropertyValue("InvoiceId", value); }
        }
        private long? _saleTrackingId;
        [IsNullable]
        [DbType("bigint")]
        public long? SaleTrackingId
        {
            get { return _saleTrackingId; }
            set { base.SetPropertyValue("SaleTrackingId", value); }
        }

        private long? _saleReferenceId;
        [IsNullable]
        [DbType("bigint")]
        public long? SaleReferenceId
        {
            get { return _saleReferenceId; }
            set { base.SetPropertyValue("SaleReferenceId", value); }
        }

        private byte _currencyType;
        [DbType("tinyint")]
        public byte CurrencyType
        {
            get { return _currencyType; }
            set { base.SetPropertyValue("CurrencyType", value); }
        }


        private byte _payTypeId;
        [DbType("tinyint")]
        public byte PayTypeId
        {
            get { return _payTypeId; }
            set { base.SetPropertyValue("PayTypeId", value); }
        }

        private DateTime _payDate;
        [DbType("datetime")]
        public DateTime PayDate
        {
            get { return _payDate; }
            set { base.SetPropertyValue("PayDate", value); }
        }

        private decimal _amount;
        [DbType("decimal")]
        public decimal Amount
        {
            get { return _amount; }
            set { base.SetPropertyValue("Amount", value); }
        }

        private string _description;
        [IsNullable]
        [DbType("nvarchar(500)")]
        public string Description
        {
            get { return _description; }
            set { base.SetPropertyValue("Description", value); }
        }

        private Int32? _status;
        [IsNullable]
        [DbType("int")]
        public Int32? Status
        {
            get { return _status; }
            set { base.SetPropertyValue("Status", value); }
        }

        private DateTime? _lastSentDate;
        [IsNullable]
        [DbType("datetime")]
        public DateTime? LastSentDate
        {
            get { return _lastSentDate; }
            set { base.SetPropertyValue("LastSentDate", value); }
        }

        private DateTime? _lastReceiveDate;
        [IsNullable]
        [DbType("datetime")]
        public DateTime? LastReceiveDate
        {
            get { return _lastReceiveDate; }
            set { base.SetPropertyValue("LastReceiveDate", value); }
        }

        private Guid? _payerId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? PayerId
        {
            get { return _payerId; }
            set { base.SetPropertyValue("PayerId", value); }
        }

        private string _payerTitle;
        [IsNullable]
        [DbType("nvarchar(150)")]
        public string PayerTitle
        {
            get { return _payerTitle; }
            set { base.SetPropertyValue("PayerTitle", value); }
        }

        private string _docNo;
        [IsNullable]
        [DbType("nvarchar(50)")]
        public string DocNo
        {
            get { return _docNo; }
            set { base.SetPropertyValue("DocNo", value); }
        }

        private bool _done;
        [DbType("bit")]
        public bool Done
        {
            get { return _done; }
            set { base.SetPropertyValue("Done", value); }
        }


        private byte? _onlineBankId;
        [IsNullable]
        [DbType("tinyint")]
        public byte? OnlineBankId
        {
            get { return _onlineBankId; }
            set { base.SetPropertyValue("OnlineBankId", value); }
        }

        private Int16? _accountId;
        [IsNullable]
        [DbType("smallint")]
        public Int16? AccountId
        {
            get
            {
                return this._accountId;
            }
            set
            {
                base.SetPropertyValue("AccountId", value);
                if (Account == null)
                {
                    if (value.HasValue)
                        this.Account = new Account { Id = value.Value };
                    else
                        this.Account = null;
                }
            }
        }


        [Assosiation(PropName = "AccountId")]
        public Account Account { get; set; }

        private string _callBackUrl;
        [IsNullable]
        [DbType("varchar(250)")]
        public string CallBackUrl
        {
            get { return _callBackUrl; }
            set { base.SetPropertyValue("CallBackUrl", value); }
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
        public Int64 TrackYourOrderNum
        {
            get { return _trackYourOrderNum; }
            set { base.SetPropertyValue("TrackYourOrderNum", value); }
        }
        private Guid? _docScan;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? DocScan
        {
            get { return _docScan; }
            set { base.SetPropertyValue("DocScan", value); }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string PersianDate
        {
            get
            {
                return this.PayDate.ShamsiDate();
            }
            set
            {
                PayDate = DateTimeUtil.ShamsiDateToGregorianDate(value);
            }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string PayerName
        {
            get
            {
                return !string.IsNullOrEmpty(this.PayerTitle)
                     ? this.PayerTitle
                     : EnterpriseNode.EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(this.PayerId).Title();
            }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string LocalUrl
        {
            get { return "http://" + System.Web.HttpContext.Current.Request.Url.Authority + ((string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ApplicationPath) || (System.Web.HttpContext.Current.Request.ApplicationPath != null) && System.Web.HttpContext.Current.Request.ApplicationPath.Equals("/")) ? "" : System.Web.HttpContext.Current.Request.ApplicationPath) + Constants.CallBackUrl + "?Id=" + Id; }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public bool PreDone
        {
            get
            {
                return this.Amount == 0 || this.PayTypeId == (byte)Enums.PayType.Documnet || (this.PayTypeId == (byte)Enums.PayType.OnlinePay && this.Done);
            }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string PayTypeDescription
        {
            get { return ((Enums.PayType)PayTypeId).GetDescriptionInLocalization(); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string CurrencytypeDescription
        {
            get { return ((Common.Definition.Enums.CurrencyType)CurrencyType).GetDescriptionInLocalization(); }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public bool IsTemp { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string OnlinebankName { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public byte[] DocScanFile { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string OnlinePayMessage { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Amount.ToString(); }
        }
    }
}
