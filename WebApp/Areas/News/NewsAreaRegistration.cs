using System.Web.Mvc;

namespace Radyn.WebApp.Areas.News
{
    public class NewsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "News";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "News_default",
                "News/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "News_default1",
                "News/{controller}/{action}/{id}/{*catchall}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
