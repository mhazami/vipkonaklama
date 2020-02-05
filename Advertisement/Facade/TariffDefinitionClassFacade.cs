using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade.Interface;
using Radyn.Framework.DbHelper;


namespace Radyn.Advertisements.Facade
{
    internal sealed class TariffDefinitionClassFacade : AdvertisementsBaseFacade<TariffDefinitionClass>, ITariffDefinitionClassFacade
    {
        internal TariffDefinitionClassFacade() { }

        internal TariffDefinitionClassFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
