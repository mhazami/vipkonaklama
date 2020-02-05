using System;
using System.Collections.Generic;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Description("System.Byte[]")]
    [Schema("Security")]
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

        private byte? _applicationID;
        [Layout(Caption = "System.Byte[]")]
        [IsNullable]
        [DbType("tinyint")]
        public byte? ApplicationID
        {
            get { return _applicationID; }
            set { base.SetPropertyValue("ApplicationID", value); }
        }

        private string _title;
        [Layout(Caption = "System.Byte[]")]
        [DbType("nvarchar(250)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _url;
        [Layout(Caption = "System.Byte[]")]
        [DbType("nvarchar(100)")]
        public string Url
        {
            get { return _url; }
            set { base.SetPropertyValue("Url", value); }
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
                    this.ParentMenu = value.HasValue ? new Menu { Id = value.Value } : null;
            }
        }

        [Assosiation(PropName = "ParentId")]
        public Menu ParentMenu { get; set; }

        private byte _order;
        [Layout(Caption = "System.Byte[]")]
        [DbType("tinyint")]
        public byte Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
        }

        private bool _display;
        [DbType("bit")]
        public bool Display
        {
            get { return _display; }
            set { base.SetPropertyValue("Display", value); }
        }

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

        private int? _menuGroupId;
        [DbType("int")]
        public int? MenuGroupId
        {
            get
            {
                return this._menuGroupId;
            }
            set
            {
                base.SetPropertyValue("MenuGroupId", value);
                if (MenuGroup == null)
                    this.MenuGroup = value.HasValue ? new MenuGroup { Id = (int)value } : null;
            }
        }

        [Assosiation(PropName = "MenuGroupId")]
        public MenuGroup MenuGroup { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string TitleWithParentTitle
        {
            get
            {
                if (this.ParentId == null || this.ParentMenu == null) return this.Title;
                return ParentMenu.Title + "/" + this.Title;
            }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<Menu> Children { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }


    }
}
