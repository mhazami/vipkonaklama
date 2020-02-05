using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.Facade.Interface
{
public interface IDiscountTypeAutoCodeFacade : IBaseFacade<DiscountTypeAutoCode>
{
    List<DiscountTypeAutoCode> GenerateAutoCode(Guid discounttypeId,int count,int characterCount);
}
}
