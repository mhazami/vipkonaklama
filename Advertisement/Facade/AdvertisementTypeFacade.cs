using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.Facade
{
    internal sealed class AdvertisementTypeFacade : AdvertisementsBaseFacade<AdvertisementType>, IAdvertisementTypeFacade
    {
        internal AdvertisementTypeFacade() { }

        internal AdvertisementTypeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
