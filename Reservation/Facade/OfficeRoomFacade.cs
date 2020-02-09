using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Facade.Interface;

namespace Radyn.Reservation.Facade
{
    internal sealed class OfficeRoomFacade : ReservationBaseFacade<OfficeRoom>, IOfficeRoomFacade
    {
        internal OfficeRoomFacade() { }

        internal OfficeRoomFacade(IConnectionHandler connectionHandler)
        : base(connectionHandler) { }

    }
}
