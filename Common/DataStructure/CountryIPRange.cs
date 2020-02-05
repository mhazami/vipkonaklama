using System;
using Radyn.Framework;

namespace Radyn.Common.DataStructure
{
    [Serializable]
    [Schema("Common")]
    public sealed class CountryIPRange : DataStructureBase<CountryIPRange>
    {
        private Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private Int32 _countryId;
        [DbType("int")]
        public Int32 CountryId
        {
            get
            {
                return this._countryId;
            }
            set
            {
                base.SetPropertyValue("CountryId", value);
                if(Country == null)
                this.Country = new Country { Id = value };
            }
        }

        [Assosiation(PropName = "CountryId")]
        //[Assosiation]
        public Country Country { get; set; }

        private string _startRange;
        [DbType("char(15)")]
        public string StartRange
        {
            get { return _startRange; }
            set { base.SetPropertyValue("StartRange", value); }
        }

        private string _endRange;
        [DbType("char(15)")]
        public string EndRange
        {
            get { return _endRange; }
            set { base.SetPropertyValue("EndRange", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Country.Name; }
        }
    }
}
