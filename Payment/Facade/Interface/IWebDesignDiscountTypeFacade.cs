using Radyn.Framework;
using Radyn.Payment.DataStructure;
using System;
using System.Collections.Generic;


namespace Radyn.Payment.Facade.Interface
{
public interface IWebDesignDiscountTypeFacade : IBaseFacade<Payment.DataStructure.WebDesignDiscountType>
{
    IEnumerable<DiscountType> GetByWebId(Guid Webid);

    bool Insert(Guid Webid, DiscountType discountType, List<DiscountTypeSection> sectiontypes,List<DiscountTypeAutoCode> discountTypeAutoCodes);

    Guid UpdateStatusAfterTransactionGroupTemp(Guid id, Guid guid);
}
}
