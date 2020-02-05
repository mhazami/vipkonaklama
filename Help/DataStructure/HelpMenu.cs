using System;
using Radyn.Framework;
using Radyn.Security.DataStructure;

namespace Radyn.Help.DataStructure
{
    [Serializable]
    [Schema("Help")]
    public sealed class HelpMenu : DataStructureBase<HelpMenu>
    {
        private Guid _helpId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid HelpId
        {
            get { return _helpId; }
            set { base.SetPropertyValue("HelpId", value); }
        }

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