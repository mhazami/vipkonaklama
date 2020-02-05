using Radyn.Web.Mvc;
using Radyn.Web.Mvc.UI;
using Radyn.Web.Mvc.UI.Theme;


namespace Radyn.WebApp.AppCode.Themes
{
    public class ThemeRegisterer
    {
        public static void Register()
        {
            ThemeManager.AddTheme("RadynBase", true)
                .AddResource(Application.CurrentApplicationPath + "/App_Themes/RadynBase/Scripts/jquery-1.11.3.min.js", ResourceType.JSFile)
                 //.AddResource(Application.CurrentApplicationPath + "/Scripts/Radyn/common.js", ResourceType.JSFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/RadynBase/CSS/jquery-ui.min.css", ResourceType.CssFile)
                .AddResource(Application.CurrentApplicationPath + "/Scripts/jquery-ui.js", ResourceType.JSFile)
                 .AddResource(Application.CurrentApplicationPath + "/Scripts/Radyn/RadynIIFE.js", ResourceType.JSFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/RadynBase/CSS/font-awesome.min.css", ResourceType.CssFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/RadynBase/Scripts/font-awesome.min.js", ResourceType.JSFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/RadynBase/Scripts/jquery.fancybox.min.js", ResourceType.JSFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/RadynBase/CSS/font-awesome.min.css", ResourceType.CssFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/RadynBase/CSS/jquery.fancybox.min.css", ResourceType.CssFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/RadynBase/Scripts/font-awesome.min.js", ResourceType.JSFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/RadynBase/CSS/bootstrap.min.css", ResourceType.CssFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/RadynBase/Scripts/bootstrap.min.js", ResourceType.JSFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/RadynBase/CSS/RadynNotify.css", ResourceType.CssFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/RadynBase/Scripts/RadynNotify.js", ResourceType.JSFile)
                 .AddResource(Application.CurrentApplicationPath +(System.Globalization.CultureInfo.CurrentCulture.Name == "fa-IR" ? "/App_Themes/RadynBase/CSS/RadynAlert.css" : "/App_Themes/RadynBase/CSS/RadynAlert-ltr.css") , ResourceType.CssFile);

            ThemeManager.AddTheme("Common", true)
                .AddResource(Application.CurrentApplicationPath + "/App_Themes/Common/CSS/materialize.min.css", ResourceType.CssFile)
                .AddResource(Application.CurrentApplicationPath + "/App_Themes/Common/Scripts/materialize.min.js", ResourceType.JSFile)
                .AddResource(Application.CurrentApplicationPath + "/App_Themes/Common/CSS/Style-Common.css", ResourceType.CssFile);

             ThemeManager.AddTheme("LookUpBase", true)
               
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/MainBase/Radyn-Controlls/Grid/Base/Radyn-Grid-base.css", ResourceType.CssFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/MainBase/Radyn-Controlls/Grid/Base/Radyn-Grid-RTL.css", ResourceType.CssFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/MainBase/Radyn-Controlls/Grid/Theme/Radyn-Grid-Silver.css", ResourceType.CssFile)
                 .AddResource(Application.CurrentApplicationPath + "/App_Themes/MainBase/CSS/RDN-Silver.css", ResourceType.CssFile)
                 ;


            ThemeManager.AddTheme("MainBase", true)
                .AddResource(Application.CurrentApplicationPath + "/App_Themes/MainBase/CSS/RadynCommon.css", ResourceType.CssFile)
                .AddResource(Application.CurrentApplicationPath + "/App_Themes/MainBase/Radyn-Controlls/Grid/Base/Radyn-Grid-base.css", ResourceType.CssFile)
                .AddResource(Application.CurrentApplicationPath + "/App_Themes/MainBase/Radyn-Controlls/Grid/Base/Radyn-Grid-RTL.css", ResourceType.CssFile)
                .AddResource(Application.CurrentApplicationPath + "/App_Themes/MainBase/Radyn-Controlls/Grid/Theme/Radyn-Grid-Silver.css", ResourceType.CssFile)
                .AddResource(Application.CurrentApplicationPath + "/App_Themes/MainBase/CSS/RDN-Silver.css", ResourceType.CssFile)
                .AddResource(Application.CurrentApplicationPath + "/App_Themes/MainBase/Script/MenuNav.js", ResourceType.JSFile);


            ThemeManager.AddTheme("RadynEditor", true)
                .AddResource(Application.CurrentApplicationPath + "/Scripts/Radyn/BootstrapEditor/Css/summernote.css", ResourceType.CssFile)
                .AddResource(Application.CurrentApplicationPath + "/Scripts/Radyn/BootstrapEditor/js/summernote.js", ResourceType.JSFile)
                .AddResource(Application.CurrentApplicationPath + "/Scripts/Radyn/BootstrapEditor/Css/custome.css", ResourceType.CssFile);


        }
    }
}