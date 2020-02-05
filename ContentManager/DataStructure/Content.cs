using System;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class Content : DataStructureBase<Content>
    {
        private Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _pageName;
        [DbType("nvarchar(200)")]
        public string PageName
        {
            get { return _pageName; }
            set { base.SetPropertyValue("PageName", value); }
        }

        private string _title;
        [IsNullable]
        [DbType("nvarchar(500)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _keyword;
        [IsNullable]
        [DbType("nvarchar(500)")]
        public string Keyword
        {
            get { return _keyword; }
            set { base.SetPropertyValue("Keyword", value); }
        }

        private string _description;
        [IsNullable]
        [DbType("nvarchar(1000)")]
        public string Description
        {
            get { return _description; }
            set { base.SetPropertyValue("Description", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }
        private bool _isExternal;
        [DbType("bit")]
        public bool IsExternal
        {
            get { return _isExternal; }
            set { base.SetPropertyValue("IsExternal", value); }
        }
        private string _startDate;
        [IsNullable]
        [DbType("char(10)")]
        public string StartDate
        {
            get { return _startDate; }
            set { base.SetPropertyValue("StartDate", value); }
        }

        private string _expireDate;
        [IsNullable]
        [DbType("char(10)")]
        public string ExpireDate
        {
            get { return _expireDate; }
            set { base.SetPropertyValue("ExpireDate", value); }
        }

        private string _link;
        [IsNullable]
        [DbType("nvarchar(300)")]
        public string Link
        {
            get { return _link; }
            set { base.SetPropertyValue("Link", value); }
        }

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

        private Int32 _visitCount;
        [DbType("int")]
        public Int32 VisitCount
        {
            get { return _visitCount; }
            set { base.SetPropertyValue("VisitCount", value); }
        }

        private string _text;
        [IsNullable]
        [DbType("ntext")]
        public string Text
        {
            get { return _text; }
            set { base.SetPropertyValue("Text", value); }
        }

        private string _subject;
        [IsNullable]
        [DbType("nvarchar(500)")]
        public string Subject
        {
            get { return _subject; }
            set { base.SetPropertyValue("Subject", value); }
        }

        private string _abstract;
        [IsNullable]
        [DbType("nvarchar(max)")]
        public string Abstract
        {
            get { return _abstract; }
            set { base.SetPropertyValue("Abstract", value); }
        }

        private Int32? _sectionId;
        [IsNullable]
        [DbType("int")]
        public Int32? SectionId
        {
            get
            {
                return this._sectionId;
            }
            set
            {
                base.SetPropertyValue("SectionId", value);
                if (Section == null)
                {
                    if (value.HasValue)
                        this.Section = new Section { Id = value.Value };
                    else
                        this.Section = null;
                }
            }
        }

        [Assosiation(PropName = "SectionId")]
        public Section Section { get; set; }

        private bool _isSection;
        [DbType("bit")]
        public bool IsSection
        {
            get { return _isSection; }
            set { base.SetPropertyValue("IsSection", value); }
        }

        private bool _hasContainer;
        [DbType("bit")]
        public bool HasContainer
        {
            get { return _hasContainer; }
            set { base.SetPropertyValue("HasContainer", value); }
        }

        private Guid? _containerId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ContainerId
        {
            get
            {
                return this._containerId;
            }
            set
            {
                base.SetPropertyValue("ContainerId", value);
                if (Container == null)
                {
                    if (value.HasValue)
                        this.Container = new Container { Id = value.Value };
                    else
                        this.Container = null;
                }
            }
        }

        [Assosiation(PropName = "ContainerId")]
        public Container Container { get; set; }

        private string _userScript;
        [IsNullable]
        [DbType("nvarchar(MAX)")]
        public string UserScript
        {
            get { return _userScript; }
            set { base.SetPropertyValue("UserScript", value); }
        }

        

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
