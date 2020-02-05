using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;
using Radyn.Common;
using Radyn.Security.DataStructure;
using Stimulsoft.Report;

namespace Radyn.WebApp.AppCode.Security
{
    public partial class SessionParameters
    {
        public static User User
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session["user"] != null)
                    return (User)HttpContext.Current.Session["user"];
                return null;
            }
            set
            {

                HttpContext.Current.Session["user"] = value;
            }
        }
        public static KeyValuePair<string, string>? Error
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session["Error"] != null)
                    return (KeyValuePair<string, string>)HttpContext.Current.Session["Error"];
                return null;
            }
            set
            {

                HttpContext.Current.Session["Error"] = value;
            }
        }
        public static StiReport Report
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session["Report"] != null)
                    return (StiReport)HttpContext.Current.Session["Report"];
                return null;
            }
            set
            {

                HttpContext.Current.Session["Report"] = value;
            }
        }

        public static bool HasLoginPasswordError
        {
            get
            {
                if (HttpContext.Current.Session["HasLoginPasswordError"] != null)
                    return (bool)HttpContext.Current.Session["HasLoginPasswordError"];
                return false;
            }
            set
            {
                HttpContext.Current.Session["HasLoginPasswordError"] = value;

            }
        }
        public static List<Operation> UserOperation
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session["Operations"] != null)
                    return (List<Operation>)HttpContext.Current.Session["Operations"];
                return null;
            }
            set
            {

                HttpContext.Current.Session["Operations"] = value;
            }
        }

        public static UserType UserType
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session["UserType"] != null)
                    return (UserType)HttpContext.Current.Session["UserType"];
                return UserType.None;
            }
            set
            {

                HttpContext.Current.Session["UserType"] = value;
            }
        }

        public static string Culture
        {
            get
            {
                if (HttpContext.Current.Session["culture"] == null)
                    HttpContext.Current.Session["culture"] = CommonComponent.GetDefaultCulture;
                return HttpContext.Current.Session["culture"].ToString();
            }
            set
            {

                HttpContext.Current.Session["culture"] = value;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(value ?? (CommonComponent.GetDefaultCulture ?? "tr-TR"));
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(value ?? (CommonComponent.GetDefaultCulture ?? "tr-TR"));
            }
        }

        public static WebDesign.DataStructure.User WebDesignUser
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session["WebDesignUser"] != null)
                    return (WebDesign.DataStructure.User)HttpContext.Current.Session["WebDesignUser"];
                return null;
            }
            set
            {

                HttpContext.Current.Session["WebDesignUser"] = value;
            }
        }

        public static WebDesign.DataStructure.WebSite CurrentWebSite
        {
            get
            {
                if (Web.HttpContext.Current != null && Web.HttpContext.Current.Session["CurrentWebSite"] != null)
                    return (WebDesign.DataStructure.WebSite)Web.HttpContext.Current.Session["CurrentWebSite"];
                return null;
            }
            set
            {

                Web.HttpContext.Current.Session["CurrentWebSite"] = value;
            }
        }

        public static bool WebSiteSessionStarted
        {
            get
            {
                if (Web.HttpContext.Current != null && Web.HttpContext.Current.Session["WebSiteSessionStarted"] != null)
                    return (bool)Web.HttpContext.Current.Session["WebSiteSessionStarted"];
                return false;
            }
            set
            {

                Web.HttpContext.Current.Session["WebSiteSessionStarted"] = value;
            }
        }


    }

    public enum UserType
    {
        None,
        Host,
        User,

    }
}