using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.Facade.Interface
{
    public interface ITempFacade : IBaseFacade<Temp>
    {
       
        bool Insert(Temp temp, List<DiscountType> transactionDiscountAttaches);
        Transaction RemoveTempAndReturnTransaction(Guid tempId);
        Transaction RemoveTempAndReturnTransactionGroup(Guid tempId);
        bool Update(Temp temp, List<DiscountType> discountAttaches);
        bool GroupPayTemp(Temp temp, List<Guid> model);
       
    }
}
