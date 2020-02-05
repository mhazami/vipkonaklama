using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.Facade.Interface
{
public interface IDiscountTypeSectionFacade : IBaseFacade<DiscountTypeSection>
{

    List<TempDiscount> GetDiscountTypes(string modualName, byte section, Guid? tempId = null);
    bool Insert(List<DiscountTypeSection> discountTypeSections);
    bool Update(string modelname,List<DiscountTypeSection> discountTypeSections);
    IEnumerable<DiscountTypeSection> GetByModelName(string modelname);
  
}
}
