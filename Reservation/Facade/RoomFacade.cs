using Radyn.Framework.DbHelper;
using Radyn.Reservation;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class RoomFacade : ReservationBaseFacade<Room>, IRoomFacade
    {
        internal RoomFacade() { }

        internal RoomFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }


    }
}