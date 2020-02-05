using System;
using Radyn.Framework;

namespace Radyn.WebDesign.DataStructure
{
    [Serializable]
    [Schema("WebDesign")]
    public sealed class WebSiteAlias : DataStructureBase<WebSiteAlias>
    {

        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
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

        private string _url;
        [DbType("nvarchar(250)")]
        public string Url
        {
            get { return _url; }
            set { base.SetPropertyValue("Url", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.WebSite.Title; }
        }
    }
}
