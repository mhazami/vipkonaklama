using System;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class WebDesignMenuHtml : DataStructureBase<WebDesignMenuHtml>
    {
        private Guid _webSiteId;
        [Key(false)]
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
                
            }
        }

      

        private Guid _menuHtmlId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid MenuHtmlId
        {
            get
            {
                return this._menuHtmlId;
            }
            set
            {
                base.SetPropertyValue("MenuHtmlId", value);
                if (WebSiteMenuHtml == null)
                    this.WebSiteMenuHtml = new ContentManager.DataStructure.MenuHtml { Id = value };
            }
        }

        [Assosiation(PropName = "MenuHtmlId")]
        public ContentManager.DataStructure.MenuHtml WebSiteMenuHtml { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return ""; }
        }
    }
}
