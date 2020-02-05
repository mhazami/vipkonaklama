using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.DA
{
    public sealed class OrderDA : DALBase<Order>
    {
        public OrderDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

    }
    internal class OrderCommandBuilder
    {
    }
}