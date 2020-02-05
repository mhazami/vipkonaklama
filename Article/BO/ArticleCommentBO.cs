using Radyn.Article.DataStructure;
using Radyn.Comments.BO;
using Radyn.Comments.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Radyn.Article.BO
{
    internal class ArticleCommentBO : BusinessBase<ArticleComment>
    {
        public override bool Delete(IConnectionHandler connectionHandler, ArticleComment obj)
        {
            new CommentBO().Delete(connectionHandler, obj);

            return base.Delete(connectionHandler, obj);
        }

        public List<Comment> GetWithReply(IConnectionHandler connectionHandler, Guid articleId)
        {
            List<Comment> result = new List<Comment>();
            var comments = Select(connectionHandler, x => x.Comment, x => x.ArticleId == articleId && x.Comment.Approved).GroupBy(
                x => x.ParentCommentId, x => x.ParentComment,
                (key, g) => new { ParentId = key, Comment = g.ToList() });

            foreach (var comment in comments)
            {
                Comment obj = new CommentBO().Get(connectionHandler, comment.ParentId);
                obj.Children = comment.Comment;
                result.Add(obj);
            }

            return result;
        }
    }
}
