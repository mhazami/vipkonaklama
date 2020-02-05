using System;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.GhavaminGeteway;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Radyn.PaymentGateway.BO
{
    public class GhavaminBO
    {


        public string GhavaminCallPayRequest(Transaction transaction, string merchantId, string wsp1, string wsp2, string requestAuthority, string customerId)
        {
            BypassCertificateError();
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
           delegate (
               Object sender1,
               X509Certificate certificate,
               X509Chain chain,
               SslPolicyErrors sslPolicyErrors)
           {
               return true;
           };

            string newAmount = ((long) transaction.Amount).ToString();

            GhavaminGeteway.merchant client = new GhavaminGeteway.merchantClient();
            GhavaminGeteway.getPaymentTicketRequest req = new getPaymentTicketRequest()
            {
                merchantId = merchantId,
                amount = newAmount,
                wsp1 = wsp1,
                wsp2 = wsp2,
                paymentId = transaction.InvoiceId.ToString(),
                customerId = customerId
            };
            GhavaminGeteway.getPaymentTicketRequest1 request = new getPaymentTicketRequest1()
            {
                getPaymentTicketRequest = req
            };
            GhavaminGeteway.getPaymentTicketResponse1 Token = new getPaymentTicketResponse1();
            Token = client.getPaymentTicket(request);
            string paymentTicket = Token.getPaymentTicketResponse.paymentTicket;


            var data = string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}", merchantId, transaction.InvoiceId,
                paymentTicket, Enums.Bank.Ghavamin.AfterCallBackUrl(transaction.Id, requestAuthority), wsp1, wsp2, customerId);
            transaction.AdditionalData = StringUtils.Encrypt(data);
            var radynCallPayRequestInRadyn = Enums.Bank.Ghavamin.CallBankUrl(transaction.Id, requestAuthority);
            return !PaymentComponenets.Instance.TransactionFacade.Update(transaction) ? string.Empty : radynCallPayRequestInRadyn;
        }

        internal Transaction GhavaminCallBackPayRequest(Guid Id, string resultCode, string SayanRef, string paymentID)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null)
                return null;

            string value = transaction.AdditionalData;
            string deValue = StringUtils.Decrypt(value);
            var data = deValue.Split('#');
            string PaymentTicket = data[2];

            //if (!String.IsNullOrEmpty(SayanRef))
            //    SayanRef = "0";
            //if (!String.IsNullOrEmpty(paymentID))
            //    paymentID = "0";

            if (Int32.Parse(resultCode) == 00)
            {
                BypassCertificateError();
                GhavaminGeteway.merchant client = new merchantClient();
                GhavaminGeteway.verifyRequest vrRequest = new GhavaminGeteway.verifyRequest();
                GhavaminGeteway.verifyRequest1 vrRequest1 = new verifyRequest1();

                vrRequest.merchantId = data[0];
                vrRequest.wsp1 = data[4];
                vrRequest.wsp2 = data[5];
                vrRequest.paymentTicket = PaymentTicket;

                vrRequest1.verifyRequest = vrRequest;

                GhavaminGeteway.verifyResponse status = new verifyResponse();
                GhavaminGeteway.verifyResponse1 status1 = new GhavaminGeteway.verifyResponse1();

                status1.verifyResponse = status;

                status1 = client.verify(vrRequest1);


                if (Int32.Parse(status1.verifyResponse.resultCode) < 0)
                {
                    transaction.Status = Int32.Parse(status1.verifyResponse.resultCode);
                }
                else
                {
                    transaction.Status = Int32.Parse(resultCode);
                    transaction.Done = true;
                }
            }
            else
            {
                transaction.Status = Int32.Parse(resultCode);
            }
            transaction.RefId = SayanRef;
            if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return null;
            return transaction;
        }


        void BypassCertificateError()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                delegate (
                    Object sender1,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        }

    }
}
