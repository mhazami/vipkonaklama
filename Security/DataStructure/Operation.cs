using System;
using System.Collections.Generic;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class Operation : DataStructureBase<Operation>
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
        [DbType("nvarchar(70)")]
        [Layout(Caption = "عنوان")]
        [Filter]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        [Layout(Caption = "فعال")]
        [Filter(SqlCompareOperator.Equal)]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

        private byte? _order;
        [IsNullable]
        [DbType("tinyint")]
        public byte? Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
        }

        private bool _expand;
        [DbType("bit")]
        public bool Expand
        {
            get { return _expand; }
            set { base.SetPropertyValue("Expand", value); }
        }

        private Guid? _logoId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? LogoId
        {
            get
            {
                return this._logoId;
            }
            set
            {
                base.SetPropertyValue("LogoId", value);
                if (File == null)
                {
                    if (value.HasValue)
                        this.File = new File { Id = value.Value };
                    else
                        this.File = null;
                }
            }
        }

        [Assosiation(PropName = "LogoId")]
        public File File { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<dynamic> MenuGroupList { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<dynamic> ParentMenus { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<Menu> AllMenus { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public Dictionary<int, List<Menu>> GroupMenuList { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
