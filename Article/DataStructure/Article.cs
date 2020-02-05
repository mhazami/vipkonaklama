using System;
using System.Collections.Generic;
using Radyn.Framework;

namespace Radyn.Article.DataStructure
{
    [Serializable]
    [Schema("Article")]
    public sealed class Article : DataStructureBase<Article>
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
        [DbType("nvarchar(300)")]
        [MultiLanguage]
        public string Title
        {
            get { return _title; }
            set { base.SetPropertyValue("Title", value); }
        }

        private string _keyword;
        [DbType("nvarchar(500)")]
        [MultiLanguage]
        public string Keyword
        {
            get { return _keyword; }
            set { base.SetPropertyValue("Keyword", value); }
        }

        private string _abstract;
        [IsNullable]
        [DbType("nvarchar(1000)")]
        [MultiLanguage]
        public string Abstract
        {
            get { return _abstract; }
            set { base.SetPropertyValue("Abstract", value); }
        }

        private string _text;
        [IsNullable]
        [DbType("ntext")]
        [MultiLanguage]
        public string Text
        {
            get { return _text; }
            set { base.SetPropertyValue("Text", value); }
        }

        private Int32 _order;
        [DbType("int")]
        public Int32 Order
        {
            get { return _order; }
            set { base.SetPropertyValue("Order", value); }
        }

        private string _publishDate;
        [DbType("char(10)")]
        public string PublishDate
        {
            get { return _publishDate; }
            set { base.SetPropertyValue("PublishDate", value); }
        }



        private Int32 _visitCount;
        [DbType("int")]
        public Int32 VisitCount
        {
            get { return _visitCount; }
            set { base.SetPropertyValue("VisitCount", value); }
        }

        private Guid? _thumbnailId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ThumbnailId
        {
            get { return _thumbnailId; }
            set { base.SetPropertyValue("ThumbnailId", value); }
        }

        private bool _enable;
        [DbType("bit")]
        public bool Enable
        {
            get { return _enable; }
            set { base.SetPropertyValue("Enable", value); }
        }

        private int _articleCategoryId;
        [DbType("int")]
        public int ArticleCategoryId
        {
            get
            {
                return this._articleCategoryId;
            }
            set
            {
                base.SetPropertyValue("ArticleCategoryId", value);
                if (ArticleCategory == null) this.ArticleCategory = new ArticleCategory {Id = value};

            }
        }

        [Assosiation(PropName = "ArticleCategoryId")]
        public ArticleCategory ArticleCategory { get; set; }


        private bool _getComment;
        [DbType("bit")]
        public bool GetComment { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<ArticleFileAttachment> ArticleFileAttachments { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return string.Empty; }
        }
    }
}
