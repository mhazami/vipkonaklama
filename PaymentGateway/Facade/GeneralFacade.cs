using System;
using System.Web;
using Radyn.Framework;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Tools;
using Radyn.PaymentGateway.BO;
using Radyn.PaymentGateway.Facade.Interface;
using Enums = Radyn.PaymentGateway.Tools.Enums;

namespace Radyn.PaymentGateway.Facade
{
    internal class GeneralFacade : IGeneralFacade
    {

        public string OnlinePay(Guid tempId, Transaction transaction, string authority, string merchantId, string terminalId, string userName, string password, string certificatePath, string certificatePassword, string merchantPublicKey, string merchantPrivateKey)
        {
            try
            {
                var callPayRequest = string.Empty;
                return CallPayRequest(tempId, transaction, authority, merchantId, terminalId, userName, password, callPayRequest, certificatePath, certificatePassword, merchantPublicKey, merchantPrivateKey);
            }
            catch (KnownException knownException)
            {
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source, knownException.StackTrace);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public string OnlinePay(Guid tempId, Transaction transaction, string authority)
        {
            try
            {
                var callPayRequest = string.Empty;
                var terminal = Extentions.TerminalInfo;
                return CallPayRequest(tempId, transaction, authority, terminal.MerchantId, terminal.TerminalId, terminal.UserName, terminal.Password, callPayRequest, terminal.CertificatePath, terminal.CertificatePassword, terminal.MerchantPublicKey, terminal.MerchantPrivateKey);
            }
            catch (KnownException knownException)
            {
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source, knownException.StackTrace);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        private static string CallPayRequest(Guid tempId, Transaction transaction, string authority, string merchantId, string terminalId,
         string userName, string password, string callPayRequest, string certificatePath, string certificatePassword, string merchantPublicKey, string merchantPrivateKey)
        {
            var addTransaction = PaymentComponenets.Instance.TransactionFacade.UpdateTempAndAddTransaction(tempId, transaction);
            if (addTransaction == null) return string.Empty;
            if (transaction.OnlineBankId == null) return string.Empty;
            var onlineBankId = (Enums.Bank)transaction.OnlineBankId;
            switch (onlineBankId)
            {
                case Enums.Bank.Radyn:
                    break;
                case Enums.Bank.Mellat:
                    callPayRequest = new MellatBO().MellatCallPayRequest(addTransaction, terminalId, userName, password,
                        authority);
                    break;
                case Enums.Bank.Melli:
                    callPayRequest = new MelliBO().MelliCallPayRequest(addTransaction, terminalId, userName, password, authority);
                    break;
//                case Enums.Bank.Melli:
//                    callPayRequest = new MelliApiBO().MelliCallPayRequest(addTransaction, terminalId, userName, password, authority);
//                    break;
                case Enums.Bank.Pasargad:
                    callPayRequest = new PasargadBO().PasargadPayRequest(addTransaction, terminalId, userName, password,
                        authority);
                    break;
                case Enums.Bank.Saderat:
                    callPayRequest = new SaderatBO().SaderatCallPayRequest(addTransaction, terminalId, userName, password, authority);
                    break;
                case Enums.Bank.Saman:
                    callPayRequest = new SamanBO().SamanCallPayRequest(addTransaction, terminalId, userName, password, authority);
                    break;
                case Enums.Bank.EghtesadeNovin:
                    var fullPath = HttpContext.Current.Server.MapPath(certificatePath);

                    callPayRequest = new EghtesadeNovinBO().EghtesadeNovinCallPayRequest(addTransaction, terminalId, userName, password, authority, fullPath, certificatePassword);
                    break;
                case Enums.Bank.IranKish:
                    callPayRequest = new IranKishBO().IranKishCallPayRequest(addTransaction, merchantId, authority);
                    break;
                case Enums.Bank.Mabna:
                    callPayRequest = new MabnaBO().MabnaCallPayRequest(addTransaction, merchantId, terminalId, authority, merchantPublicKey, merchantPrivateKey);
                    break;
                case Enums.Bank.Ghavamin:
                    callPayRequest = new GhavaminBO().GhavaminCallPayRequest(addTransaction, merchantId, merchantPublicKey, merchantPrivateKey,
                        authority, null);
                    break;
                case Enums.Bank.ZarinPal:
                    callPayRequest = new ZarinPalBO().ZarinPalCallPayRequest(addTransaction, merchantId, authority);
                    break;
            }
            return callPayRequest;
        }
    }
}
