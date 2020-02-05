using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;
using Radyn.News.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class NewsContentTypeFacade : NewsBaseFacade<NewsContentType>, INewsContentTypeFacade
    {
        internal NewsContentTypeFacade()
        {
        }

        internal NewsContentTypeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       

      

       
    }
}
