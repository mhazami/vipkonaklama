using System;
using Radyn.Framework;

namespace Radyn.Gallery.DataStructure
{
    [Serializable]
    [Schema("Gallery")]
    public sealed class Gallery : DataStructureBase<Gallery>
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
        [IsNullable]
        [DbType("nvarchar(150)")]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private Guid? _parentGallery;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ParentGallery
        {
            get
            {
                return this._parentGallery;
            }
            set
            {
                base.SetPropertyValue("ParentGallery", value);

                if (Parent == null)
                {
                    if (value.HasValue)
                        this.Parent = new Gallery { Id = value.Value };
                    else
                        this.Parent = null;
                }

            }
        }

        [Assosiation(PropName = "ParentGallery")]
        //[Assosiation]
        public Gallery Parent { get; set; }

        private string _createDate;
        [DbType("char(10)")]
        public string CreateDate
        {
            get { return _createDate; }
            set { base.SetPropertyValue("CreateDate", value); }
        }

        private Guid? _creator;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? Creator
        {
            get
            {
                return this._creator;
            }
            set
            {
                base.SetPropertyValue("Creator", value);
                if (EnterpriseNode == null)
                    this.EnterpriseNode = value.HasValue ? new EnterpriseNode.DataStructure.EnterpriseNode { Id = value.Value } : null;
            }
        }

        [Assosiation(PropName = "Creator")]
        //[Assosiation]
        public EnterpriseNode.DataStructure.EnterpriseNode EnterpriseNode { get; set; }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

        private Guid? _thumbnail;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? Thumbnail
        {
            get { return _thumbnail; }
            set { base.SetPropertyValue("Thumbnail", value); }
        }

        private bool _isExternal;
        [DbType("bit")]
        public bool IsExternal
        {
            get { return _isExternal; }
            set { base.SetPropertyValue("IsExternal", value); }
        }

        private int _order;
        [DbType("int")]
        public int Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
        }



        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
