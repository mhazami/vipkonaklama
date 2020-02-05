using System;
using Radyn.Framework;

namespace Radyn.PhoneBook.DataStructure
{
    [Serializable]
    [Schema("PhoneBook")]
    public sealed class AddressType : DataStructureBase<AddressType>
    {
        private Int16 _id;
        [Key(true)]
        [DbType("smallint")]
        public Int16 Id
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


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
