using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.DA
{
    public sealed class RoomDA : DALBase<Room>
    {
        public RoomDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

    }
    internal class RoomCommandBuilder
    {
    }
}