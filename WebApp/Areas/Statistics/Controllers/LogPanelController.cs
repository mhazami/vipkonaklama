using System.Web.Mvc;
using Radyn.Statistics;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.Statistics.Controllers
{
    public class LogPanelController : WebDesignBaseController
    {
      
        public ActionResult GetStatitics()
        {
            try
            {
                var str = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                var authority = System.Web.HttpContext.Current.Request.Url.Authority;
                var result = StatisticComponents.Instance.LogFacade.GetStatisticsModel(authority + str);
                return PartialView("PartialViewStatitics", result);
            }
            catch (System.Exception ex)
            {

                return null;
            }
        }
    }
}
