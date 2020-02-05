using System;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class OperationMenu : DataStructureBase<OperationMenu>
    {
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
