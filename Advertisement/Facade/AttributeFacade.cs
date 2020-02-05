using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.Facade
{
    internal sealed class AttributeFacade : AdvertisementsBaseFacade<Attribute>, IAttributeFacade
    {
        internal AttributeFacade() { }

        internal AttributeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        
    }
}
