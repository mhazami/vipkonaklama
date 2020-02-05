using System;
using System.Collections.Generic;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class Menu : DataStructureBase<Menu>
    {
        public Menu()
        {
            Children = new List<Menu>();
        }
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _text;
        [DbType("nvarchar(80)")]
        [MultiLanguage]
        public string Text
        {
            get { return _text; }
            set { base.SetPropertyValue("Text", value); }
        }

        private string _link;
        [IsNullable]
        [DbType("nvarchar(300)")]
        public string Link
        {
            get { return _link; }
            set { base.SetPropertyValue("Link", value); }
        }

        private Guid? _imageUrl;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ImageUrl
        {
            get { return _imageUrl; }
            set { base.SetPropertyValue("ImageUrl", value); }
        }

        private Guid? _parentId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ParentId
        {
            get
            {
                return this._parentId;
            }
            set
            {
                base.SetPropertyValue("ParentId", value);
                if (ParentMenu == null)
                {
                    if (value.HasValue)
                        this.ParentMenu = new Menu { Id = value.Value };
                    else
                        this.ParentMenu = null;
                }
            
            }
        }

        [Assosiation(PropName = "ParentId")]
        public Menu ParentMenu { get; set; }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

        private Int32 _order;
        [DbType("int")]
        public Int32 Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
        }

    
        private bool _isExternal;
        [DbType("bit")]
        public bool IsExternal
        {
            get { return _isExternal; }
            set { base.SetPropertyValue("IsExternal", value); }
        }

        private bool _isVertical;
        [DbType("bit")]
        public bool IsVertical
        {
            get { return _isVertical; }
            set { base.SetPropertyValue("IsVertical", value); }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public bool Selected { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<Menu> Children { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public Content Content { get; set; }
      

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return ""; }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string LangId { get; set; }

    }
}
