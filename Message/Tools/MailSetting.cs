using System.Configuration;

namespace Radyn.Message.Tools
{
    public class MailSetting : ConfigurationSection
    {
        [ConfigurationProperty("mailHost")]
        public string MailHost
        {
            get { return (string)this["mailHost"]; }
            set { this["mailHost"] = value; }
        }

        [ConfigurationProperty("mailPort")]
        public short MailPort
        {
            get { return (short)this["mailPort"]; }
            set { this["mailPort"] = value; }
        }

        [ConfigurationProperty("mailPassword")]
        public string MailPassword
        {
            get { return (string)this["mailPassword"]; }
            set { this["mailPassword"] = value; }
        }

        [ConfigurationProperty("mailUserName")]
        public string MailUserName
        {
            get { return (string)this["mailUserName"]; }
            set { this["mailUserName"] = value; }
        }

        [ConfigurationProperty("mailFrom")]
        public string MailFrom
        {
            get { return (string)this["mailFrom"]; }
            set { this["mailFrom"] = value; }
        }

        [ConfigurationProperty("mailSenderDisplayName")]
        public string MailSenderDisplayName
        {
            get { return (string)this["mailSenderDisplayName"]; }
            set { this["mailSenderDisplayName"] = value; }
        }


        [ConfigurationProperty("enableSSL")]
        public bool EnableSSL
        {
            get { return (bool)this["enableSSL"]; }
            set { this["enableSSL"] = value; }
        }

    }
}
