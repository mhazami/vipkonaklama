using System.Web.Mvc;

namespace Radyn.WebApp.Areas.EnterpriseNode
{
    public class EnterpriseNodeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "EnterpriseNode";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "EnterpriseNode_default",
                "EnterpriseNode/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
