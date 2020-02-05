using System;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class Container : DataStructureBase<Container>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _title;
        [DbType("nvarchar(100)")]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _html;
        [DbType("ntext")]
        [MultiLanguage]
        public string Html
        {
            get { return _html; }
            set { base.SetPropertyValue("Html", value); }
        }
        private bool _isExternal;
        [DbType("bit")]
        public bool IsExternal
        {
            get { return _isExternal; }
            set { base.SetPropertyValue("IsExternal", value); }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
