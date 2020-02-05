using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Slider
{
    public class SliderAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Slider";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Slider_default",
                "Slider/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
