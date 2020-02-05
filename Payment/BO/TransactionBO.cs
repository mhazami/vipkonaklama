using System;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Tools;

namespace Radyn.Payment.BO
{
    internal class TransactionBO : BusinessBase<Transaction>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Transaction obj)
        {
           
                var id = obj.Id;
                BOUtility.GetGuidForId(ref id);
                obj.Id = id;
                obj.Done = false;
                if (obj.PayDate == DateTime.MinValue) obj.PayDate = DateTime.Now;
                if (string.IsNullOrEmpty(obj.CallBackUrl))
                    throw new Exception(Resources.Payment.ErrorInSaveTransaction);
                return base.Insert(connectionHandler, obj);
           
        }

        public bool ValidateTransaction(Transaction transaction)
        {
            if (transaction.PayTypeId != ((byte) Enums.PayType.Documnet)) return true;
            if (string.IsNullOrEmpty(transaction.DocNo))
                throw new Exception(Resources.Payment.PleaseEnterDocumentNumber);
            if (transaction.PayDate == DateTime.MinValue)
                throw new Exception(Resources.Payment.PleaseEnterPayDate);
            return true;
        }

        public Transaction InsertTransactionFromTemp(IConnectionHandler connectionHandler, Temp temp, Transaction newtransaction, byte paytype)
        {
            var bo = new TransactionBO();
            var orderId = bo.Max(connectionHandler, x => x.InvoiceId) + 1;
            var transaction = new Transaction
            {
                Amount = temp.Amount,
                PayTypeId = paytype,
                InvoiceId = orderId,
                Status = null,
                Description = temp.Description,
                PayerId = temp.PayerId,
                PayerTitle = temp.PayerTitle,
                CallBackUrl = temp.CallBackUrl,
                CurrencyType = temp.CurrencyType,
                AdditionalData = temp.AdditionalData,
                TrackYourOrderNum = temp.TrackYourOrderNum

            };
            if (newtransaction != null)
            {
                transaction.DocNo = newtransaction.DocNo;
                transaction.PayDate = newtransaction.PayDate;
                transaction.DocScan = newtransaction.DocScan;
                transaction.AccountId = newtransaction.AccountId;
                transaction.OnlineBankId = newtransaction.OnlineBankId;

            }

            if (!ValidateTransaction(transaction)) return null;
            if (!this.Insert(connectionHandler, transaction))
                throw new Exception(Resources.Payment.ErrorInSaveTransaction);
            var transactionDiscountBo = new TransactionDiscountBO();
            var discountAttaches = new TempDiscountBO().Where(connectionHandler, discount => discount.TempId == temp.Id);
            foreach (var discountAttach in discountAttaches)
            {
                if (discountAttaches.Count > 1)
                    System.Threading.Thread.Sleep(1000);
                var transactionDiscount = new TransactionDiscount
                {
                    TransactionId = transaction.Id,
                    DiscountTypeId = discountAttach.DiscountTypeId,
                    AttachId = discountAttach.AttachId
                };
                if (!transactionDiscountBo.Insert(connectionHandler, transactionDiscount))
                    throw new Exception(Resources.Payment.ErrorInSaveTransactionDiscount);
            }
            return transaction;
        }
        public Transaction UpdateTempAndAddTransaction(IConnectionHandler connectionHandler, Guid tempId, Transaction transaction, byte payTypeId)
        {
            if (tempId == Guid.Empty)
                throw new Exception(Resources.Payment.TransactionNotFound);
            var tempBo = new TempBO();
            var temp = tempBo.Get(connectionHandler, tempId);
            if (temp == null) return null;
            var newtransaction = this.InsertTransactionFromTemp(connectionHandler, temp, transaction, payTypeId);
            temp.Date = DateTime.Now;
            temp.TransactionId = newtransaction.Id;
            if (!tempBo.Update(connectionHandler, temp))
                throw new Exception(Resources.Payment.ErrorInSaveTransaction);
            var list = tempBo.Where(connectionHandler, x => x.ParentId == tempId);
            foreach (var temp1 in list)
            {
                if (temp1 == null || temp1.TransactionId.HasValue) continue;
                temp1.TransactionId = newtransaction.Id;
                if (!tempBo.Update(connectionHandler, temp1))
                    throw new Exception(Resources.Payment.ErrorInSaveTransaction);
            }
            return newtransaction;
        }

       
    }
}
