using System.Collections.Generic;
using System.Web;
using Radyn.PhoneBook;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebDesign.Definition;

namespace Radyn.WebApp.Areas.PhoneBook.Tools
{
    public static class AppExtention
    {

        public static void GetPhoneBookConfig()
        {


            if (HttpContext.Current.Session == null) return;
            if (SessionParameters.PhoneBookConfiguration == null && SessionParameters.CurrentWebSite != null && SessionParameters.CurrentWebSite.Status == Enums.WebSiteStatus.NoProblem)
                SessionParameters.PhoneBookConfiguration = PhoneBookComponenets.Instance.ConfigurationFacade.Get(SessionParameters.CurrentWebSite.Id);
            if (SessionParameters.PhoneBookConfiguration == null)
                SessionParameters.Error = new KeyValuePair<string, string>(Resources.PhoneBook.NotConfig,
                    "/Content/Images/conference-alert/setting.png");
            else
                SessionParameters.Error = null;



        }


    }
}