using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.DA
{
    public sealed class ReserveTypeDA : DALBase<ReserveType>
    {
        public ReserveTypeDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        {
        }
    }
    internal class ReserveTypeCommandBuilder
    {
    }
}
