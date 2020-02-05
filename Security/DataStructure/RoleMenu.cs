using System;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class RoleMenu : DataStructureBase<RoleMenu>
    {
        private Guid _roleId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid RoleId
        {
            get
            {
                return this._roleId;
            }
            set
            {
                base.SetPropertyValue("RoleId", value);
                if (Role == null)
                    this.Role = new Role { Id = value };
            }
        }

        [Assosiation(PropName = "RoleId")]
        public Role Role { get; set; }

        private Guid _menuId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid MenuId
        {
            get
            {
                return this._menuId;
            }
            set
            {
                base.SetPropertyValue("MenuId", value);
                if (Menu == null)
                    this.Menu = new Menu { Id = value };
            }
        }

        [Assosiation(PropName = "MenuId")]
        public Menu Menu { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Menu.Title; }
        }
    }
}
