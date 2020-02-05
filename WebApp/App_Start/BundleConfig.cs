

using System.Web.Optimization;

namespace Radyn.WebApp.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Hotel




            //Our
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/App_Themes/RadynBase/Scripts/jquery-{version}.js",
                "~/App_Themes/RadynBase/Scripts/bootstrap.min.js",
                "~/App_Themes/RadynBase/Scripts/font-awesome.min.js"));

            bundles.Add(new StyleBundle("~/styles/css").Include(
                "~/App_Themes/RadynBase/CSS/bootstrap.min.css",
                "~/App_Themes/RadynBase/CSS/font-awesome.min.css",
                "~/App_Themes/RadynBase/CSS/r-slider.css"));

            bundles.Add(new StyleBundle("~/adminstyles/css").Include(
               System.Globalization.CultureInfo.CurrentCulture.Name=="fa-IR"? "~/App_Themes/RadynBase/CSS/RadynAlert.css": "~/App_Themes/RadynBase/CSS/RadynAlert-ltr.css",
                "~/App_Themes/RadynBase/CSS/RadynNotify.css"));
        }
    }
}