using System;
using Radyn.Framework;

namespace Radyn.Common.DataStructure
{
    [Serializable]
    [Schema("Common")]
    public sealed class City : DataStructureBase<City>
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

        private bool _enable;
        [DbType("bit")]
        public bool Enable
        {
            get { return _enable; }
            set { base.SetPropertyValue("Enable", value); }
        }
        private Int32 _provinceId;
        [DbType("int")]
        public Int32 ProvinceId
        {
            get
            {
                return this._provinceId;
            }
            set
            {
                base.SetPropertyValue("ProvinceId", value);
                if (Province == null)
                    this.Province = new Province() { Id = value };
            }
        }

        [Assosiation(PropName = "ProvinceId")]
        //[Assosiation]
        public Province Province { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
