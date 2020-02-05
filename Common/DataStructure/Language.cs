using System;
using Radyn.Framework;

namespace Radyn.Common.DataStructure
{
    [Serializable]
    [Schema("Common")]
    public sealed class Language : DataStructureBase<Language>
    {
        private string _id;
        [Key(false)]
        [DbType("char(5)")]
        public string Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _displayName;
        [DbType("nvarchar(40)")]
        public string DisplayName
        {
            get { return _displayName; }
            set { base.SetPropertyValue("DisplayName", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }
        private bool _showLogo;
        [DbType("bit")]
        public bool ShowLogo
        {
            get { return _showLogo; }
            set { base.SetPropertyValue("ShowLogo", value); }
        }

        private Guid _logoId;
        [DbType("uniqueidentifier")]
        public Guid LogoId
        {
            get { return _logoId; }
            set { base.SetPropertyValue("LogoId", value); }
        }

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
            get { return this.Id; }
        }
    }
}
