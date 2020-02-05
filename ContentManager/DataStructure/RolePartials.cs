using System;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class RolePartials : DataStructureBase<RolePartials>
    {
        private Guid _roleId;
        [DbType("uniqueidentifier")]
        public Guid RoleId
        {
            get { return _roleId; }
            set { base.SetPropertyValue("RoleId", value); }
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
