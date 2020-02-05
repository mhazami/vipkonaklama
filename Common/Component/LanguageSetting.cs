using System.Configuration;

namespace Radyn.Common.Component
{
    public class LanguageSetting : ConfigurationSection
    {
        [ConfigurationProperty("defaultCulture")]
        public string DefaultCulture
        {
            get { return (string)this["defaultCulture"]; }
            set { this["defaultCulture"] = value; }
        }

    }
}
