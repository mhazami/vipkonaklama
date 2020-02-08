using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;
using System;

namespace Radyn.Reservation.BO
{
    internal class HotelOfficeBO : BusinessBase<HotelOffice>
    {
        public override bool Insert(IConnectionHandler connectionHandler, HotelOffice obj)
        {
            Guid id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
    }
}
