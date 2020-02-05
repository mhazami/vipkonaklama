using System;
using Radyn.Framework;


namespace Radyn.PhoneBook.DataStructure
{
    [Serializable]
    [Schema("PhoneBook")]
    public sealed class Configuration : DataStructureBase<Configuration>
    {
        
        private Guid _webSiteId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid WebSiteId
        {
            get
            {
                return this._webSiteId;
            }
            set
            {
                base.SetPropertyValue("WebSiteId", value);
                
            }
        }

      


        private string _officeName;
        [MultiLanguage]
        public string OfficeName
        {
            get { return _officeName; }
            set { _officeName = value; }
        }


        private string _departmentName;
        [MultiLanguage]
        public string DepartmentName
        {
            get { return _departmentName; }
            set { _departmentName = value; }
        }


        private string _description;
        [DbType("nvarchar(1000)")]
        [IsNullable]
        public string Description
        {
            get { return _description; }
            set { base.SetPropertyValue("Description", value); }
        }

     


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Description; }
        }
    }
}
