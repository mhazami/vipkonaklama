using System;
using Radyn.Advertisements.BO;
using Radyn.Advertisements.DataStructure;
using Radyn.Advertisements.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.Facade
{
    internal sealed class TariffFacade : AdvertisementsBaseFacade<Tariff>, ITariffFacade
    {
        internal TariffFacade() { }

        internal TariffFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

      

        public decimal CalculatePriceTariff(Guid tariffClassId, int? dayCount, int? clickCount, int? visitCount)
        {
            try
            {
                var tariffClass = new TariffClassBO().Get(this.ConnectionHandler, tariffClassId);
                decimal Price = 0;
                if (dayCount != null && clickCount != null && visitCount != null)
                {
                    Price = (decimal)(((tariffClass.Price / tariffClass.PerDay) * dayCount) + ((tariffClass.Price / tariffClass.PerClick) * clickCount) + ((tariffClass.Price / tariffClass.PerVisit) * visitCount));
                }
                else if (dayCount == null && clickCount != null && visitCount != null)
                {
                    Price = (decimal)(((tariffClass.Price / tariffClass.PerClick) * clickCount) + ((tariffClass.Price / tariffClass.PerVisit) * visitCount));
                }
                else if (dayCount != null && clickCount != null)
                {
                    Price = (decimal)(((tariffClass.Price / tariffClass.PerDay) * dayCount) + ((tariffClass.Price / tariffClass.PerClick) * clickCount));
                }
                else if (clickCount != null)
                {
                    Price = (decimal)(((tariffClass.Price / tariffClass.PerClick) * clickCount));
                }
                else if (dayCount != null && visitCount != null)
                {
                    Price = (decimal)(((tariffClass.Price / tariffClass.PerDay) * dayCount) + ((tariffClass.Price / tariffClass.PerVisit) * visitCount));
                }
                else if (dayCount == null && visitCount != null)
                {
                    Price = (decimal)(((tariffClass.Price / tariffClass.PerVisit) * visitCount));
                }
                else
                {
                    var price = (tariffClass.Price / tariffClass.PerDay) * dayCount;
                    if (price != null)
                        Price = (decimal)price;
                }
                return Price;
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
