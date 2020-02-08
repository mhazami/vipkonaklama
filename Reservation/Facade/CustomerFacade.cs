using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Facade.Interface;

namespace Radyn.Reservation.Facade
{
    internal sealed class CustomerFacade : ReservationBaseFacade<Customer>, ICustomerFacade
    {
        internal CustomerFacade() { }

        internal CustomerFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }


    }
}