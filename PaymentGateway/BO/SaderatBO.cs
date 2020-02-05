using System;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;

namespace Radyn.PaymentGateway.BO
{
    public class SaderatBO
    {



        private static SaderdatGateway _saderdatGateway;

        internal static SaderdatGateway SaderdatGateway
        {
            get { return _saderdatGateway ?? new SaderdatGateway(); }
        }

        public string SaderatCallPayRequest(Transaction transaction, string terminalId, string username, string password, string requestAuthority)
        {
            var data = string.Format("{0}#{1}#{2}#{3}#{4}#{5}#", username, terminalId,password, transaction.InvoiceId,
                 (long)transaction.Amount,
                Enums.Bank.Saderat.AfterCallBackUrl(transaction.Id, requestAuthority));
            transaction.AdditionalData = StringUtils.Encrypt(data);
            var radynCallPayRequestInRadyn = Enums.Bank.Saderat.CallBankUrl(transaction.Id, requestAuthority);
            return !PaymentComponenets.Instance.TransactionFacade.Update(transaction) ? string.Empty : radynCallPayRequestInRadyn;
        }



        public Transaction SaderatCallBackPayRequest(Guid Id, string orederId, string resultCode, string referenceId)
        {

            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            transaction.RefId = referenceId;
            transaction.Status = (int?) resultCode.ToEnum<SaderatEnums.resultCode>();
            if (resultCode.ToEnum<SaderatEnums.resultCode>() == SaderatEnums.resultCode.Succed)
            {
                var checkRequestStatus = this.SaderatCheckRequestStatus(transaction, resultCode, referenceId);
                transaction.Status = (int?)checkRequestStatus.ToString().ToEnum<SaderatEnums.resultCode>();
                if (checkRequestStatus >= 1000)
                {
                     transaction.Done = true;  
                     
                }
            }
            
            if (!PaymentComponenets.Instance.TransactionFacade.Update(transaction)) return null;
            return transaction;
        }
        public long SaderatCheckRequestStatus(Transaction transaction, string resultCode, string referenceId)
        {
            if (string.IsNullOrEmpty(resultCode) || string.IsNullOrEmpty(referenceId)) return 0;
            var str = StringUtils.Decrypt(transaction.AdditionalData);
            var strterminal = str.Split('#');
            var merchandId = strterminal[0];
            return SaderdatGateway.verify(new verifyRequest() { merchantId = merchandId, referenceNumber = referenceId });
            
        
        }




       
    }
}
