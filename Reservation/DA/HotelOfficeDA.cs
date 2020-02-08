using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.DA
{
    public sealed class HotelOfficeDA : DALBase<HotelOffice>
    {
        public HotelOfficeDA(IConnectionHandler connectionHandler) : base(connectionHandler) { }
        internal class HotelOfficeCommandBuilder { }
    }
}
