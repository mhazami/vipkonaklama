using System;
using Radyn.Advertisements.DataStructure;
using Radyn.Framework;

namespace Radyn.Advertisements.Facade.Interface
{
    public interface ITariffFacade : IBaseFacade<Tariff>
    {
        decimal CalculatePriceTariff(Guid tariffClassId, int? dayCount, int? clickCount, int? visitCount);
    }
}
