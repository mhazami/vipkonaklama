using System;
using System.Collections.Generic;
using Radyn.Comments.DataStructure;
using Radyn.Framework;

namespace Radyn.Article.Facade.Interface
{
    public interface IArticleCommentFacade : IBaseFacade<DataStructure.ArticleComment>
    {
        List<Comment> GetWithReply(Guid articleId);
    }
}
