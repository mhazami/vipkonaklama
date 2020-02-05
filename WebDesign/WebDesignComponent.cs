using Radyn.Framework.DbHelper;
using Radyn.WebDesign.Facade;
using Radyn.WebDesign.Facade.Interface;


namespace Radyn.WebDesign
{
    public class WebDesignComponent
    {

        private WebDesignComponent()
        {

        }
        private static WebDesignComponent _instance;
        public static WebDesignComponent Instance
        {
            get { return _instance ?? (_instance = new WebDesignComponent()); }
        }
        public IConfigurationFacade ConfigurationFacade
        {
            get { return new ConfigurationFacade(); }
        }

        public IConfigurationFacade ConfigurationTansactionalFacade(IConnectionHandler connectionHandler)
        {
            return new ConfigurationFacade(connectionHandler); 
        }
        public IWebSiteAliasFacade WebSiteAliasFacade
        {
            get { return new WebSiteAliasFacade(); }
        }

       
        public IWebSiteFacade WebSiteFacade
        {
            get { return new WebSiteFacade(); }
        }
        public IResourceFacade ResourceFacade
        {
            get { return new ResourceFacade(); }
        }
        public IUserFacade UserFacade
        {
            get { return new UserFacade(); }
        }




     
      
    }
}