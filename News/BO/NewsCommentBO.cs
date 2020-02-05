using Radyn.Comments.BO;
using Radyn.Comments.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System.Collections.Generic;
using System.Linq;

namespace Radyn.News.BO
{
    public class NewsCommentBO : BusinessBase<News.DataStructure.NewsComment>
    {
        public List<Comment> GetWithReply(IConnectionHandler connectionHandler, int newsId)
        {
            List<Comment> result = new List<Comment>();
            var comments = Select(connectionHandler, x => x.Comment, x => x.NewsId == newsId && x.Comment.Approved).GroupBy(
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
