using System;
using Radyn.Framework;

namespace Radyn.EnterpriseNode.DataStructure
{
    [Serializable]
    [Schema("EnterpriseNode")]
    public sealed class EnterpriseNodeType : DataStructureBase<EnterpriseNodeType>
    {
        private Int32 _id;
        [Key(false)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [DbType("nvarchar(150)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
