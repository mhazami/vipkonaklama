using System;
using Radyn.Framework;

namespace Radyn.Statistics.DataStructure
{
    [Serializable]
    [Schema("Statistics")]
    public sealed class IPReng : DataStructureBase<IPReng>
    {
        private Int32 _id;
        [Key(false)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _fromIP;
        [DbType("nchar(15)")]
        public string FromIP
        {
            get { return _fromIP; }
            set { base.SetPropertyValue("FromIP", value); }
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
                if (Country == null)
                    this.Country = new Country { Id = value };
            }
        }

        [Assosiation(PropName = "CountryId")]
        public Country Country { get; set; }

        private string _toIP;
        [DbType("nchar(15)")]
        public string ToIP
        {
            get { return _toIP; }
            set { base.SetPropertyValue("ToIP", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Country.Title; }
        }
    }
}
