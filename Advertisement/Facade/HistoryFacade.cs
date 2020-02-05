using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.Facade
{
    internal sealed class HistoryFacade : AdvertisementsBaseFacade<History>, IHistoryFacade
    {
        internal HistoryFacade() { }

        internal HistoryFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        
    }
}
