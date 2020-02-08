using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Facade.Interface;

namespace Radyn.Reservation.Facade
{
    internal sealed class HotelOfficeFacade : ReservationBaseFacade<HotelOffice>, IHotelOfficeFacade
    {
        internal HotelOfficeFacade() { }

        internal HotelOfficeFacade(IConnectionHandler connectionHandler)
        : base(connectionHandler) { }

    }
}