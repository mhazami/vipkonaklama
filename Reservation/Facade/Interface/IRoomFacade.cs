using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.Facade.Interface
{
    public interface IRoomFacade : IBaseFacade<Room>
    {
        List<dynamic> GetTree(Guid hotelId, Guid officeId);
    }
}
