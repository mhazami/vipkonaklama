using System;
using Radyn.Framework;

namespace Radyn.ContentManager.DataStructure
{
    [Serializable]
    [Schema("ContentManage")]
    public sealed class DefaultHtmlDesgin : DataStructureBase<DefaultHtmlDesgin>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _body;
        [DbType("ntext")]
        [MultiLanguage]
        public string Body
        {
            get { return _body; }
            set { base.SetPropertyValue("Body", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }
      
        private string _title;
        [DbType("nvarchar(250)")]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _image;
        [DbType("varchar(50)")]
        [MultiLanguage]
        public string Image
        {
            get { return _image; }
            set { base.SetPropertyValue("Image", value); }
        }



        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Body; }
        }
    }
}
