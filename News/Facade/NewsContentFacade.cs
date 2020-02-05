using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;
using Radyn.News.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class NewsContentFacade : NewsBaseFacade<NewsContent>, INewsContentFacade
    {
        internal NewsContentFacade() { }

        internal NewsContentFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
    }
}
