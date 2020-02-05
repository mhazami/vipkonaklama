using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;
using Radyn.News.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class RelatedNewsGroupFacade : NewsBaseFacade<RelatedNewsGroup>, IRelatedNewsGroupFacade
    {
        internal RelatedNewsGroupFacade() { }

        internal RelatedNewsGroupFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        
    }
}
