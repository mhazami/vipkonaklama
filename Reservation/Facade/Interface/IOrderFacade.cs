using Radyn.Framework;
using Radyn.Reservation.DataStructure;
using System;

namespace Radyn.Reservation.Facade.Interface
{
    public interface IOrderFacade : IBaseFacade<Order>
    {
        bool UpdateWithCustomer(Order order);

        bool InsertWithCustomer(Order order);
        decimal GetTotalPrice(DateTime startdate, DateTime enddate, byte roomtypeId, Guid reserveType);
    }
}
