using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Comments
{
    public class CommentsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Comments";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Comments_default",
                "Comments/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
