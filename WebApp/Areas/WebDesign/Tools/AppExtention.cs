using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebDesign;
using Radyn.WebDesign.Definition;

namespace Radyn.WebApp.Areas.WebDesign.Tools
{
    public static class AppExtention
    {
        public static string WebSiteMoudelName
        {
            get { return "WebSite-(" + SessionParameters.CurrentWebSite.Id + ")"; }
        }
        public static List<KeyValuePair<string, string>> SlideList()
        {
            var liste = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(
                    "1",Resources.WebDesign.BigSlideId),
                new KeyValuePair<string, string>(
                    "0",Resources.WebDesign.AverageSlideId),
                new KeyValuePair<string, string>(
                    "-1",Resources.WebDesign.MiniSlideId)
            };

            return liste;
        }
        public static List<KeyValuePair<string, string>> GetFormList()
        {
            var liste = new List<KeyValuePair<string, string>>
            {
                
                new KeyValuePair<string, string>(
                    WebSiteMoudelName + "-/home/ContactUs",Resources.WebDesign.ContactUsForm),
                new KeyValuePair<string, string>(
                    WebSiteMoudelName + "-/home/RequestDemo","درخواست سرویس"),
                new KeyValuePair<string, string>(
                    WebSiteMoudelName + "-/WebDesign/Userpanel/Complete",Resources.WebDesign.UserInFormationForm),
              
               

            };

            return liste;
        }
        public static string FillPaymentTypes(FormCollection collection)
        {

            var str = string.Empty; ;
            var enumerable = collection.AllKeys.FirstOrDefault(s => s.Equals("PaymentTypeId"));
            if (enumerable != null)
            {
                var strings = collection[enumerable].Split(',');
                foreach (var variable in strings)
                {
                    if (string.IsNullOrEmpty(variable)) continue;
                    if (!string.IsNullOrEmpty(str)) str += "-";
                    str += variable;
                }

            }
            return str;
        }
        public static KeyValuePair<string, string>? WebSitError()
        {

            if (SessionParameters.CurrentWebSite == null || SessionParameters.CurrentWebSite.Status == Enums.WebSiteStatus.NotRegistered)
                return new KeyValuePair<string, string>(Resources.WebDesign.NotFound, "/Content/Images/conference-alert/not-found.png");
            var status = SessionParameters.CurrentWebSite.Status;
            switch (status)
            {
                case Enums.WebSiteStatus.NotConfiged:

                    return new KeyValuePair<string, string>(Resources.WebDesign.NotConfig, "/Content/Images/conference-alert/setting.png");
                    break;

                case Enums.WebSiteStatus.Disabled:
                    return new KeyValuePair<string, string>(Resources.WebDesign.Disabled, "/Content/Images/conference-alert/disable.png");
                    break;

                case Enums.WebSiteStatus.NotRegistered:
                    return new KeyValuePair<string, string>(Resources.WebDesign.NotRegistered, "/Content/Images/conference-alert/not-found.png");
                    break;

            }
            return null;
        }
        public static string GetWebSiteResources(Enums.UseLayout layout = Enums.UseLayout.None)
        {
            if (SessionParameters.CurrentWebSite == null) return string.Empty;
            return WebDesignComponent.Instance.ResourceFacade.GetWebSiteResourceHtml(
                SessionParameters.CurrentWebSite.Id, SessionParameters.Culture, layout);
        }


        
        public static void GetWebSite()
        {

            var webSiteFacade = WebDesignComponent.Instance.WebSiteFacade;
            var authority = HttpContext.Current.Request.Url.Authority;
            if (authority.ToLower().StartsWith("www."))
                authority = authority.Replace("www.", "");
            if (HttpContext.Current.Session == null) return;
            if (SessionParameters.CurrentWebSite == null || SessionParameters.CurrentWebSite.Status != Enums.WebSiteStatus.NoProblem)
                SessionParameters.CurrentWebSite = webSiteFacade.GetWebSiteByUrl(authority);
            SessionParameters.Error = WebSitError();
            if (SessionParameters.UserOperation != null || SessionParameters.WebSiteSessionStarted || SessionParameters.CurrentWebSite.Status != Enums.WebSiteStatus.NoProblem) return;
            var list = CommonComponent.Instance.WebDesignLanguageFacade.GetValidList(SessionParameters.CurrentWebSite.Id);
            if (list != null && list.Any())
            {
                if (list.Count() == 1)
                    SessionParameters.Culture = list.First().Id;
            }
            SessionParameters.WebSiteSessionStarted = true;
        }


    }
}