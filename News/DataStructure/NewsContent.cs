using System;
using Radyn.Common.DataStructure;
using Radyn.Framework;

namespace Radyn.News.DataStructure
{
    [Serializable]
    [Description("محتوای خبر")]
    [Schema("News")]
    public sealed class NewsContent : DataStructureBase<NewsContent>
    {
        private Int32 _id;
        [Key(false)]
        [DbType("int")]
        public Int32 Id
        {
            get
            {
                return this._id;
            }
            set
            {
                base.SetPropertyValue("Id", value);
                if (News == null)
                    this.News = new Radyn.News.DataStructure.News { Id = value };
            }
        }

        [Assosiation(PropName = "Id")]
        public Radyn.News.DataStructure.News News { get; set; }

        private string _languageId;
        [Key(false)]
        [DbType("char(5)")]
        public string LanguageId
        {
            get
            {
                return this._languageId;
            }
            set
            {
                base.SetPropertyValue("LanguageId", value);
                if (Language == null)
                    this.Language = new Language { Id = value };
            }
        }

        [Assosiation(PropName = "LanguageId")]
        public Language Language { get; set; }

        private string _overTitle;
        [IsNullable]
        [DbType("nvarchar(250)")]
        public string OverTitle
        {
            get { return _overTitle; }
            set { base.SetPropertyValue("OverTitle", value); }
        }

        private string _title1;
        [DbType("nvarchar(4000)")]
        public string Title1
        {
            get { return _title1; }
            set { base.SetPropertyValue("Title1", value); }
        }

        private string _title2;
        [IsNullable]
        [DbType("nvarchar(4000)")]
        public string Title2
        {
            get { return _title2; }
            set { base.SetPropertyValue("Title2", value); }
        }

        private string _lead;
        [IsNullable]
        [DbType("nvarchar(max)")]
        public string Lead
        {
            get { return _lead; }
            set { base.SetPropertyValue("Lead", value); }
        }

        private string _body;
        [IsNullable]
        [DbType("ntext")]
        public string Body
        {
            get { return _body; }
            set { base.SetPropertyValue("Body", value); }
        }

        private string _sutitr;
        [IsNullable]
        [DbType("nvarchar(max)")]
        public string Sutitr
        {
            get { return _sutitr; }
            set { base.SetPropertyValue("Sutitr", value); }
        }

        private Int32 _visitCount;
        [Layout(Caption = "تعداد بازدید ")]
        [DbType("int")]
        public Int32 VisitCount
        {
            get { return _visitCount; }
            set { base.SetPropertyValue("VisitCount", value); }
        }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Title1; }
        }
    }
}
