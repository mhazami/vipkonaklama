using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.DA
{
    public sealed class HotelDA : DALBase<Hotel>
    {
        public HotelDA(IConnectionHandler connectionHandler) : base(connectionHandler) { }
        internal class HotelCommandBuilder { }
    }
}
