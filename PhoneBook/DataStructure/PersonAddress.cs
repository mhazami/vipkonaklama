using System;
using Radyn.Framework;

namespace Radyn.PhoneBook.DataStructure
{
    [Serializable]
    [Schema("PhoneBook")]
    public sealed class PersonAddress : DataStructureBase<PersonAddress>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _address;
        [DbType("nvarchar(500)")]
        public string Address
        {
            get { return _address; }
            set { base.SetPropertyValue("Address", value); }
        }

        private Guid _personId;
        [DbType("uniqueidentifier")]
        public Guid PersonId
        {
            get
            {
                return this._personId;
            }
            set
            {
                base.SetPropertyValue("PersonId", value);
                this.Person = new Person { Id = value };
            }
        }

        [Assosiation]
        public Person Person { get; set; }

        private Int16 _addressTypeId;
        [DbType("smallint")]
        public Int16 AddressTypeId
        {
            get
            {
                return this._addressTypeId;
            }
            set
            {
                base.SetPropertyValue("AddressTypeId", value);
                if(AddressType==null)
                this.AddressType = new AddressType { Id = value };
            }
        }

        [Assosiation(PropName = "AddressTypeId")]
        public AddressType AddressType { get; set; }

        private bool _isDefault;
        [DbType("bit")]
        public bool IsDefault
        {
            get { return _isDefault; }
            set { base.SetPropertyValue("IsDefault", value); }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Address; }
        }
    }
}
