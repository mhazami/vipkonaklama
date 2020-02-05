using Radyn.PhoneBook.DataStructure;

namespace Radyn.WebApp.AppCode.Security
{
    public partial class SessionParameters
    {
      

        public static Configuration PhoneBookConfiguration
        {
            get
            {
                if (Web.HttpContext.Current != null && Web.HttpContext.Current.Session["PhoneBookConfiguration"] != null)
                    return (Configuration)Web.HttpContext.Current.Session["PhoneBookConfiguration"];
                return null;
            }
            set
            {

                Web.HttpContext.Current.Session["PhoneBookConfiguration"] = value;
            }
        }
      
       
    }
}