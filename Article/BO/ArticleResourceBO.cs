using System;
using System.Collections.Generic;
using Radyn.Article.DA;
using Radyn.Article.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.BO
{
internal class ArticleResourceBO : BusinessBase<ArticleResource>
{
    public IEnumerable<ArticleResource> GetAllByArticleId(IConnectionHandler connectionHandler, Guid aId)
    {
        var articleResourceDa = new ArticleResourceDA(connectionHandler);
        return articleResourceDa.GetAllByArticleId(aId);
    }
}
}
