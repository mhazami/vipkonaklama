using System;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class UserPartials : DataStructureBase<UserPartials>
    {
        private Guid _userId;
        [DbType("uniqueidentifier")]
        public Guid UserId
        {
            get { return _userId; }
            set { base.SetPropertyValue("UserId", value); }
        }

        private Guid _partialId;
        [DbType("uniqueidentifier")]
        public Guid PartialId
        {
            get
            {
                return this._partialId;
            }
            set
            {
                base.SetPropertyValue("PartialId", value);
                if (Partials == null)
                    this.Partials = new Partials { Id = value };
            }
        }

        [Assosiation(PropName = "PartialId")]
        public Partials Partials { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return ""; }
        }
    }
}
