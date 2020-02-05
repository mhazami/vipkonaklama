using System.Web.Mvc;

namespace Radyn.WebApp.Areas.FormGenerator
{
    public class FormGeneratorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FormGenerator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FormGenerator_default",
                "FormGenerator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
