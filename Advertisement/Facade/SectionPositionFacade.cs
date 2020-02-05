using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.Facade
{
    internal sealed class SectionPositionFacade : AdvertisementsBaseFacade<SectionPosition>, ISectionPositionFacade
    {
        internal SectionPositionFacade() { }

        internal SectionPositionFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

      
    }
}
