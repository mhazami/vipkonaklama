using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.DA
{
    public sealed class CustomerDA : DALBase<Customer>
    {
        public CustomerDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

    }
    internal class CustomerCommandBuilder
    {
    }
}