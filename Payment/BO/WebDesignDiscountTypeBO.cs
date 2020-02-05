using System.Collections.Generic;
using System.Linq;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using Radyn.Payment.DataStructure;



namespace Radyn.Payment.BO
{
    internal class WebDesignDiscountTypeBO : BusinessBase<WebDesignDiscountType>
    {
        public  decimal CalulateAmountNew(IConnectionHandler paymentConnection, decimal amount, List<Payment.DataStructure.DiscountType> discountAttaches)
        {

            var discountTypeFacade = PaymentComponenets.Instance.DiscountTypeTransactionalFacade(paymentConnection);
            decimal outamout = amount;
            if (discountAttaches.Any())
            {
                var @where = discountTypeFacade.Where(x => x.Id.In(discountAttaches.Select(i => i.Id)));
                foreach (var transactionDiscountAttach in discountAttaches)
                {
                    var discountType = @where.FirstOrDefault(x=>x.Id==transactionDiscountAttach.Id);
                    if(discountType==null)continue;
                    if (discountType.IsPercent)
                        outamout -= (amount * discountType.ValidValue.ToDecimal()) / 100;
                    else
                        outamout -= discountType.ValidValue.ToDecimal();
                }
            }
           
            return outamout;
        }

       
    }
}
