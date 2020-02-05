using System;
using System.Collections.Generic;
using Radyn.Article.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.DA
{
    public sealed class ArticleCategoryDA : DALBase<ArticleCategory>
    {
        public ArticleCategoryDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public IEnumerable<ArticleCategory> GetAllByArticleId(Guid aId)
        {
            var articleCategoryCommandBuilder = new ArticleCategoryCommandBuilder();
            var query = articleCategoryCommandBuilder.GetAllByArticleId(aId);
            return DBManager.GetCollection<ArticleCategory>(base.ConnectionHandler, query);
        }
    }
    internal class ArticleCategoryCommandBuilder
    {
        public string GetAllByArticleId(Guid aId)
        {
            return string.Format("SELECT     Article.ArticleCategory.*" +
                                 " FROM         Article.ArticleCategory INNER JOIN " +
                                 " Article.ArticleCategories ON Article.ArticleCategory.Id = Article.ArticleCategories.ArticleCategoriId " +
                                 " where Article.ArticleCategories.ArticleId='{0}'", aId);
        }
    }
}
