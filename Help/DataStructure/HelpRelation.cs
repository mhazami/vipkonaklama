using System;
using Radyn.Framework;

namespace Radyn.Help.DataStructure
{
    [Serializable]
    [Schema("Help")]
    public sealed class HelpRelation : DataStructureBase<HelpRelation>
    {
        private Guid _helpRelationCollectionId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid HelpRelationCollectionId
        {
            get
            {
                return this._helpRelationCollectionId;
            }
            set
            {
                base.SetPropertyValue("HelpRelationCollectionId", value);
                if (HelpReationCollection == null)
                    this.HelpReationCollection = new HelpReationCollection { Id = value };
            }
        }

        [Assosiation(PropName = "HelpRelationCollectionId")]
        public HelpReationCollection HelpReationCollection { get; set; }

        private Guid _helpId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid HelpId
        {
            get
            {
                return this._helpId;
            }
            set
            {
                base.SetPropertyValue("HelpId", value);
                if (Help == null)
                    this.Help = new Radyn.Help.DataStructure.Help { Id = value };
            }
        }

        [Assosiation(PropName = "HelpId")]
        public Radyn.Help.DataStructure.Help Help { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Help.DefaultTitle; }
        }
    }
}
