using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;
using System;

namespace Radyn.Reservation.BO
{
    internal class HotelFloorBO : BusinessBase<HotelFloor>
    {
        public override bool Insert(IConnectionHandler connectionHandler, HotelFloor obj)
        {
            Guid id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
    }
}
