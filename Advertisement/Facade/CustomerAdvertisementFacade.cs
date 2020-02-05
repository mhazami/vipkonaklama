using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.Facade
{
    internal sealed class CustomerAdvertisementFacade : AdvertisementsBaseFacade<CustomerAdvertisement>, ICustomerAdvertisementFacade
    {
        internal CustomerAdvertisementFacade() { }

        internal CustomerAdvertisementFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

     
    }
}
