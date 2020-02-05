using System;
using System.Web;
using Radyn.Statistics;

namespace Radyn.WebApp.Areas.Statistics.Components
{
    public static class LogCompoenents
    {
        public static bool AddLog(HttpRequest httpContext,string sesstionId)
        {
            try
            {
                var authority = httpContext.Url.Authority;
                var web = StatisticComponents.Instance.WebSiteFacade.SelectFirstOrDefault(x=>x.Id,x => x.Url.ToLower() == authority.ToLower());
                if (web == Guid.Empty) return true;
                var log = new Radyn.Statistics.DataStructure.Log
                {
                    WebSiteId = web,
                    Path = authority+httpContext.Path,
                    Url = authority+httpContext.Url.AbsolutePath,
                    IP = httpContext.UserHostAddress,
                    IsLocal = httpContext.IsLocal
                };
                if (httpContext.UrlReferrer != null) log.HttpReferer = httpContext.UrlReferrer.Authority + httpContext.UrlReferrer.AbsolutePath;
                log.PhysicalPath = httpContext.PhysicalPath;
                log.SesstionId = sesstionId;
                if (log.HttpReferer != null)
                    if (log.HttpReferer.Equals(log.Url)) return true;
                return StatisticComponents.Instance.LogFacade.InserNewLog(log);
            }
            catch (Exception)
            {
                
               
            }
            return false;
        }
    }
}