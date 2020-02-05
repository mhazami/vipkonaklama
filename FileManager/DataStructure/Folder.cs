using System;
using Radyn.Framework;

namespace Radyn.FileManager.DataStructure
{
    [Serializable]
    [Schema("FileManager")]
    public sealed class Folder : DataStructureBase
    {
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id { get; set; }

        [DbType("nvarchar(150)")]
        public string Title { get; set; }

        [DbType("bit")]
        public bool IsExternal { get; set; }

        private Guid? _ParentFolderId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ParentFolderId
        {
            get
            {
                return this._ParentFolderId;
            }
            set
            {
                this._ParentFolderId = value;
                if (ParentFolder == null)
                {
                    if (value.HasValue)
                        this.ParentFolder = new Folder { Id = value.Value };
                    else
                        this.ParentFolder = null;
                }
            }
        }

        [Assosiation(PropName = "ParentFolderId")]
        //[Assosiation]
        public Folder ParentFolder { get; set; }

        [DbType("datetime")]
        public DateTime CreateDate { get; set; }

       
        [DbType("uniqueidentifier")]
        [IsNullable]
        public Guid? OwnerId { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
        
        
    }
}
