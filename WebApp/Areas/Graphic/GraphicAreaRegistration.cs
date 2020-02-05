using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Graphic
{
    public class GraphicAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Graphic";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Graphic_default",
                "Graphic/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
