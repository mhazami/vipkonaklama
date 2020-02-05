using System;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;

namespace Radyn.PaymentGateway.BO
{
    public class MelliBO
    {

        private static MelliGeteway _melliPaymentGatewayImplService;

        internal static MelliGeteway MelliPaymentGatewayImplService
        {
            get { return _melliPaymentGatewayImplService ?? new MelliGeteway(); }
        }

        public string MelliCallPayRequest(Transaction transaction, string terminalId, string username, string password, string requestAuthority)
        {
            string radynCallPayRequestInRadyn;
            transaction.AdditionalData = StringUtils.Encrypt(terminalId + "," + username + "," + password);
            var str = this.MelliCalpayRequest(transaction, requestAuthority);
            if (!string.IsNullOrEmpty(str))
            {
                var outstr = str.Split(',');
                transaction.Status = (int?)MelliEnums.AppStatusCode.WaitStatus;
                transaction.RefId = outstr[0];
                transaction.AdditionalData = StringUtils.Encrypt(terminalId + "," + username + "," + password + "," + outstr[1]);
                radynCallPayRequestInRadyn = Enums.Bank.Melli.CallBankUrl(transaction.Id, requestAuthority);
            }
            else radynCallPayRequestInRadyn = Enums.Bank.Melli.AfterCallBackUrl(transaction.Id, requestAuthority);
            if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return string.Empty;
            return radynCallPayRequestInRadyn;
        }


        public Transaction MelliCallBackPayRequest(Guid Id, string orderId, string timeStamp, string fp)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            var checkRequestStatus = this.MelliCheckRequestStatus(transaction, timeStamp, fp);
            if (string.IsNullOrEmpty(checkRequestStatus)) return transaction;
            var str = checkRequestStatus.Split(',');
            if (string.IsNullOrEmpty(str[2]) || str[2].ToEnum<MelliEnums.AppStatus>() == MelliEnums.AppStatus.NO_FAIL)
                transaction.Status = (int)MelliEnums.AppStatusCode.ak;
            else transaction.Status = str[0].ToInt();
            if (!String.IsNullOrEmpty(str[1]))
                transaction.SaleReferenceId = str[1].ToLong();
            if (transaction.Status == (int)MelliEnums.AppStatusCode.b &&
                str[2].ToEnum<MelliEnums.AppStatus>() == MelliEnums.AppStatus.COMMIT)
            {
                transaction.Done = true;
            }
            if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return null;
            return transaction;
        }
        public string MelliCheckRequestStatus(Transaction transaction, string timeStamp, string fp)
        {
            if (string.IsNullOrEmpty(timeStamp) || string.IsNullOrEmpty(fp)) return string.Empty;
            string appStatus;
            string refrenceNumber;
            var str = StringUtils.Decrypt(transaction.AdditionalData);
            var strterminal = str.Split(',');
            var terminalId = strterminal[0];
            var username = strterminal[1];
            var number = MelliPaymentGatewayImplService.CheckRequestStatus(transaction.InvoiceId, (long)transaction.Amount, username, terminalId, transaction.RefId, timeStamp, fp, out refrenceNumber, out appStatus);
            return number + "," + refrenceNumber + "," + appStatus;
        }

        public string MelliCalpayRequest(Transaction transaction, string requestAuthority)
        {
            string key;
            var str = StringUtils.Decrypt(transaction.AdditionalData);
            var strterminal = str.Split(',');
            var terminalId = strterminal[0];
            var username = strterminal[1];
            var password = strterminal[2];
            password = Utility.StringUtils.Decrypt(password).Trim();

            var paymentUtilityAdditionalData =
                MelliPaymentGatewayImplService.PaymentUtilityAdditionalData(username, (long)transaction.Amount,
                    transaction.InvoiceId, password, terminalId,
                   Enums.Bank.Melli.AfterCallBackUrl(transaction.Id, requestAuthority), DateTime.Now.ToString(), out key);
            return key + "," + paymentUtilityAdditionalData;
        }
    }
}
