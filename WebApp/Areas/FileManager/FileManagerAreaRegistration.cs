using System.Web.Mvc;

namespace Radyn.WebApp.Areas.FileManager
{
    public class FileManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FileManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FileManager_default",
                "FileManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
