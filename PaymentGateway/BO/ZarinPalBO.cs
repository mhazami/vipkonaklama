using System;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;

namespace Radyn.PaymentGateway.BO
{
    public class ZarinPalBO
    {

        public string ZarinPalCallPayRequest(Transaction transaction, string merchantId, string requestAuthority)
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            //تقسم بر ده برای تبدیل زیال به تومان
            int newAmount = ((int)transaction.Amount) / 10;

            ZarinPalService.PaymentGatewayImplementationServicePortTypeClient zarinPalService = new ZarinPalService.PaymentGatewayImplementationServicePortTypeClient();
            string authority = "";
            int status = zarinPalService.PaymentRequest(merchantId, newAmount, transaction.Description, "", "", Enums.Bank.ZarinPal.AfterCallBackUrl(transaction.Id, requestAuthority), out authority);
            if (status == 100)
            {
                var data = $"{merchantId}#{transaction.InvoiceId}#{Enums.Bank.ZarinPal.AfterCallBackUrl(transaction.Id, requestAuthority)}#{authority}";
                transaction.AdditionalData = StringUtils.Encrypt(data);
                var radynCallPayRequestInRadyn = Enums.Bank.ZarinPal.CallBankUrl(transaction.Id, requestAuthority);
                return !PaymentComponenets.Instance.TransactionFacade.Update(transaction) ? string.Empty : radynCallPayRequestInRadyn;
            }
            else
            {
                throw new Exception("امکان اتصال به درگاه وجود ندارد");
            }
        }


        internal Transaction ZarinPalCallBackPayRequest(Guid transId, string status, string authority)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(transId);
            if (transaction == null)
                return null;

            string value = transaction.AdditionalData;
            string deValue = StringUtils.Decrypt(value);
            var data = deValue.Split('#');

            if (status.Equals("OK"))
            {
                int Amount = ((int)transaction.Amount) / 10;
                long RefID;
                string merchantId = data[0];
                System.Net.ServicePointManager.Expect100Continue = false;
                ZarinPalService.PaymentGatewayImplementationServicePortTypeClient zarinPalService = new ZarinPalService.PaymentGatewayImplementationServicePortTypeClient();

                int Status = zarinPalService.PaymentVerification(merchantId, authority, Amount, out RefID);

                transaction.RefId = RefID.ToString();
                transaction.Status = Status;
                if (Status == 100)
                {
                    transaction.Done = true;
                }

                if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return null;
                return transaction;
            }
            else
            {
                //انصراف توسط کاربر یا عدم موفقیت پرداخت
                transaction.Status = 404;
                if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return null;
                return transaction;
            }
        }
    }
}
