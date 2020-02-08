using System;
using Radyn.Framework;

namespace Radyn.Common.DataStructure
{
    [Serializable]
    [Schema("Common")]
    public sealed class Parish : DataStructureBase<Parish>
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
        [DbType("nvarchar(100)")]
        public string Name
        {
            get { return _name; }
            set { base.SetPropertyValue("Name", value); }
        }

      
        private Int32 _cityId;
        [DbType("int")]
        public Int32 CityId
        {
            get
            {
                return this._cityId;
            }
            set
            {
                base.SetPropertyValue("CityId", value);
            }
        }

        [Assosiation(PropName = "CityId")]
        public City City { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Name; }
        }
    }
}
