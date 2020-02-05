using System;
using Radyn.Framework;

namespace Radyn.Article.DataStructure
{
    [Serializable]
    [Schema("Article")]
    public sealed class ArticleResource : DataStructureBase<ArticleResource>
    {
        private Int16 _id;
        [Key(true)]
        [DbType("smallint")]
        public Int16 Id
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

        private string _webAddress;
        [IsNullable]
        [DbType("char(50)")]
        public string WebAddress
        {
            get { return _webAddress; }
            set { base.SetPropertyValue("WebAddress", value); }
        }

        private string _publishDateResource;
        [IsNullable]
        [DbType("char(10)")]
        public string PublishDateResource
        {
            get { return _publishDateResource; }
            set { base.SetPropertyValue("PublishDateResource", value); }
        }

        private string _publisher;
        [IsNullable]
        [DbType("nvarchar(200)")]
        public string Publisher
        {
            get { return _publisher; }
            set { base.SetPropertyValue("Publisher", value); }
        }

        private string _country;
        [IsNullable]
        [DbType("nvarchar(70)")]
        public string Country
        {
            get { return _country; }
            set { base.SetPropertyValue("Country", value); }
        }

        private string _language;
        [IsNullable]
        [DbType("nvarchar(70)")]
        public string Language
        {
            get { return _language; }
            set { base.SetPropertyValue("Language", value); }
        }

        private string _auther;
        [IsNullable]
        [DbType("nvarchar(200)")]
        public string Auther
        {
            get { return _auther; }
            set { base.SetPropertyValue("Auther", value); }
        }
        
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title; }
        }
    }
}
