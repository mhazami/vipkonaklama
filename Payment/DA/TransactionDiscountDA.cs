using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.DA
{
    public sealed class TransactionDiscountDA : DALBase<TransactionDiscount>
    {
        public TransactionDiscountDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
 
    }
    internal class TransactionDiscountCommandBuilder
    {
        
        
    }
}
