using System;
using Radyn.Framework;

namespace Radyn.Statistics.DataStructure
{
    [Serializable]
    [Schema("Statistics")]
    public sealed class Accounts : DataStructureBase<Accounts>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _userName;
        [IsNullable]
        [DbType("nvarchar(50)")]
        public string UserName
        {
            get { return _userName; }
            set { base.SetPropertyValue("UserName", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return UserName; }
        }
    }
}
