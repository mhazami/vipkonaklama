using System;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class WebDesignContainer : DataStructureBase<WebDesignContainer>
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

     

        private Guid _containerId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid ContainerId
        {
            get
            {
                return this._containerId;
            }
            set
            {
                base.SetPropertyValue("ContainerId", value);
                this.WebSiteContainer = new ContentManager.DataStructure.Container { Id = value };
            }
        }

        [Assosiation(PropName= "ContainerId")]
        public ContentManager.DataStructure.Container WebSiteContainer { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.WebSiteContainer.DescriptionField; }
        }
    }
}
