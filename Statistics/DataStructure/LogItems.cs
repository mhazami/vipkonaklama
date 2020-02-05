using System;
using Radyn.Framework;

namespace Radyn.Statistics.DataStructure
{
    [Serializable]
    [Schema("Statistics")]
    public sealed class LogItems : DataStructureBase<LogItems>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Guid? _requestId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? RequestId
        {
            get
            {
                return this._requestId;
            }
            set
            {
                base.SetPropertyValue("RequestId", value);
                if (Log == null)
                {
                    if (value.HasValue)
                        this.Log = new Log { Id = value.Value };
                    else
                        this.Log = null;
                }
            }
        }

        [Assosiation(PropName = "RequestId")]
        public Log Log { get; set; }

        private string _userAgent;
        [IsNullable]
        [DbType("nchar(10)")]
        public string UserAgent
        {
            get { return _userAgent; }
            set { base.SetPropertyValue("UserAgent", value); }
        }

        private string _userHostName;
        [IsNullable]
        [DbType("nchar(10)")]
        public string UserHostName
        {
            get { return _userHostName; }
            set { base.SetPropertyValue("UserHostName", value); }
        }

        private string _userHostAddress;
        [IsNullable]
        [DbType("nchar(10)")]
        public string UserHostAddress
        {
            get { return _userHostAddress; }
            set { base.SetPropertyValue("UserHostAddress", value); }
        }

        private string _userLanguages;
        [IsNullable]
        [DbType("nchar(10)")]
        public string UserLanguages
        {
            get { return _userLanguages; }
            set { base.SetPropertyValue("UserLanguages", value); }
        }

        private string _httpHost;
        [IsNullable]
        [DbType("nchar(10)")]
        public string HttpHost
        {
            get { return _httpHost; }
            set { base.SetPropertyValue("HttpHost", value); }
        }

        private string _serverName;
        [IsNullable]
        [DbType("nchar(10)")]
        public string ServerName
        {
            get { return _serverName; }
            set { base.SetPropertyValue("ServerName", value); }
        }

        private string _serverPort;
        [IsNullable]
        [DbType("nchar(10)")]
        public string ServerPort
        {
            get { return _serverPort; }
            set { base.SetPropertyValue("ServerPort", value); }
        }

        private string _serverSoftware;
        [IsNullable]
        [DbType("nchar(10)")]
        public string ServerSoftware
        {
            get { return _serverSoftware; }
            set { base.SetPropertyValue("ServerSoftware", value); }
        }

        private string _remotHost;
        [IsNullable]
        [DbType("nchar(10)")]
        public string RemotHost
        {
            get { return _remotHost; }
            set { base.SetPropertyValue("RemotHost", value); }
        }

        private string _remotPort;
        [IsNullable]
        [DbType("nchar(10)")]
        public string RemotPort
        {
            get { return _remotPort; }
            set { base.SetPropertyValue("RemotPort", value); }
        }

        private string _remotAddress;
        [IsNullable]
        [DbType("nchar(10)")]
        public string RemotAddress
        {
            get { return _remotAddress; }
            set { base.SetPropertyValue("RemotAddress", value); }
        }

        private string _localAddress;
        [IsNullable]
        [DbType("nchar(10)")]
        public string LocalAddress
        {
            get { return _localAddress; }
            set { base.SetPropertyValue("LocalAddress", value); }
        }

        private string _httpCookie;
        [IsNullable]
        [DbType("nchar(10)")]
        public string HttpCookie
        {
            get { return _httpCookie; }
            set { base.SetPropertyValue("HttpCookie", value); }
        }

        private string _queryString;
        [IsNullable]
        [DbType("nchar(10)")]
        public string QueryString
        {
            get { return _queryString; }
            set { base.SetPropertyValue("QueryString", value); }
        }

        private string _certSubject;
        [IsNullable]
        [DbType("nchar(10)")]
        public string CertSubject
        {
            get { return _certSubject; }
            set { base.SetPropertyValue("CertSubject", value); }
        }

        private string _certServerSubject;
        [IsNullable]
        [DbType("nchar(10)")]
        public string CertServerSubject
        {
            get { return _certServerSubject; }
            set { base.SetPropertyValue("CertServerSubject", value); }
        }

        private Int32? _screenPixelHeight;
        [IsNullable]
        [DbType("int")]
        public Int32? ScreenPixelHeight
        {
            get { return _screenPixelHeight; }
            set { base.SetPropertyValue("ScreenPixelHeight", value); }
        }

        private Int32? _screenPixelWith;
        [IsNullable]
        [DbType("int")]
        public Int32? ScreenPixelWith
        {
            get { return _screenPixelWith; }
            set { base.SetPropertyValue("ScreenPixelWith", value); }
        }

        private string _serverAddress;
        [IsNullable]
        [DbType("nchar(10)")]
        public string ServerAddress
        {
            get { return _serverAddress; }
            set { base.SetPropertyValue("ServerAddress", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.ServerAddress; }
        }
    }
}
