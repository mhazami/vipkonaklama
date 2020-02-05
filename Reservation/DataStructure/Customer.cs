using Radyn.Common.DataStructure;
using Radyn.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyn.Reservation.DataStructure
{
    [Serializable]
    [Schema("Reservation")]

    public sealed class Customer : DataStructureBase<Customer>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _firstName;
        [DbType("nvarchar(50)")]
        public string FirstName
        {
            get { return _firstName; }
            set { base.SetPropertyValue("FirstName", value); }
        }

        private string _lastName;
        [DbType("nvarchar(50)")]
        public string LastName
        {
            get { return _lastName; }
            set { base.SetPropertyValue("LastName", value); }
        }

        private string _phoneNumber;
        [DbType("char(11)")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { base.SetPropertyValue("PhoneNumber", value); }
        }

        private string _email;
        [DbType("nvarchar(100)")]
        public string Email
        {
            get { return _email; }
            set { base.SetPropertyValue("Email", value); }
        }

        private int _countryId;
        [DbType("char(11)")]
        public int CountryId
        {
            get { return _countryId; }
            set { base.SetPropertyValue("CountryId", value); }
        }
        [Assosiation(PropName = "CountryId")]
        public Country Country { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField { get { return $"{this.FirstName} {this.LastName}"; } }
    }
}
