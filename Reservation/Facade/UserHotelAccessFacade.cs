using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Facade.Interface;

namespace Radyn.Reservation.Facade
{
    internal sealed class UserHotelAccessFacade : ReservationBaseFacade<UserHotelAccess>, IUserHotelAccessFacade
    {
        internal UserHotelAccessFacade() { }

        internal UserHotelAccessFacade(IConnectionHandler connectionHandler)
        : base(connectionHandler) { }

    }
}
