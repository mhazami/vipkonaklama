using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.DataStructure;

namespace Radyn.Reservation.BO
{
    internal class CustomerBO : BusinessBase<Customer>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Customer obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
    }
}
