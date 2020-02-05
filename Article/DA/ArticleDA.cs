using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System.Collections.Generic;

namespace Radyn.Contracts.DAL.UrbanBase
{
    public sealed class ArticleDA : DALBase<Article.DataStructure.Article>
    {
        public ArticleDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }

        public IEnumerable<Article.DataStructure.Article> GetByCategoryId(short categoryId, int? topCount)
        {
            ArticleCommandBuilder articleCommandBuilder = new ArticleCommandBuilder();
            string query = articleCommandBuilder.GetByCategoryId(categoryId, topCount);
            return DBManager.GetCollection<Article.DataStructure.Article>(base.ConnectionHandler, query);
        }
    }
    internal class ArticleCommandBuilder
    {
        public string GetByCategoryId(short categoryId, int? topCount)
        {
            return string.Format("SELECT   distinct  {0}  Article.Article.*" +
                                 " FROM        Article.Article INNER JOIN " +
                                 " Article.ArticleCategories ON Article.Article.Id = Article.ArticleCategories.ArticleId " +
                                 " where Article.ArticleCategories.ArticleCategoriId={1}   order by PublishDate desc", topCount != null && topCount > 0 ? "top(" + topCount + ")" : "", categoryId);
        }
    }
}
