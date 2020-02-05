using Radyn.Article.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.DA
{
    public sealed class ArticleResourcesDA : DALBase<ArticleResources>
    {
        public ArticleResourcesDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class ArticleResourcesCommandBuilder
    {
    }
}
