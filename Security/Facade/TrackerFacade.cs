using Radyn.Framework;
using Radyn.Security.BO;
using Radyn.Security.Facade.Interface;
using System;
using System.Collections.Generic;

namespace Radyn.Security.Facade
{
    internal sealed class TrackerFacade : SecurityBaseFacade<Tracker>, ITrackerFacade
    {
        public List<Tracker> Search(int pageindex, int pagesize, Tracker tracker, string fromDate, string todate, string fromTo)
        {
            try
            {
                return new TrackerBO().Search(this.ConnectionHandler, pageindex, pagesize, tracker, fromDate, todate, fromTo);
            }
            catch (KnownException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
