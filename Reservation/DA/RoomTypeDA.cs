using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.DA
{
    public sealed class RoomTypeDA : DALBase<RoomType>
    {
        public RoomTypeDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

    }
    internal class RoomTypeCommandBuilder
    {
    }
}