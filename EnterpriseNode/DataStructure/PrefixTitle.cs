using System;
using Radyn.Framework;

namespace Radyn.EnterpriseNode.DataStructure
{
    [Serializable]
    [Schema("EnterpriseNode")]
    public sealed class PrefixTitle : DataStructureBase<PrefixTitle>
    {
        private byte _id;
        [Key(true)]
        [DbType("tinyint")]
        public byte Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [IsNullable]
        [MultiLanguage]
        [DbType("nvarchar(50)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _decription;
        [IsNullable]
        [DbType("nvarchar(500)")]
        public string Decription
        {
            get { return _decription; }
            set { base.SetPropertyValue("Decription", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
