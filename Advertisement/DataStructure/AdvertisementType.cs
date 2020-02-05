using System;
using Radyn.Framework;

namespace Radyn.Advertisements.DataStructure
{
    [Serializable]
    [Schema("Advertisement")]
    public sealed class AdvertisementType : DataStructureBase<AdvertisementType>
    {
        private Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _type;
        [DbType("nvarchar(50)")]
        public string Type
        {
            get { return _type; }
            set { base.SetPropertyValue("Type", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Type; }
        }
    }
}
