using System;
using Radyn.Framework;

namespace Radyn.WebDesign.DataStructure
{
    [Serializable]
    [Schema("WebDesign")]
    public sealed class Configuration : DataStructureBase<Configuration>
    {
        public Configuration()
        {
            EnableAds = false;
        }
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get
            {
                return this._id;
            }
            set
            {
                base.SetPropertyValue("Id", value);
                this.WebSite = new WebSite { Id = value };
            }
        }

        [Assosiation]
        public WebSite WebSite { get; set; }

        private string _introPageUrl;
        [IsNullable]
        [DbType("nvarchar(250)")]
        public string IntroPageUrl
        {
            get { return _introPageUrl; }
            set { base.SetPropertyValue("IntroPageUrl", value); }
        }


        private Int32? _maxNewsShow;
        [IsNullable]
        [DbType("int")]
        public Int32? MaxNewsShow
        {
            get { return _maxNewsShow; }
            set { base.SetPropertyValue("MaxNewsShow", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

        private Int32? _headerId;
        [IsNullable]
        [DbType("int")]
        public Int32? HeaderId
        {
            get { return _headerId; }
            set { base.SetPropertyValue("HeaderId", value); }
        }

        private Int32? _footerId;
        [IsNullable]
        [DbType("int")]
        public Int32? FooterId
        {
            get { return _footerId; }
            set { base.SetPropertyValue("FooterId", value); }
        }

        private short? _bigSlideId;

        [DbType("smallint")]
        [IsNullable]
        public short? BigSlideId
        {
            get { return this._bigSlideId; }
            set { base.SetPropertyValue("BigSlideId", value); }
        }

        private short? _miniSlideId;
        [DbType("smallint")]
        [IsNullable]
        public short? MiniSlideId
        {
            get { return this._miniSlideId; }
            set { base.SetPropertyValue("MiniSlideId", value); }
        }

        private short? _averageSlideId;
        [DbType("smallint")]
        [IsNullable]
        public short? AverageSlideId
        {
            get { return this._averageSlideId; }
            set { base.SetPropertyValue("AverageSlideId", value); }
        }
        private int? _introPageId;
        [IsNullable]
        [DbType("int")]
        public int? IntroPageId
        {
            get { return _introPageId; }
            set { base.SetPropertyValue("IntroPageId", value); }
        }



     

        private Guid? _defaultContainerID;

        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? DefaultContainerID
        {
            get { return _defaultContainerID; }
            set { base.SetPropertyValue("DefaultContainerID", value); }
        }

        private Guid? _defaultHTMLID;

        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? DefaultHTMLID
        {
            get { return _defaultHTMLID; }
            set { base.SetPropertyValue("DefaultHTMLID", value); }
        }

        private Guid? _defaultMenuHtmlId;
        [DbType("uniqueidentifier")]
        public Guid? DefaultMenuHtmlId
        {
            get
            {
                return this._defaultMenuHtmlId;
            }
            set
            {
                base.SetPropertyValue("DefaultMenuHtmlId", value);
                if (MenuHtml == null)
                    this.MenuHtml = value.HasValue ? new ContentManager.DataStructure.MenuHtml { Id = value.Value } : null;
            }
        }

        [Assosiation(PropName = "DefaultMenuHtmlId")]
        public ContentManager.DataStructure.MenuHtml MenuHtml { get; set; }

        private Guid? _favIcon;

        [DbType("uniqueidentifier")]
        [IsNullable]
        public Guid? FavIcon
        {
            get { return _favIcon; }
            set
            {
                base.SetPropertyValue("FavIcon", value);
            }
        }

        //SMS
        private int _sMSAccountId;
        [IsNullable]
        [DbType("int")]
        public int SMSAccountId
        {
            get { return _sMSAccountId; }
            set { base.SetPropertyValue("SMSAccountId", value); }
        }
        private string _sMSAccountUserName;
        [IsNullable]
        [DbType("varchar(20)")]
        public string SMSAccountUserName
        {
            get { return _sMSAccountUserName; }
            set { base.SetPropertyValue("SMSAccountUserName", value); }
        }
        private string _sMSAccountPassword;
        [IsNullable]
        [DbType("varchar(150)")]
        public string SMSAccountPassword
        {
            get { return _sMSAccountPassword; }
            set { base.SetPropertyValue("SMSAccountPassword", value); }
        }


        //Email
        private string _mailHost;
        [DbType("varchar(50)")]
        public string MailHost
        {
            get { return _mailHost; }
            set { base.SetPropertyValue("MailHost", value); }
        }
        private string _mailPassword;
        [DbType("varchar(100)")]
        public string MailPassword
        {
            get { return _mailPassword; }
            set { base.SetPropertyValue("MailPassword", value); }
        }

        private string _mailUserName;
        [DbType("varchar(50)")]
        public string MailUserName
        {
            get { return _mailUserName; }
            set { base.SetPropertyValue("MailUserName", value); }
        }

        private string _mailFrom;
        [DbType("varchar(50)")]
        public string MailFrom
        {
            get { return _mailFrom; }
            set { base.SetPropertyValue("MailFrom", value); }
        }

        private short _mailPort;
        [DbType("smallint")]
        public short MailPort
        {
            get { return _mailPort; }
            set { base.SetPropertyValue("MailPort", value); }
        }
        private bool _enableSSL;
        [DbType("bit")]
        public bool EnableSSL
        {
            get { return _enableSSL; }
            set { base.SetPropertyValue("EnableSSL", value); }
        }


        private short _groupEmailInterval;
        [DbType("smallint")]
        [IsNullable]
        public short GroupEmailInterval
        {
            get { return this._groupEmailInterval; }
            set { base.SetPropertyValue("GroupEmailInterval", value); }
        }

        //Discount
        private Int16 _disscountCount;
        [Layout(Caption = "چند تخفیف داشته باشد")]
        [DbType("smallint")]
        public Int16 DisscountCount
        {
            get { return _disscountCount; }
            set { base.SetPropertyValue("DisscountCount", value); }
        }

        private string _paymentType;
        [DbType("varchar(20)")]
        [IsNullable]
        public string PaymentType
        {
            get { return _paymentType; }
            set { base.SetPropertyValue("PaymentType", value); }
        }

        private string _merchantId;
        [IsNullable]
        [DbType("varchar(100)")]
        public string MerchantId
        {
            get { return _merchantId; }
            set { base.SetPropertyValue("MerchantId", value); }
        }

        private int _terminalId;
        [IsNullable]
        [DbType("int")]
        public int TerminalId
        {
            get { return _terminalId; }
            set { base.SetPropertyValue("TerminalId", value); }
        }

        private byte? _bankId;
        [DbType("tinyint")]
        [IsNullable]
        public byte? BankId
        {
            get { return _bankId; }
            set { base.SetPropertyValue("BankId", value); }
        }

        private string _terminalUserName;
        [IsNullable]
        [DbType("varchar(20)")]
        public string TerminalUserName
        {
            get { return _terminalUserName; }
            set { base.SetPropertyValue("TerminalUserName", value); }
        }
        private string _terminalPassword;
        [IsNullable]
        [DbType("varchar(4000)")]
        public string TerminalPassword
        {
            get { return _terminalPassword; }
            set { base.SetPropertyValue("TerminalPassword", value); }
        }

        private string _certificatePath;
        [IsNullable]
        [DbType("varchar(200)")]
        public string CertificatePath
        {
            get { return _certificatePath; }
            set { base.SetPropertyValue("CertificatePath", value); }
        }

        private string _certificatePassword;
        [IsNullable]
        [DbType("varchar(200)")]
        public string CertificatePassword
        {
            get { return _certificatePassword; }
            set { base.SetPropertyValue("CertificatePassword", value); }
        }

        private string _merchantPublicKey;
        [IsNullable]
        [DbType("varchar(MAX)")]
        public string MerchantPublicKey
        {
            get { return _merchantPublicKey; }
            set { base.SetPropertyValue("MerchantPublicKey", value); }
        }
        private string _merchantPrivateKey;
        [IsNullable]
        [DbType("varchar(MAX)")]
        public string MerchantPrivateKey
        {
            get { return _merchantPrivateKey; }
            set { base.SetPropertyValue("MerchantPrivateKey", value); }
        }

        //UI
      
        private Guid? _backgroundImage;
        [DbType("uniqueidentifier")]
        [IsNullable]
        public Guid? BackgroundImage
        {
            get { return _backgroundImage; }
            set { base.SetPropertyValue("BackgroundImage", value); }
        }


        private string _backgroundColor;
        [DbType("varchar(6)")]
        [IsNullable]
        public string BackgroundColor
        {
            get { return _backgroundColor; }
            set { base.SetPropertyValue("BackgroundColor", value); }
        }

        private string _webStatistics;
        [DbType("nvarchar(max)")]
        public string WebStatistics
        {
            get { return _webStatistics; }
            set { base.SetPropertyValue("WebStatistics", value); }
        }

        //base
        private bool _hasFinancialOperation;
        [DbType("bit")]
        public bool HasFinancialOperation
        {
            get { return _hasFinancialOperation; }
            set { base.SetPropertyValue("HasFinancialOperation", value); }
        }

        private bool _registerEmailConfirm;
        [DbType("bit")]
        public bool RegisterEmailConfirm
        {
            get { return _registerEmailConfirm; }
            set { base.SetPropertyValue("RegisterEmailConfirm", value); }
        }



        //Alarm




        private byte? _userRegisterInformType;
        [DbType("tinyint")]
        public byte? UserRegisterInformType
        {
            get { return _userRegisterInformType; }
            set { base.SetPropertyValue("UserRegisterInformType", value); }
        }


        private bool _enableArticleComment;

        [DbType("bit")]
        public bool EnableArticleComment
        {
            get { return _enableArticleComment; }
            set { base.SetPropertyValue("EnableArticleComment", value);}
        }

        private bool _enableNewsComment;
        [DbType("bit")]
        public bool EnableNewsComment
        {
            get { return _enableNewsComment; }
            set { base.SetPropertyValue("EnableNewsComment", value); }
        }

        private bool _enableAds;
        [DbType("bit")]
        public bool EnableAds
        {
            get { return _enableAds; }
            set { base.SetPropertyValue("EnableAds", value); }
        }


        private string _cRMServiceUserName;
        [IsNullable]
        [DbType("nvarchar(50)")]
        public string CRMServiceUserName
        {
            get { return _cRMServiceUserName; }
            set { base.SetPropertyValue("CRMServiceUserName", value); }
        }

        private string _cRMServicePassword;
        [IsNullable]
        [DbType("nvarchar(200)")]
        public string CRMServicePassword
        {
            get { return _cRMServicePassword; }
            set { base.SetPropertyValue("CRMServicePassword", value); }
        }

        private string _cRMServiceUrl;
        [IsNullable]
        [DbType("nvarchar(200)")]
        public string CRMServiceUrl
        {
            get { return _cRMServiceUrl; }
            set { base.SetPropertyValue("CRMServiceUrl", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.WebSite.Title; }
        }
    }
}
