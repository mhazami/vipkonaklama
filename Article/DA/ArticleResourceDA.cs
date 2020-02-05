using System;
using System.Collections.Generic;
using Radyn.Article.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.DA
{
    public sealed class ArticleResourceDA : DALBase<ArticleResource>
    {
        public ArticleResourceDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public IEnumerable<ArticleResource> GetAllByArticleId(Guid aId)
        {
            var articleCategoryCommandBuilder = new ArticleResourceCommandBuilder();
            var query = articleCategoryCommandBuilder.GetAllByArticleId(aId);
            return DBManager.GetCollection<ArticleResource>(base.ConnectionHandler, query);
        }
    }
    internal class ArticleResourceCommandBuilder
    {
        public string GetAllByArticleId(Guid aId)
        {
            return string.Format("SELECT     Article.ArticleResource.* " +
                                 " FROM         Article.ArticleResource INNER JOIN " +
                                 " Article.ArticleResources ON Article.ArticleResource.Id = Article.ArticleResources.ArticleResourceId " +
                                 " where Article.ArticleResources.ArticleId='{0}' ", aId);

        }
    }
}
