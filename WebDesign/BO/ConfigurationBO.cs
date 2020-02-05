using System;
using System.Linq.Expressions;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using Radyn.WebDesign.DataStructure;
using Radyn.WebDesign.Definition;

namespace Radyn.WebDesign.BO
{
    internal class ConfigurationBO : BusinessBase<Configuration>
    {

        public override bool Insert(IConnectionHandler connectionHandler, Configuration configuration)
        {
            var id = configuration.Id;
            BOUtility.GetGuidForId(ref id);
            configuration.Id = id;
            configuration.Enabled = true;

            configuration.TerminalPassword = !string.IsNullOrEmpty(configuration.TerminalPassword)
                ? StringUtils.Encrypt(configuration.TerminalPassword)
                : null;
            configuration.SMSAccountPassword = !string.IsNullOrEmpty(configuration.SMSAccountPassword)
                ? StringUtils.Encrypt(configuration.SMSAccountPassword)
                : null;
            configuration.MailPassword = !string.IsNullOrEmpty(configuration.MailPassword)
                ? StringUtils.Encrypt(configuration.MailPassword)
                : null;
            configuration.CertificatePassword = !string.IsNullOrEmpty(configuration.CertificatePassword)
                ? StringUtils.Encrypt(configuration.CertificatePassword)
                : null;


            configuration.CRMServicePassword = !string.IsNullOrEmpty(configuration.CRMServicePassword)
                ? StringUtils.Encrypt(configuration.CRMServicePassword)
                : null;


           
            configuration.MerchantPublicKey = !string.IsNullOrEmpty(configuration.MerchantPublicKey)
                ? StringUtils.Encrypt(configuration.MerchantPublicKey)
                : null;


            configuration.MerchantPrivateKey = !string.IsNullOrEmpty(configuration.MerchantPrivateKey)
                ? StringUtils.Encrypt(configuration.MerchantPrivateKey)
                : null;
            return base.Insert(connectionHandler, configuration);
        }
        public string FillTempAdditionalData(IConnectionHandler connectionHandler, Guid congressId)
        {
            var configuration = new ConfigurationBO().SelectFirstOrDefault(connectionHandler,
                new Expression<Func<Configuration, object>>[]
                {
                    x => x.BankId, x => x.PaymentType, x => x.TerminalId, x => x.TerminalUserName,
                    x => x.TerminalPassword, x => x.CertificatePath, x => x.CertificatePassword, x => x.MerchantId,
                    x => x.MerchantPublicKey, x => x.MerchantPrivateKey
        
                }, x => x.Id == congressId);
            if (configuration == null) throw new Exception(Resources.WebDesign.Congresshasnotdonesetting);
            if (string.IsNullOrEmpty(configuration.PaymentType)) throw new Exception(Resources.WebDesign.Nopaymenthasbeenselectedfortheconference);
            var merchantPublicKey = (configuration.MerchantPublicKey != null ? configuration.MerchantPublicKey : String.Empty);
            var merchantPrivateKey = (configuration.MerchantPrivateKey != null ? configuration.MerchantPrivateKey : String.Empty);
            var certificatePath = (configuration.CertificatePath != null ? configuration.CertificatePath : String.Empty);
            var certificatePassword = (configuration.CertificatePassword != null ? configuration.CertificatePassword : String.Empty);
            var terminalId = (configuration.TerminalId != null ? configuration.TerminalId : String.Empty);
            var bankId = (configuration.BankId != null ? (byte)configuration.BankId : (byte?)null);
            var terminalUserName = (configuration.TerminalUserName != null ? configuration.TerminalUserName : String.Empty);
            var terminalPassword = (configuration.TerminalPassword != null ? configuration.TerminalPassword : String.Empty);
            var merchantId = (configuration.MerchantId != null ? configuration.MerchantId : String.Empty);
            var paymentType = (configuration.PaymentType != null ? configuration.PaymentType : String.Empty);
           return Radyn.WebDesign.Definition.Extention.FillTempAdditionalData(paymentType, bankId, Constants.PaytypeUrl, terminalId, terminalUserName, terminalPassword, certificatePath, certificatePassword, merchantId, merchantPublicKey, merchantPrivateKey);
        }

    }
}
