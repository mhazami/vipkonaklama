using System;
using Radyn.Framework;

namespace Radyn.Statistics.DataStructure
{
    [Serializable]
    [Schema("Statistics")]
    public sealed class OS : DataStructureBase<OS>
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
        [DbType("nvarchar(50)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _latinName;
        [DbType("nchar(50)")]
        public string LatinName
        {
            get { return _latinName; }
            set { base.SetPropertyValue("LatinName", value); }
        }

        private Guid? _logoId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? LogoId
        {
            get { return _logoId; }
            set { base.SetPropertyValue("LogoId", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
