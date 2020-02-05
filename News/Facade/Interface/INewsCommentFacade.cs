using System.Collections.Generic;
using Radyn.Comments.DataStructure;
using Radyn.Framework;
using Radyn.News.DataStructure;

namespace Radyn.News.Facade.Interface
{
    public interface INewsCommentFacade : IBaseFacade<NewsComment>
    {
        List<Comment> GetWithReply(int newsId);
    }
}
