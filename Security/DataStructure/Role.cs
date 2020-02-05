using System;
using Radyn.Framework;

namespace Radyn.Security.DataStructure
{
    [Serializable]
    [Schema("Security")]
    public sealed class Role : DataStructureBase<Role>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _name;
        [DbType("nvarchar(50)")]
        [Filter]
        [Layout(Caption = "نام سمت")]
        public string Name
        {
            get { return _name; }
            set { base.SetPropertyValue("Name", value); }
        }

        private bool _isAdmin;
        [DbType("bit")]
        [Filter(SqlCompareOperator.Equal)]
        [Layout(Caption = "مدیر است؟")]
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { base.SetPropertyValue("IsAdmin", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Name; }
        }
    }
}
