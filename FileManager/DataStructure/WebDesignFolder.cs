using System;
using Radyn.Framework;

namespace Radyn.FileManager.DataStructure
{
    [Serializable]
    [Schema("FileManager")]
    public sealed class WebDesignFolder : DataStructureBase<WebDesignFolder>
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

       

        private Guid _folderId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid FolderId
        {
            get
            {
                return this._folderId;
            }
            set
            {
                base.SetPropertyValue("FolderId", value);
                this.WebSiteFolder = new Radyn.FileManager.DataStructure.Folder { Id = value };
            }
        }

        [Assosiation(PropName = "FolderId")]
        public Radyn.FileManager.DataStructure.Folder WebSiteFolder { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.WebSiteFolder.Title; }
        }
    }
}
