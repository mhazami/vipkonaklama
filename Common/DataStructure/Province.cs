using System;
using Radyn.Framework;

namespace Radyn.Common.DataStructure
{
    [Serializable]
    [Schema("Common")]
    public sealed class Province : DataStructureBase<Province>
    {
        private Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [DbType("nvarchar(100)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
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
        //[Assosiation]
        public Country Country { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
