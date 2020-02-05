using System;
using Radyn.ContentManager.Definition;
using Radyn.Framework;
using Radyn.Security.DataStructure;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class Partials : DataStructureBase<Partials>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [DbType("nvarchar(150)")]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _url;
        [DbType("nvarchar(200)")]
        public string Url
        {
            get { return _url; }
            set { base.SetPropertyValue("Url", value); }
        }

        private string _refId;
        [DbType("varchar(100)")]
        public string RefId
        {
            get { return _refId; }
            set { base.SetPropertyValue("RefId", value); }
        }

        private string _contextName;
        [IsNullable]
        [DbType("nvarchar(100)")]
        public string ContextName
        {
            get { return _contextName; }
            set { base.SetPropertyValue("ContextName", value); }
        }

        private Guid _operationId;
        [DbType("uniqueidentifier")]
        public Guid OperationId
        {
            get { return _operationId; }
            set
            {
                base.SetPropertyValue("OperationId", value);
                if (Operation == null)
                    this.Operation = new Operation() { Id = value };
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

        private Guid? _containerId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ContainerId
        {
            get { return _containerId; }
            set
            {
                base.SetPropertyValue("ContainerId", value);
                if (Container == null)
                    this.Container = value.HasValue ? new Container() { Id = value.Value } : null;
            }
        }

        [Assosiation(PropName = "ContainerId")]
        public Container Container { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public Enums.PartialTypes Type { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string StringId { get; set; }

      [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string Html { get; set; }

        private string _image;
       [MultiLanguage]
        public string Image
        {
            get { return _image; }
            set { base.SetPropertyValue("Image", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
