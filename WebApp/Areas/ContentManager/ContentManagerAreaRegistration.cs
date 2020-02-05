using System.Web.Mvc;

namespace Radyn.WebApp.Areas.ContentManager
{
    public class ContentManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ContentManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ContentManager_default",
                "ContentManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "ContentManager_default1",
                "ContentManager/{controller}/{action}/{id}/{*catchall}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "ContentManager_pages",
                "Pages/{id}/{title}",
                new { controller = "Pages", action = "ShowPage", Id = UrlParameter.Optional, title = UrlParameter.Optional }
            );
        }
    }
}
