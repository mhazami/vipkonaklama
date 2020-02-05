using System;
using Radyn.Comments.DataStructure;
using Radyn.Framework;

namespace Radyn.Article.DataStructure
{
    [Serializable]
    [Schema("Article")]
   public sealed class ArticleComment:DataStructureBase<ArticleComment>
    {
        private Guid _commentId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid CommentId
        {
            get
            {
                return this._commentId;
            }
            set
            {
                base.SetPropertyValue("CommentId", value);
                if (Comment == null)
                    this.Comment = new Comment { Id = value };
            }
        }
        [Assosiation(PropName = "CommentId")]
        public Comment Comment { get; set; }

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
                if (Article == null)
                    this.Article = new Article { Id = value };
            }
        }
        [Assosiation(PropName = "ArticleId")]
        public Article Article{ get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField { get; }
    }
}
