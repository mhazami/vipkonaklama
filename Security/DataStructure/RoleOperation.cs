using System;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class RoleOperation : DataStructureBase<RoleOperation>
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
