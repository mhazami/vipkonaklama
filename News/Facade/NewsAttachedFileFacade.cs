using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;
using Radyn.News.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class NewsAttachedFileFacade : NewsBaseFacade<NewsAttachedFile>, INewsAttachedFileFacade
    {
        internal NewsAttachedFileFacade() { }

        internal NewsAttachedFileFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        
    }
}
