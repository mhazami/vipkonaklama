using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Facade.Interface;

namespace Radyn.Reservation.Facade
{
    internal sealed class ReserveTypeFacade : ReservationBaseFacade<ReserveType>, IReserveTypeFacade
    {
        internal ReserveTypeFacade() { }

        internal ReserveTypeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }
    }
}
