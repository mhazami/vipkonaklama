using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;
using Radyn.News.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class NewsAttributeFacade : NewsBaseFacade<NewsAttribute>, INewsAttributeFacade
    {
        internal NewsAttributeFacade() { }

        internal NewsAttributeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
    }
}
