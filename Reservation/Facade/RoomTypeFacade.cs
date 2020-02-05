using Radyn.Framework.DbHelper;
using Radyn.Reservation;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class RoomTypeFacade : ReservationBaseFacade<RoomType>, IRoomTypeFacade
    {
        internal RoomTypeFacade() { }

        internal RoomTypeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }


    }
}