using System;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class WebDesignMenu : DataStructureBase<WebDesignMenu>
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

      

        private Guid _menuId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid MenuId
        {
            get
            {
                return this._menuId;
            }
            set
            {
                base.SetPropertyValue("MenuId", value);
                this.WebSiteMenu = new Radyn.ContentManager.DataStructure.Menu { Id = value };
            }
        }

        [Assosiation(PropName = "MenuId")]
        public Radyn.ContentManager.DataStructure.Menu WebSiteMenu { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.WebSiteMenu.Text; }
        }
    }
}
