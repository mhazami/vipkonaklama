using System;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class RoleAction : DataStructureBase<RoleAction>
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

        private Guid _actionId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid ActionId
        {
            get
            {
                return this._actionId;
            }
            set
            {
                base.SetPropertyValue("ActionId", value);
                if (Action == null)
                    this.Action = new Action { Id = value };
            }
        }

        [Assosiation(PropName = "ActionId")]
        public Action Action { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Action.Name; }
        }
    }
}
