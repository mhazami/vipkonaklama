using System;
using Radyn.Comments.DataStructure;
using Radyn.Framework;

namespace Radyn.News.DataStructure
{
    [Serializable]
    [Schema("News")]
   public sealed class NewsComment : DataStructureBase<NewsComment>
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


        private int _newsId;
        [Key(false)]
        [DbType("int")]
        public int NewsId
        {
            get
            {
                return this._newsId;
            }
            set
            {
                base.SetPropertyValue("NewsId", value);
                if (News == null)
                    this.News = new News { Id = value };
            }
        }
        [Assosiation(PropName = "NewsId")]
        public News News { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField { get; }
    }
}
