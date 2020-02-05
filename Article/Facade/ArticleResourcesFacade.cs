using Radyn.Article.DataStructure;
using Radyn.Article.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Article.Facade
{
    internal sealed class ArticleResourcesFacade : ArticleBaseFacade<ArticleResources>, IArticleResourcesFacade
    {
        internal ArticleResourcesFacade() { }

        internal ArticleResourcesFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
    }
}
