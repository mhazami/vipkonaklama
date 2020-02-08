using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.DA
{
    public sealed class HotelFloorDA : DALBase<HotelFloor>
    {
        public HotelFloorDA(IConnectionHandler connectionHandler) : base(connectionHandler) { }
        internal class HotelFloorCommandBuilder { }
    }
}
