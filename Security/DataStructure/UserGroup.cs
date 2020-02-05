using System;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class UserGroup : DataStructureBase<UserGroup>
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


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Group.Name; }
        }
    }
}
