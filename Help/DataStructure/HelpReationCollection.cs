using System;
using Radyn.Framework;

namespace Radyn.Help.DataStructure
{
    [Serializable]
    [Schema("Help")]
    public sealed class HelpReationCollection : DataStructureBase<HelpReationCollection>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return ""; }
        }
    }
}
