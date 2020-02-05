using System.Configuration;

namespace Radyn.Payment.Tools
{
    public  class ModelView
    {      
        public class TerminalInfo : ConfigurationSection
        {
            [ConfigurationProperty("MerchantPublicKey")]
            public string MerchantPublicKey
            {
                get { return (string)this["MerchantPublicKey"]; }
                set { this["MerchantPublicKey"] = value; }
            }
            [ConfigurationProperty("MerchantPrivateKey")]
            public string MerchantPrivateKey
            {
                get { return (string)this["MerchantPrivateKey"]; }
                set { this["MerchantPrivateKey"] = value; }
            }
            [ConfigurationProperty("CertificatePath")]
            public string CertificatePath
            {
                get { return (string)this["CertificatePath"]; }
                set { this["CertificatePath"] = value; }
            }
            [ConfigurationProperty("CertificatePassword")]
            public string CertificatePassword
            {
                get { return (string)this["CertificatePassword"]; }
                set { this["CertificatePassword"] = value; }
            }
            [ConfigurationProperty("UserName")]
            public string UserName
            {
                get { return (string)this["UserName"]; }
                set { this["UserName"] = value; }
            }
            [ConfigurationProperty("Password")]
            public string Password
            {
                get { return (string)this["Password"]; }
                set { this["Password"] = value; }
            }
            [ConfigurationProperty("TerminalId")]
            public string TerminalId
            {
                get { return (string)this["TerminalId"]; }
                set { this["TerminalId"] = value; }
            }
            [ConfigurationProperty("MerchantId")]
            public string MerchantId
            {
                get { return (string)this["MerchantId"]; }
                set { this["MerchantId"] = value; }
            }

        }
    }
}
