using System.Web.Mvc;

namespace Radyn.WebApp.Areas.FAQ
{
    public class FAQAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FAQ";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FAQ_default",
                "FAQ/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
