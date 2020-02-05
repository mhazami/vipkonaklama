using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.Facade
{
    internal sealed class PositionFacade : AdvertisementsBaseFacade<Position>, IPositionFacade
    {
        internal PositionFacade() { }

        internal PositionFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
