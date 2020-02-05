using System;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class UserRole : DataStructureBase<UserRole>
    {
        private Guid _userId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid UserId
        {
            get
            {
                return this._userId;
            }
            set
            {
                base.SetPropertyValue("UserId", value);
                if (User == null)
                    this.User = new User { Id = value };
            }
        }

        [Assosiation(PropName = "UserId")]
        public User User { get; set; }

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
