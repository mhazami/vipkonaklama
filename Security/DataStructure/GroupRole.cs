using System;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class GroupRole : DataStructureBase<GroupRole>
    {
        private Guid _groupId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid GroupId
        {
            get
            {
                return this._groupId;
            }
            set
            {
                base.SetPropertyValue("GroupId", value);
                if (Group == null)
                    this.Group = new Group { Id = value };
            }
        }

        [Assosiation(PropName = "GroupId")]
        public Group Group { get; set; }

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


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Role.Name; }
        }
    }
}
