using System;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class MenuGroup : DataStructureBase<MenuGroup>
    {
        private Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _name;
        [DbType("nvarchar(70)")]
        public string Name
        {
            get { return _name; }
            set { base.SetPropertyValue("Name", value); }
        }

        private Guid _operationId;
        [DbType("uniqueidentifier")]
        public Guid OperationId
        {
            get
            {
                return this._operationId;
            }
            set
            {
                base.SetPropertyValue("OperationId", value);
                if (Operation == null)
                    this.Operation = new Operation { Id = value };
            }
        }

        [Assosiation(PropName = "OperationId")]
        public Operation Operation { get; set; }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }
        private Guid? _imageId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ImageId
        {
            get
            {
                return this._imageId;
            }
            set
            {
                base.SetPropertyValue("ImageId", value);
                if (File == null)
                    this.File = value.HasValue ? new File { Id = value.Value } : null;
            }
        }

        [Assosiation(PropName = "ImageId")]
        public File File { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Name; }
        }
    }
}
