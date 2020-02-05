using System;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class Action : DataStructureBase<Action>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Guid _menuId;
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

        private string _name;
        [DbType("nvarchar(50)")]
        public string Name
        {
            get { return _name; }
            set { base.SetPropertyValue("Name", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Name; }
        }
    }
}
