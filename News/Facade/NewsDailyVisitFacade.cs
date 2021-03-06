using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;
using Radyn.News.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class NewsDailyVisitFacade : NewsBaseFacade<NewsDailyVisit>, INewsDailyVisitFacade
    {
        internal NewsDailyVisitFacade() { }

        internal NewsDailyVisitFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
    }
}
