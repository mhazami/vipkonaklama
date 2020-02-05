using System;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class UserOperation : DataStructureBase<UserOperation>
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

        private Guid _operationId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid OperationId
        {
            get
            {
                return this._operationId;
            }
            set
            {
                base.SetPropertyValue("OperationId", value);
                if (Operation == null)
                    this.Operation = new Operation { Id = value };
            }
        }

        [Assosiation(PropName = "OperationId")]
        public Operation Operation { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Operation.Title; }
        }
    }
}
