using System;
using Radyn.Framework;

namespace Radyn.EnterpriseNode.DataStructure
{
    [Serializable]
    [Schema("EnterpriseNode")]
    public sealed class RealEnterpriseNode : DataStructureBase<RealEnterpriseNode>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get
            {
                return this._id;
            }
            set
            {
                base.SetPropertyValue("Id", value);
                if (EnterpriseNode == null)
                    this.EnterpriseNode = new EnterpriseNode { Id = value };
            }
        }

        [Assosiation]
        public EnterpriseNode EnterpriseNode { get; set; }

        private string _firstName;
        [DbType("nvarchar(30)")]
        //[MultiLanguage]
        public string FirstName
        {
            get { return _firstName; }
            set { base.SetPropertyValue("FirstName", value); }
        }

        private string _lastName;
        [DbType("nvarchar(30)")]
        //[MultiLanguage]
        public string LastName
        {
            get { return _lastName; }
            set { base.SetPropertyValue("LastName", value); }
        }

        private string _birthDate;
        [IsNullable]
        [DbType("char(10)")]
        public string BirthDate
        {
            get { return _birthDate; }
            set { base.SetPropertyValue("BirthDate", value); }
        }

        private string _fatherName;
        [IsNullable]
        [DbType("nvarchar(50)")]
        //[MultiLanguage]
        public string FatherName
        {
            get { return _fatherName; }
            set { base.SetPropertyValue("FatherName", value); }
        }

        private string _iDNumber;
        [Layout(Caption = "شماره شناسنامه")]
        [IsNullable]
        [DbType("nchar(10)")]
        public string IDNumber
        {
            get { return _iDNumber; }
            set { base.SetPropertyValue("IDNumber", value); }
        }

        private string _nationalCode;
        [IsNullable]
        [DbType("char(11)")]
        public string NationalCode
        {
            get { return _nationalCode; }
            set { base.SetPropertyValue("NationalCode", value); }
        }

        private bool? _gender;
        [IsNullable]
        [DbType("bit")]
        public bool? Gender
        {
            get { return _gender; }
            set { base.SetPropertyValue("Gender", value); }
        }

        private string _foreignCode;
        [IsNullable]
        [DbType("nchar(20)")]
        public string ForeignCode
        {
            get { return _foreignCode; }
            set { base.SetPropertyValue("ForeignCode", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.FirstName + this.LastName; }
        }
    }
}
