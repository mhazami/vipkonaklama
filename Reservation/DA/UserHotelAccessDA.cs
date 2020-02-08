using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.DA
{
    public sealed class UserHotelAccessDA : DALBase<UserHotelAccess>
    {
        public UserHotelAccessDA(IConnectionHandler connectionHandler) : base(connectionHandler) { }
        internal class UserHotelAccessCommandBuilder { }
    }
}
