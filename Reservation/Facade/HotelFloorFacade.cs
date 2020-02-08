using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Facade.Interface;

namespace Radyn.Reservation.Facade
{
    internal sealed class HotelFloorFacade : ReservationBaseFacade<HotelFloor>, IHotelFloorFacade
    {
        internal HotelFloorFacade() { }

        internal HotelFloorFacade(IConnectionHandler connectionHandler)
        : base(connectionHandler) { }

    }
}