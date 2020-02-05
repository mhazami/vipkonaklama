using System;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class MenuHtml : DataStructureBase<MenuHtml>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _masterBody;
        [DbType("ntext")]
        [MultiLanguage]
        public string MasterBody
        {
            get { return _masterBody; }
            set { base.SetPropertyValue("MasterBody", value); }
        }


      
        private string _hasChildBody;
        [DbType("ntext")]
        [MultiLanguage]
        public string HasChildBody
        {
            get { return _hasChildBody; }
            set { base.SetPropertyValue("HasChildBody", value); }
        }
        private string _rootMenuBody;
        [DbType("ntext")]
        [MultiLanguage]
        public string RootMenuBody
        {
            get { return _rootMenuBody; }
            set { base.SetPropertyValue("RootMenuBody", value); }
        }

        private string _childMenuBody;
        [DbType("ntext")]
        [MultiLanguage]
        public string ChildMenuBody
        {
            get { return _childMenuBody; }
            set { base.SetPropertyValue("ChildMenuBody", value); }
        }


        

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }
        private bool _isExternal;
        [DbType("bit")]
        public bool IsExternal
        {
            get { return _isExternal; }
            set { base.SetPropertyValue("IsExternal", value); }
        }

        private string _title;
        [DbType("nvarchar(250)")]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }




        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.MasterBody; }
        }
    }
}
