using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Radyn.Web.Mvc;
using System.Web.Http;
using Radyn.WebApp.App_Start;


namespace Radyn.WebApp
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
        protected void Application_Error(object sender, EventArgs e)
        {
           
        }


        protected void Application_Start()
        {
            Application["reported"] = false;
            RadynMvcControlsRouteRegistrar.Register();
            FileManager.FileManagerComponent.RegisterFileManagerRoute();
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            AppCode.Themes.ThemeRegisterer.Register();
            AppCode.Themes.WebDesignThemesRegister.Register();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var thread = new Thread(ProcessOnStartUp);
            thread.Start();


        }

      
        public static void ProcessOnStartUp()
        {
            

        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            
           


        }
        protected void Session_Start()
        {
            Web.HttpContext.Current.Session["SesstionId"] = HttpContext.Current.Session.SessionID;

        }

        protected void Session_End()
        {
            if (Web.HttpContext.Current.Session["user"] != null)
            {
                Web.HttpContext.Current.Session.Abandon();
            }
            if (Web.HttpContext.Current.Session["SesstionId"] != null)
            {
                Web.HttpContext.Current.Session.Remove("SesstionId");
            }
        }

    }
}
