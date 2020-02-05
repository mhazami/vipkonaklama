using System.Collections.Generic;

using Radyn.Framework;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.Facade.Interface
{
public interface IDiscountTypeFacade : IBaseFacade<DiscountType>
{
    bool Update(DiscountType discountType, List<DiscountTypeAutoCode> discountTypeAutoCodeFacades);
    bool Update(DiscountType discountType, string modelname, List<DiscountTypeSection> sectiontypes, List<DiscountTypeAutoCode> discountTypeAutoCodeFacades);
    bool Insert(DiscountType discountType, List<DiscountTypeAutoCode> discountTypeAutoCodeFacades);
    bool Insert(DiscountType discountType, List<DiscountTypeSection> sectiontypes, List<DiscountTypeAutoCode> discountTypeAutoCodeFacades);
    
}
}
