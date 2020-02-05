using System;
using Radyn.Framework;

namespace Radyn.Article.DataStructure
{
    [Serializable]
    [Schema("Article")]
    public sealed class ArticleResources : DataStructureBase<ArticleResources>
    {
        private Guid _articleId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid ArticleId
        {
            get
            {
                return this._articleId;
            }
            set
            {
                base.SetPropertyValue("ArticleId", value);
                this.Article = new Article { Id = value };
            }
        }

        [Assosiation(PropName = "ArticleId", FillData = false)]
        public Article Article { get; set; }

        private Int16 _articleResourceId;
        [Key(false)]
        [DbType("smallint")]
        public Int16 ArticleResourceId
        {
            get
            {
                return this._articleResourceId;
            }
            set
            {
                base.SetPropertyValue("ArticleResourceId", value);
                this.ArticleResource = new ArticleResource { Id = value };
            }
        }

        [Assosiation(PropName = "ArticleResourceId")]
        public ArticleResource ArticleResource { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.ArticleResource.Title; }
        }
    }
}
