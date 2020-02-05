using System;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;

namespace Radyn.PaymentGateway.BO
{
    public class SamanBO
    {



        private static SamanGateway _samanGateway;

        internal static SamanGateway SamanGateway
        {
            get { return _samanGateway ?? new SamanGateway(); }
        }

        public string SamanCallPayRequest(Transaction transaction, string terminalId, string username, string password, string requestAuthority)
        {
            var data = string.Format("{0}#{1}#{2}#{3}#{4}#{5}#", username, terminalId, password, transaction.InvoiceId,
                 (long)transaction.Amount,
                Enums.Bank.Saman.AfterCallBackUrl(transaction.Id, requestAuthority));
            transaction.AdditionalData = StringUtils.Encrypt(data);
            var radynCallPayRequestInRadyn = Enums.Bank.Saman.CallBankUrl(transaction.Id, requestAuthority);
            return !PaymentComponenets.Instance.TransactionFacade.Update(transaction) ? string.Empty : radynCallPayRequestInRadyn;
        }
        internal Transaction SamanCallBackPayRequest(Guid Id, string orederId, string status, string refNum, string traceNo)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null)
                return null;
            if (status != "OK")
                throw new Exception(SamanEnums.StatusList[status]);
            if (string.IsNullOrEmpty(refNum))
                throw new Exception("گويا خريد شما توسط بانک تاييد شده است اما رسيد ديجيتالي شما تاييد نگشت");
            var value = !string.IsNullOrEmpty(transaction.AdditionalData) ? StringUtils.Decrypt(transaction.AdditionalData) : "";
            var data = value.Split('#');
            var result = SamanGateway.verifyTransaction(refNum, data[1]);
            if (result > 0)
            {
                if (result < transaction.Amount.ToString().ToDouble())
                {
                    transaction.Status = (int?) SamanEnums.Status.OkSmaller;
                }
                else
                {
                    if (result.Equals(transaction.Amount.ToString().ToDouble()))
                    {
                        transaction.Status = (int?) SamanEnums.Status.Ok;
                        transaction.Done = true;
                        
                    }
                    else if (result > transaction.Amount.ToString().ToDouble())
                    {
                        transaction.Done = true;
                        transaction.Status = (int?) SamanEnums.Status.OkUpper;
                       
                    }
                }
            }
            else

                transaction.Status = (int?) result.ToString().ToEnum<SamanEnums.Status>();
            transaction.RefId = refNum;
            transaction.SaleReferenceId = traceNo.ToLong();
            if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return null;
            return transaction;

        }





    }
}
