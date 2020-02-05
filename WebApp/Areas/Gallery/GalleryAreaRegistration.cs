using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Gallery
{
    public class GalleryAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Gallery";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Gallery_default",
                "Gallery/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
