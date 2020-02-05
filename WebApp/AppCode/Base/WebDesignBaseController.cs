using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.Areas.WebDesign.Tools;
using Radyn.WebDesign.DataStructure;

namespace Radyn.WebApp.AppCode.Base
{
    [WebDesignHost]
    public class WebDesignBaseController : LocalizedController
    {

        public WebSite WebSite
        {
            get
            {
                if (System.Web.HttpContext.Current.Session == null) return null;
                if (SessionParameters.CurrentWebSite == null) AppExtention.GetWebSite();
                 return SessionParameters.CurrentWebSite;

            }


        }


       


    }
    [WebDesignHost]
    public class WebDesignBaseController<T> : LocalizedController<T> where T : class
    {

        public WebSite WebSite
        {
            get
            {
                if (System.Web.HttpContext.Current.Session == null) return null;
                if (SessionParameters.CurrentWebSite == null) AppExtention.GetWebSite();
                return SessionParameters.CurrentWebSite;

            }


        }





    }
}