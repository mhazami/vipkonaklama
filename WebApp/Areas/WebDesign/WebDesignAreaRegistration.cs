using System.Web.Mvc;

namespace Radyn.WebApp.Areas.WebDesign
{
    public class WebDesignAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WebDesign";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WebDesign_default",
                "WebDesign/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}