using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.Facade
{
    internal sealed class SectionFacade : AdvertisementsBaseFacade<Section>, ISectionFacade
    {
        internal SectionFacade() { }

        internal SectionFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
