using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using System.Collections.Generic;

namespace Radyn.Security.BO
{
    internal class TrackerBO : BusinessBase<Tracker>
    {
        internal List<Tracker> Search(IConnectionHandler connectionHandler, int pageindex, int pagesize, Tracker tracker, string fromDate, string todate, string fromTo)
        {
            var query = new PredicateBuilder<Tracker>();
            if (!string.IsNullOrEmpty(tracker.FieldDesc))
                query.And(i => i.FieldDesc.Contains(tracker.FieldDesc));
            if (!string.IsNullOrEmpty(tracker.IpAddress))
                query.And(i => i.IpAddress.Contains(tracker.IpAddress));
            if (!string.IsNullOrEmpty(tracker.ObjectName))
                query.And(i => i.ObjectName.Contains(tracker.ObjectName));
            if (!string.IsNullOrEmpty(tracker.UserName))
                query.And(i => i.UserName.Contains(tracker.UserName));
            if (!string.IsNullOrEmpty(tracker.Operation))
            {
                switch (tracker.Operation)
                {
                    case "ایجاد مستند":
                        query.And(i => i.Operation.Contains(tracker.Operation) || i.Operation.Contains("ایجاد"));
                        break;
                    case "حذف مستند":
                        query.And(i => i.Operation.Contains(tracker.Operation) || i.Operation.Contains("حذف"));
                        break;
                    case "ویرایش مستند":
                        query.And(i => i.Operation.Contains(tracker.Operation) || i.Operation.Contains("ویرایش"));
                        break;
                    default:
                        query.And(i => i.Operation.Contains(tracker.Operation));
                        break;
                }
            }
            if (!string.IsNullOrEmpty(fromDate))
                query.And(i => i.Date.CompareTo(fromDate) >= 0);
            if (!string.IsNullOrEmpty(todate))
                query.And(i => i.Date.CompareTo(todate) <= 0);
            if (!string.IsNullOrEmpty(tracker.MasterRefId) && !string.IsNullOrEmpty(tracker.RefId))
                query.And(i => i.MasterObjectRefId == tracker.MasterRefId || i.RefId == tracker.RefId);
            if (!string.IsNullOrEmpty(tracker.RootId))
                query.And(i => i.RootId == tracker.RootId);
            if (!string.IsNullOrEmpty(fromTo))
                query.And(i => i.OldVal.Contains(fromTo) || i.NewValue.Contains(fromTo));
            return this.OrderBy(connectionHandler, i => i.Id, query.GetExpression());
        }
    }
}
