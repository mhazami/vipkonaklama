using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.Facade
{
    internal sealed class TariffClassHistoryFacade : AdvertisementsBaseFacade<TariffClassHistory>, ITariffClassHistoryFacade
    {
        internal TariffClassHistoryFacade() { }

        internal TariffClassHistoryFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
