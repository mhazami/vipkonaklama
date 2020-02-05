using System.Configuration;

namespace Radyn.WebApp.AppCode.Security
{
    public static class Settings
    {
        public static bool ShowMessageInnerException
        {
            get
            {
                if (ConfigurationManager.AppSettings["ShowInnerExcption"] != null)
                    return bool.Parse(ConfigurationManager.AppSettings["ShowInnerExcption"]);
                return false;
            }
        }

      

    }
  
}