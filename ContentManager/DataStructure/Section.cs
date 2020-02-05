using System;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class Section : DataStructureBase<Section>
    {
        private Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [DbType("nvarchar(50)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private Int32? _parentSectionId;
        [IsNullable]
        [DbType("int")]
        public Int32? ParentSectionId
        {
            get
            {
                return this._parentSectionId;
            }
            set
            {
                base.SetPropertyValue("ParentSectionId", value);
                if (ParentSection == null)
                {
                    if (value.HasValue)
                        this.ParentSection = new Section { Id = value.Value };
                    else
                        this.ParentSection = null;
                }
            }
        }

        [Assosiation(PropName = "ParentSectionId")]
        public Section ParentSection { get; set; }

        private bool _isMenu;
        [DbType("bit")]
        public bool IsMenu
        {
            get { return _isMenu; }
            set { base.SetPropertyValue("IsMenu", value); }
        }

        private Guid? _menuId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? MenuId
        {
            get
            {
                return this._menuId;
            }
            set
            {
                base.SetPropertyValue("MenuId", value);
                if (Menu == null)
                {
                    if (value.HasValue)
                        this.Menu = new Menu { Id = value.Value };
                    else
                        this.Menu = null;

                }
            }
        }

        [Assosiation(PropName = "MenuId")]
        public Menu Menu { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
