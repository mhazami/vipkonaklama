using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;
using Radyn.News.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class NewsPublishTypeFacade : NewsBaseFacade<NewsPublishType>, INewsPublishTypeFacade
    {
        internal NewsPublishTypeFacade() { }

        internal NewsPublishTypeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
    }
}
