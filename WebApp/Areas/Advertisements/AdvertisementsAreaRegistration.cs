using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Advertisements
{
    public class AdvertisementsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Advertisements";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Advertisements_default",
                "Advertisements/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}