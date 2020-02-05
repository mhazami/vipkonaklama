using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Facade.Interface;

namespace Radyn.Payment.Facade
{
    internal sealed class TransactionDiscountFacade : PaymentBaseFacade<TransactionDiscount>, ITransactionDiscountFacade
    {
        internal TransactionDiscountFacade() { }

        internal TransactionDiscountFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

      
      


    }
}
