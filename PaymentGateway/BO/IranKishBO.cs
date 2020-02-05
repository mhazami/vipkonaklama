using System;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.IranKishToken;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;

namespace Radyn.PaymentGateway.BO
{
    public class IranKishBO
    {
        private static IranKishGeteway.MerchantService _iranKishGateway;

        internal static IranKishGeteway.MerchantService IranKishGateway
        {
            get { return _iranKishGateway ?? new IranKishGeteway.MerchantService(); }
        }

        public string IranKishCallPayRequest(Transaction transaction, string merchantId, string requestAuthority)
        {
            var client = new TokensClient();
            var afterCallBackUrl = Enums.Bank.IranKish.AfterCallBackUrl(transaction.Id, requestAuthority);
            var tokenResp = client.MakeToken(((uint)(transaction.Amount)).ToString(),
                merchantId, transaction.InvoiceId.ToString(),
                transaction.TrackYourOrderNum.ToString(), "",
                afterCallBackUrl, transaction.Description);
            var data = string.Format("{0}#{1}#", tokenResp.token, merchantId);
            transaction.AdditionalData = StringUtils.Encrypt(data);
            var radynCallPayRequestInRadyn = Enums.Bank.IranKish.CallBankUrl(transaction.Id, requestAuthority);
            return !PaymentComponenets.Instance.TransactionFacade.Update(transaction) ? string.Empty : radynCallPayRequestInRadyn;
        }

        internal Transaction IranKishCallBackPayRequest(Guid Id, string privateKey, string status, string refNum)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null)
                return null;
            if (string.IsNullOrEmpty(refNum))
                throw new Exception("گويا خريد شما توسط بانک تاييد شده است اما رسيد ديجيتالي شما تاييد نگشت");

            switch (status)
            {
                case "100":
                    var value = !string.IsNullOrEmpty(transaction.AdditionalData) ? StringUtils.Decrypt(transaction.AdditionalData) : "";
                    var data = value.Split('#');

                    #region Verify Transaction in Bank

                    var token = data[0];
                    var merchantId = data[1];
                    privateKey = StringUtils.Decrypt(privateKey);
                    var verifyEntity = new IranKishVerify.VerifyClient();

                    long finalResult = verifyEntity.KicccPaymentsVerification(token, merchantId, refNum, privateKey);
                    transaction.Status = finalResult.ToString().ToInt();

                    if (finalResult > 0)
                    {
                        transaction.Done = true;
                    }

                    #endregion

                    break;
            }


            transaction.RefId = refNum;
            //transaction.SaleReferenceId = orederId;

            if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return null;
            return transaction;
        }
    }
}
