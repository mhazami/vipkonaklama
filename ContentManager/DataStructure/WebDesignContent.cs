using System;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class WebDesignContent : DataStructureBase<WebDesignContent>
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

       

        private Int32 _contentId;
        [Key(false)]
        [DbType("int")]
        public Int32 ContentId
        {
            get
            {
                return this._contentId;
            }
            set
            {
                base.SetPropertyValue("ContentId", value);
                this.WebSiteContent = new ContentManager.DataStructure.Content { Id = value };
            }
        }

        [Assosiation(PropName = "ContentId")]
        public ContentManager.DataStructure.Content WebSiteContent { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.WebSiteContent.Title; }
        }
    }
}
