using Radyn.Framework.DbHelper;
using Radyn.Reservation;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Facade.Interface;

namespace Radyn.News.Facade
{
    internal sealed class ReservePriceFacade : ReservationBaseFacade<ReservePrice>, IReservePriceFacade
    {
        internal ReservePriceFacade() { }

        internal ReservePriceFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }


    }
}