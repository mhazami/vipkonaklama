using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.BO
{
    internal class ReservePriceBO : BusinessBase<ReservePrice>
    {
        public override bool Insert(IConnectionHandler connectionHandler, ReservePrice obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
    }
}
