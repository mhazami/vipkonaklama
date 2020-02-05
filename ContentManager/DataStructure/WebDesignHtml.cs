using System;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class WebDesignHtml : DataStructureBase<WebDesignHtml>
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

       

        private Guid _htmlDesginId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid HtmlDesginId
        {
            get
            {
                return this._htmlDesginId;
            }
            set
            {
                base.SetPropertyValue("HtmlDesginId", value);
                this.HtmlDesgin = new HtmlDesgin { Id = value };
            }
        }

        [Assosiation(PropName = "HtmlDesginId")]
        public HtmlDesgin HtmlDesgin { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return String.Empty; }
        }
    }
}
