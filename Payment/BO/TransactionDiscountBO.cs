using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.BO
{
    internal class TransactionDiscountBO : BusinessBase<TransactionDiscount>
    {
      
        public override bool Insert(IConnectionHandler connectionHandler, TransactionDiscount obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
   

    
    }
}
