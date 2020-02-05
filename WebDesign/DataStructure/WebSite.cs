using System;
using Radyn.Framework;
using Radyn.WebDesign.Definition;

namespace Radyn.WebDesign.DataStructure
{
    [Serializable]
    [Schema("WebDesign")]
    public sealed class WebSite : DataStructureBase<WebSite>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }
        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

        private string _installPath;
        [IsNullable]
        [DbType("nvarchar(250)")]
        public string InstallPath
        {
            get { return _installPath; }
            set { base.SetPropertyValue("InstallPath", value); }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        [MultiLanguage]
        public  string Title { get; set; }

        public Configuration _configuration;
        [Assosiation(PropName = "Id", JoinType = JoinType.Left)]
        public Configuration Configuration
        {
            get { return _configuration ?? (_configuration = new Configuration()); }
            set { _configuration = value; }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public Enums.WebSiteStatus Status { get; set; }

        private Guid _configurationId;
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public Guid ConfigurationId
        {
            get { return _configurationId == Guid.Empty?Id: _configurationId; }
            set { _configurationId = Id; }
        }
    }
}
