using Radyn.Framework.DbHelper;
using Radyn.Statistics.Facade;
using Radyn.Statistics.Facade.Interface;

namespace Radyn.Statistics
{
   public class StatisticComponents
    {
       internal StatisticComponents()
       {
           
       }
       private static StatisticComponents _instance;
       public static StatisticComponents Instance
       {
           get { return _instance ?? (_instance = new StatisticComponents()); }
       }
       public IWebSiteFacade WebSiteFacade
       {
           get { return new WebSiteFacade(); }
       }
       public IWebSiteFacade WebSiteTransactionalFacade(IConnectionHandler connectionHandler)
       {
           return new WebSiteFacade(connectionHandler); 
       }
       public ILogFacade LogFacade
       {
           get { return new LogFacade(); }
       }
   }
}
