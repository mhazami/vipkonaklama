using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class WebDesignPages : DataStructureBase<WebDesignPages>
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


        private int _pagesId;
        [Key(false)]
        [DbType("int")]
        public int PagesId
        {
            get
            {
                return this._pagesId;
            }
            set
            {
                base.SetPropertyValue("PagesId", value);
                this.Pages = new Pages() { Id = value };
            }
        }

        [Assosiation(PropName = "PagesId")]
        public Pages Pages { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Pages.Title; }
        }
    }
}
