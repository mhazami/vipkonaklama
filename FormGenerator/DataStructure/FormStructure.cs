using System;
using System.Collections.Generic;
using Radyn.ContentManager.DataStructure;
using Radyn.FormGenerator.ModelView;
using Radyn.FormGenerator.Tools;
using Radyn.Framework;

namespace Radyn.FormGenerator.DataStructure
{
    [Serializable]
    [Schema("FormGenerator")]
    public sealed class FormStructure : DataStructureBase<FormStructure>
    {
        public FormStructure()
        {
            this.GetFormControl = new Dictionary<string, object>();
        }
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _structure;
        [DbType("ntext")]
        [IsNullable]
        [MultiLanguage]
        public string Structure
        {
            get { return _structure; }
            set { base.SetPropertyValue("Structure", value); }
        }



        private string _name;
        [DbType("nvarchar(200)")]
        public string Name
        {
            get { return _name; }
            set { base.SetPropertyValue("Name", value); }
        }


        

        private DateTime _createDate;
        [DbType("datetime")]
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { base.SetPropertyValue("CreateDate", value); }
        }

        private bool _enable;
        [DbType("bit")]
        public bool Enable
        {
            get { return _enable; }
            set { base.SetPropertyValue("Enable", value); }
        }

        private bool _isExternal;
        [DbType("bit")]
        public bool IsExternal
        {
            get { return _isExternal; }
            set { base.SetPropertyValue("IsExternal", value); }
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
                    this.Menu = value.HasValue ? new Menu { Id = value.Value } : null;
            }
        }

        [Assosiation(PropName = "MenuId")]
        //[Assosiation]
        public Menu Menu { get; set; }

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
                this.Container = value.HasValue ? new Container { Id = value.Value } : null;
            }
        }
        [Assosiation(PropName = "ContainerId")]
        //[Assosiation]
        public Container Container { get; set; }


        private string _link;
        [IsNullable]
        [DbType("nvarchar(300)")]
        public string Link
        {
            get { return _link; }
            set { base.SetPropertyValue("Link", value); }
        }
      


        [MultiLanguage]
        public string StructureFileId { get; set; }



        private Controls _controls;
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public Controls Controls
        {
            get
            {
                if (this._controls == null)
                    this._controls = new Controls();
                return _controls;
            }
            set { _controls = value; }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public Dictionary<string, Object> GetFormControl { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string FillErrors { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string RefId { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string ObjectName { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public FormState FormState { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string PostFormDataUrl { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string FormUrl { get; set; }



        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<FormData> FormDatas { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public bool IsUserForms { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Name; }
        }
    }
}
