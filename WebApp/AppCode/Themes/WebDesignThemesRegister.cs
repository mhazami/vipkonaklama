using Radyn.Web.Mvc;
using Radyn.Web.Mvc.UI.Theme;

namespace Radyn.WebApp.AppCode.Themes
{
    public class WebDesignThemesRegister
    {
        public static void Register()
        {




            ThemeManager.AddTheme("WebDesignBase", true)

                .AddResource("/App_Themes/WebDesignBase/CSS/Menu/jquery-ui-1.10.1.custom.min.css", ResourceType.CssFile)
//                .AddResource("/App_Themes/WebDesignBase/Scripts/hoverIntent.js", ResourceType.JSFile)
//                .AddResource("/App_Themes/WebDesignBase/Scripts/superfish.js", ResourceType.JSFile)
//                .AddResource("/App_Themes/WebDesignBase/CSS/navbar.css", ResourceType.CssFile)
//                .AddResource("/App_Themes/WebDesignBase/Scripts/navbar.js", ResourceType.JSFile)
//                .AddResource("/App_Themes/WebDesignBase/CSS/r-slider.css", ResourceType.CssFile)
//                .AddResource("/App_Themes/WebDesignBase/Scripts/r-slider.js", ResourceType.JSFile)
//                .AddResource("/App_Themes/WebDesignBase/CSS/WebDesign-Common.css", ResourceType.CssFile)
//                .AddResource("/App_Themes/WebDesignBase/CSS/Menu/superfish.css", ResourceType.CssFile)
//                .AddResource("/App_Themes/WebDesignBase/CSS/Menu/superfish-vertical.css", ResourceType.CssFile)
//                .AddResource("/App_Themes/WebDesignBase/CSS/WebDesign-Common-Responsive.css", ResourceType.CssFile)


                ;




        }
    }
}