using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.DA
{
    public sealed class ReservePriceDA : DALBase<ReservePrice>
    {
        public ReservePriceDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

    }
    internal class ReservePriceCommandBuilder
    {
    }
}