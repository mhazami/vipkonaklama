using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;
using System;

namespace Radyn.Reservation.BO
{
    internal class UserHotelAccessBO : BusinessBase<UserHotelAccess>
    {
        public override bool Insert(IConnectionHandler connectionHandler, UserHotelAccess obj)
        {
            
            return base.Insert(connectionHandler, obj);
        }
    }
}
