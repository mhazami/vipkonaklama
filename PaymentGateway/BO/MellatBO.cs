using System;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.Services;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;

namespace Radyn.PaymentGateway.BO
{
    public class MellatBO
    {
        //        public class Constants
        //        {
        //            public const long TerminalId = 1282459;
        //            public const string MellatTerminalUsername = "radyn";
        //            public const string MellatTerminalPassword = "radyn82";
        //        }
        private static MellatPaymentGatewayImplService _mellatPaymentGatewayImplService;

        internal static MellatPaymentGatewayImplService MellatPaymentGatewayImplService
        {
            get { return _mellatPaymentGatewayImplService ?? new MellatPaymentGatewayImplService(); }
        }
        public string MellatCallPayRequest(Transaction transaction, string terminalId, string username, string password, string requestAuthority)
        {
            var radynCallPayRequestInRadyn = string.Empty;
            transaction.AdditionalData = StringUtils.Encrypt(terminalId + "," + username + "," + password);
            var str = this.MellatCalPayRequest(transaction, requestAuthority);
            if (!string.IsNullOrEmpty(str))
            {
                var strArray = str.Split(new[] { ',' });
                transaction.Status = (int?)strArray[0].ToEnum<MellatEnums.StatusEnums>();
                if (transaction.Status == (byte)MellatEnums.StatusEnums.TransactionSucced)
                {
                    transaction.RefId = strArray[1];
                    radynCallPayRequestInRadyn = Enums.Bank.Mellat.CallBankUrl(transaction.Id, requestAuthority);
                }
                else radynCallPayRequestInRadyn = Enums.Bank.Mellat.AfterCallBackUrl(transaction.Id, requestAuthority);
            }
           
            if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return string.Empty;
            return radynCallPayRequestInRadyn;
        }

        public Transaction MellatCallBackPayRequest(Guid id, string recCode, string refId, long oredrId, long saleOrderId, long saleReferenceId)
        {
            string strout;
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(id);
            if (string.IsNullOrEmpty(recCode) || string.IsNullOrEmpty(refId) || string.IsNullOrEmpty(transaction.AdditionalData)) return transaction;
            transaction.SaleReferenceId = saleReferenceId;
            var str = recCode;
            string result = "";
            if (recCode.ToEnum<MellatEnums.StatusEnums>() == MellatEnums.StatusEnums.TransactionSucced)
            {
                result = this.MellatVerifyRequest(transaction);
                transaction.Status = result.ToInt();
                if (result.ToEnum<MellatEnums.StatusEnums>() == MellatEnums.StatusEnums.TransactionSucced)
                {
                    str = this.MellatSettleRequest(transaction);
                    transaction.Status = str.ToInt();

                }
                else
                {
                    result = this.MellatInquiryRequest(transaction);
                    transaction.Status = result.ToInt();
                    if (result.ToEnum<MellatEnums.StatusEnums>() == MellatEnums.StatusEnums.TransactionSucced)
                    {
                        str = this.MellatSettleRequest(transaction);
                        transaction.Status = str.ToInt();
                        transaction.Done = true;
                        
                    }
                    else
                    {
                        str = this.MellatReversalRequest(transaction);
                        transaction.Status = str.ToInt();
                    }
                }
            }

            strout = string.IsNullOrEmpty(str) ? result : str;
            if (!string.IsNullOrEmpty(strout))
                transaction.Status = strout.ToInt();
            if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return null;
            return transaction;
        }
       
        public string MellatSettleRequest(Transaction transaction)
        {
            var str = StringUtils.Decrypt(transaction.AdditionalData);
            var strterminal = str.Split(',');
            var terminalId = strterminal[0];
            var username = strterminal[1];
            var password = strterminal[2];
            if (transaction.SaleReferenceId == null || string.IsNullOrEmpty(transaction.AdditionalData)) return string.Empty;
            return MellatPaymentGatewayImplService.bpSettleRequest(terminalId.ToLong(),
                                                                                       username,
                                                                                       StringUtils.Decrypt(password).Trim(),
                                                                                       transaction.InvoiceId,
                                                                                       transaction.InvoiceId,
                                                                                       (long)transaction.SaleReferenceId);
        }



        public string MellatInquiryRequest(Transaction transaction)
        {

            var str = StringUtils.Decrypt(transaction.AdditionalData);
            var strterminal = str.Split(',');
            var terminalId = strterminal[0];
            var username = strterminal[1];
            var password = strterminal[2];
            if (transaction.SaleReferenceId == null || string.IsNullOrEmpty(transaction.AdditionalData)) return string.Empty;
            return MellatPaymentGatewayImplService.bpInquiryRequest(terminalId.ToLong(),
                                                                                      username,
                                                                                       StringUtils.Decrypt(password).Trim(),
                                                                                       transaction.InvoiceId,
                                                                                       transaction.InvoiceId,
                                                                                       (long)transaction.SaleReferenceId);

        }

        public string MellatReversalRequest(Transaction transaction)
        {
            var str = StringUtils.Decrypt(transaction.AdditionalData);
            var strterminal = str.Split(',');
            var terminalId = strterminal[0];
            var username = strterminal[1];
            var password = strterminal[2];
            if (transaction.SaleReferenceId == null || string.IsNullOrEmpty(transaction.AdditionalData)) return string.Empty;
            return MellatPaymentGatewayImplService.bpReversalRequest(terminalId.ToLong(),
                                                                                       username,
                                                                                        StringUtils.Decrypt(password).Trim(),
                                                                                       transaction.InvoiceId,
                                                                                       transaction.InvoiceId,
                                                                                       (long)transaction.SaleReferenceId);
        }

        public string MellatVerifyRequest(Transaction transaction)
        {
            var str = StringUtils.Decrypt(transaction.AdditionalData);
            var strterminal = str.Split(',');
            var terminalId = strterminal[0];
            var username = strterminal[1];
            var password = strterminal[2];
            if (transaction.SaleReferenceId == null || string.IsNullOrEmpty(transaction.AdditionalData)) return string.Empty;
            return MellatPaymentGatewayImplService.bpVerifyRequest(terminalId.ToLong(),
                                                                                      username,
                                                                                         StringUtils.Decrypt(password).Trim(),
                                                                                     transaction.InvoiceId,
                                                                                        transaction.InvoiceId,
                                                                                       (long)transaction.SaleReferenceId);
        }

        public string MellatCalPayRequest(Transaction transaction, string requestAuthority)
        {
            var datetime = DateTime.Now;
            var str = StringUtils.Decrypt(transaction.AdditionalData);
            var strterminal = str.Split(',');
            var terminalId = strterminal[0];
            var username = strterminal[1];
            var password = strterminal[2];
            return MellatPaymentGatewayImplService.bpPayRequest(terminalId.ToLong(),
                                                                        username,
                                                                           StringUtils.Decrypt(password).Trim(),
                                                                         transaction.InvoiceId, (long)transaction.Amount,
                                                                         string.Format("{0}{1}{2}", datetime.Year,
                                                                                       datetime.Month,
                                                                                       datetime.Day),
                                                                         string.Format("{0}{1}{2}", DateTime.Now.Hour,
                                                                                       datetime.Minute,
                                                                                       datetime.Second), transaction.Description,
                                                                        Enums.Bank.Mellat.AfterCallBackUrl(transaction.Id, requestAuthority),
                                                                         0);
        }
    }
}
