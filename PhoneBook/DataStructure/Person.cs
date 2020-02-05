using System;
using Radyn.Framework;

namespace Radyn.PhoneBook.DataStructure
{
    [Serializable]
    [Schema("PhoneBook")]
    public partial class Person : DataStructureBase<Person>
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
               
            }
        }

        [Assosiation(PropName = "Id")]
        public EnterpriseNode.DataStructure.EnterpriseNode EnterpriseNode { get; set; }

        private string _personeliCode;
        [IsNullable]
        [DbType("nvarchar(50)")]
        public string PersoneliCode
        {
            get { return _personeliCode; }
            set { base.SetPropertyValue("PersoneliCode", value); }
        }

        private string _remark;
        [IsNullable]
        [DbType("nvarchar(max)")]
        public string Remark
        {
            get { return _remark; }
            set { base.SetPropertyValue("Remark", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

      

        private string _jopTitle;
        [IsNullable]
        [DbType("nvarchar(500)")]
        public string JopTitle
        {
            get { return _jopTitle; }
            set { base.SetPropertyValue("JopTitle", value); }
        }




        private Int32? _officeId;
        [IsNullable]
        [DbType("int")]
        public Int32? OfficeId
        {
            get
            {
                return this._officeId;
            }
            set
            {
                base.SetPropertyValue("OfficeId", value);
                if (PersonOffice == null)
                {
                    if (value.HasValue)
                        this.PersonOffice = new Office { Id = value.Value };
                    else
                        this.PersonOffice = null;
                }
             
            }
        }
        [Assosiation(PropName = "OfficeId")]
        public Office PersonOffice { get; set; }

        private Int32? _departmentId;
        [IsNullable]
        [DbType("int")]
        public Int32? DepartmentId
        {
            get
            {
                return this._departmentId;
            }
            set
            {
                base.SetPropertyValue("DepartmentId", value);
                if (PersonDepartment == null)
                {
                    if (value.HasValue)
                        this.PersonDepartment = new Department { Id = value.Value };
                    else
                        this.PersonDepartment = null;
                }
            
            }
        }
        [Assosiation(PropName = "DepartmentId")]
        public Department PersonDepartment { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string PersonPhone { get; set; }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string PersonAddress { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.PersoneliCode; }
        }
    }
}
