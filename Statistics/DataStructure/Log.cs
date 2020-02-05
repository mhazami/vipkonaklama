using System;
using Radyn.Framework;

namespace Radyn.Statistics.DataStructure
{
    [Serializable]
    [Schema("Statistics")]
    public sealed class Log : DataStructureBase<Log>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _iP;
        [DbType("varchar(15)")]
        public string IP
        {
            get { return _iP; }
            set { base.SetPropertyValue("IP", value); }
        }

        private string _sesstionId;
        [DbType("nvarchar(50)")]
        public string SesstionId
        {
            get { return _sesstionId; }
            set { base.SetPropertyValue("SesstionId", value); }
        }

        private Guid _webSiteId;
        [DbType("uniqueidentifier")]
        public Guid WebSiteId
        {
            get
            {
                return this._webSiteId;
            }
            set
            {
                base.SetPropertyValue("WebSiteId", value);
                if (WebSite == null)
                    this.WebSite = new WebSite { Id = value };
            }
        }

        [Assosiation(PropName = "WebSiteId")]
        public WebSite WebSite { get; set; }

        private string _httpReferer;
        [IsNullable]
        [DbType("nchar(100)")]
        public string HttpReferer
        {
            get { return _httpReferer; }
            set { base.SetPropertyValue("HttpReferer", value); }
        }

        private DateTime _date;
        [DbType("datetime")]
        public DateTime Date
        {
            get { return _date; }
            set { base.SetPropertyValue("Date", value); }
        }

        private string _url;
        [IsNullable]
        [DbType("nvarchar(400)")]
        public string Url
        {
            get { return _url; }
            set { base.SetPropertyValue("Url", value); }
        }

        private Int32? _browserId;
        [IsNullable]
        [DbType("int")]
        public Int32? BrowserId
        {
            get
            {
                return this._browserId;
            }
            set
            {
                base.SetPropertyValue("BrowserId", value);
                if (Browser == null)
                {
                    if (value.HasValue)
                        this.Browser = new Browser { Id = value.Value };
                    else
                        this.Browser = null;
                }
            }
        }

        [Assosiation]
        public Browser Browser { get; set; }

        private string _path;
        [IsNullable]
        [DbType("nchar(200)")]
        public string Path
        {
            get { return _path; }
            set { base.SetPropertyValue("Path", value); }
        }

        private string _physicalPath;
        [IsNullable]
        [DbType("nchar(200)")]
        public string PhysicalPath
        {
            get { return _physicalPath; }
            set { base.SetPropertyValue("PhysicalPath", value); }
        }

        private bool? _isLocal;
        [IsNullable]
        [DbType("bit")]
        public bool? IsLocal
        {
            get { return _isLocal; }
            set { base.SetPropertyValue("IsLocal", value); }
        }

        private string _rawUrl;
        [IsNullable]
        [DbType("nchar(100)")]
        public string RawUrl
        {
            get { return _rawUrl; }
            set { base.SetPropertyValue("RawUrl", value); }
        }

        private Int32? _osId;
        [IsNullable]
        [DbType("int")]
        public Int32? OsId
        {
            get
            {
                return this._osId;
            }
            set
            {
                base.SetPropertyValue("OsId", value);

                if (OS == null)
                {
                    if (value.HasValue)
                        this.OS = new OS { Id = value.Value };
                    else
                        this.OS = null;
                }

            }
        }

        [Assosiation]
        public OS OS { get; set; }

        private Int32? _searchEngineId;
        [IsNullable]
        [DbType("int")]
        public Int32? SearchEngineId
        {
            get
            {
                return this._searchEngineId;
            }
            set
            {
                base.SetPropertyValue("SearchEngineId", value);
                if (SearchEngine == null)
                {
                    if (value.HasValue)
                        this.SearchEngine = new SearchEngine { Id = value.Value };
                    else
                        this.SearchEngine = null;
                }
            }
        }

        [Assosiation]
        public SearchEngine SearchEngine { get; set; }

        private bool? _fromMobile;
        [IsNullable]
        [DbType("bit")]
        public bool? FromMobile
        {
            get { return _fromMobile; }
            set { base.SetPropertyValue("FromMobile", value); }
        }

        private Guid? _accountId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? AccountId
        {
            get
            {
                return this._accountId;
            }
            set
            {
                base.SetPropertyValue("AccountId", value);
                if (Accounts == null)
                {
                    if (value.HasValue)
                        this.Accounts = new Accounts { Id = value.Value };
                    else
                        this.Accounts = null;
                }

            }
        }

        [Assosiation]
        public Accounts Accounts { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.SesstionId; }
        }
    }
}
