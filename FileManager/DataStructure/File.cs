using System;
using Radyn.Framework;

namespace Radyn.FileManager.DataStructure
{
    [Serializable]
    [Schema("FileManager")]
    public sealed class File : DataStructureBase
    {
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id { get; set; }

        [IsNullable]
        [DbType("nvarchar(150)")]
        public string FileName { get; set; }

        [DbType("nvarchar(500)")]
        public string ContentType { get; set; }

        [DbType("varchar(10)")]
        public string Extension { get; set; }

        [DbType("varbinary(max)")]
        public byte[] Content { get; set; }

        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? FolderId { get; set; }

        [IsNullable]
        [DbType("char(5)")]
        public string LanguageId { get; set; }

        [DbType("datetime")]
        public DateTime SaveDate { get; set; }

        private Guid? _CreatorId;

        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? CreatorId
        {
            get
            {
                return this._CreatorId;
            }
            set
            {
                this._CreatorId = value;
               
            }
        }
        [IsNullable]
        [DbType("nvarchar(1000)")]
        public string Tags { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public long MaxSize { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.FileName; }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string FullName
        {
            get { return this.FileName + "." + this.Extension; }
        }
    }
}
