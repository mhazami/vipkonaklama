using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.DA
{
    public sealed class OfficeRoomDA : DALBase<OfficeRoom>
    {
        public OfficeRoomDA(IConnectionHandler connectionHandler) : base(connectionHandler) { }
        internal class OfficeRoomCommandBuilder { }
    }
}
