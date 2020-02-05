using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;
using Radyn.News.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class NewsPropertyFacade : NewsBaseFacade<NewsProperty>, INewsPropertyFacade
    {
        internal NewsPropertyFacade() { }

        internal NewsPropertyFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        
    }
}
