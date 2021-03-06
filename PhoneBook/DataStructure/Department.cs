using System;
using Radyn.Framework;

namespace Radyn.PhoneBook.DataStructure
{
    [Serializable]
    [Schema("PhoneBook")]
    public sealed class Department : DataStructureBase<Department>
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
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }
        private string _description;
        [IsNullable]
        [DbType("nvarchar(max)")]
        public string Description
        {
            get { return _description; }
            set { base.SetPropertyValue("Description", value); }
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
                if (Office == null)
                {
                    if (value.HasValue)
                        this.Office = new Office { Id = value.Value };
                    else
                        this.Office = null;
                }

            }
        }
        [Assosiation(PropName = "OfficeId")]
        public Office Office { get; set; }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }

}