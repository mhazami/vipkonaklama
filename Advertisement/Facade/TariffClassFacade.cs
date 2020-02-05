using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.Facade
{
    internal sealed class TariffClassFacade : AdvertisementsBaseFacade<TariffClass>, ITariffClassFacade
    {
        internal TariffClassFacade() { }

        internal TariffClassFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        
    }
}
