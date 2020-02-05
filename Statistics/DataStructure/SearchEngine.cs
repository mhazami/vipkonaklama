using System;
using Radyn.Framework;

namespace Radyn.Statistics.DataStructure
{
    [Serializable]
    [Schema("Statistics")]
    public sealed class SearchEngine : DataStructureBase<SearchEngine>
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
        [IsNullable]
        [DbType("nchar(10)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private Guid? _logoId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? LogoId
        {
            get { return _logoId; }
            set { base.SetPropertyValue("LogoId", value); }
        }

        private string _ipAddress;
        [IsNullable]
        [DbType("nchar(10)")]
        public string IpAddress
        {
            get { return _ipAddress; }
            set { base.SetPropertyValue("IpAddress", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
