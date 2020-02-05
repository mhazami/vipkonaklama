using Radyn.Framework;
using System.Collections.Generic;

namespace Radyn.Security.Facade.Interface
{
    public interface ITrackerFacade : IBaseFacade<Tracker>
    {
        List<Tracker> Search(int pageindex, int pagesize, Tracker tracker, string fromDate, string todate, string fromTo);
    }
}
