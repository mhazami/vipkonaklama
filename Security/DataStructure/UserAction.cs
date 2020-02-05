using System;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class UserAction : DataStructureBase<UserAction>
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
