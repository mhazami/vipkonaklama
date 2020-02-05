using System;
using Radyn.Framework;

namespace Radyn.PhoneBook.DataStructure
{
    [Serializable]
    [Schema("PhoneBook")]
    public sealed class PersonPhone : DataStructureBase<PersonPhone>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
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

        private string _telNumber;
        [DbType("varchar(35)")]
        public string TelNumber
        {
            get { return _telNumber; }
            set { base.SetPropertyValue("TelNumber", value); }
        }

        private Int16 _phoneTypeId;
        [DbType("smallint")]
        public Int16 PhoneTypeId
        {
            get
            {
                return this._phoneTypeId;
            }
            set
            {
                base.SetPropertyValue("PhoneTypeId", value);
                if(PhoneType==null)
                this.PhoneType = new PhoneType { Id = value };
            }
        }

        [Assosiation(PropName = "PhoneTypeId")]
        public PhoneType PhoneType { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.TelNumber; }
        }
    }
}
