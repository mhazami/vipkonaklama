using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Tools;

namespace Radyn.Payment.Facade.Interface
{
    public interface ITransactionFacade : IBaseFacade<Transaction>
    {
        Transaction DocumnetPay(Guid tempId,Transaction transaction, HttpPostedFileBase fileBase);
        Transaction AfterGetway(Guid transactionId, string refId,string reccode,string salerefrence);
        Transaction UpdateTempAndAddTransaction(Guid tempId, Transaction transaction, byte payTypeId = (byte)Enums.PayType.OnlinePay);
        bool Done(Guid transactionId);
        List<Transaction> GetUserTransactions(Guid userId);
        int GetUserTempCount(Guid userId);
        
     
    }
}
