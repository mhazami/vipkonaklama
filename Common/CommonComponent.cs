using System.Configuration;
using Radyn.Common.Component;
using Radyn.Common.Facade;
using Radyn.Common.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Common
{
    public class CommonComponent
    {
        private CommonComponent()
        {

        }
       
        public static string GetDefaultCulture
        {
            get
            {
                var languageSetting = (LanguageSetting)ConfigurationManager.GetSection("radyn/language");
                return languageSetting != null ? languageSetting.DefaultCulture : null;
            }
           
        }

        public IWebDesignLanguageFacade WebDesignLanguageFacade
        {
            get { return new WebDesignLanguageFacade(); }
        }

        public static CommonComponent Instance
        {
            get { return new CommonComponent(); }
        }
        public IBankFacade BankFacade
        {
            get { return new BankFacade(); }
        }
        
        public ILanguageFacade LanguageFacade
        {
            get { return new LanguageFacade(); }
        }
        public ILanguageFacade LanguageTransactionalFacade(IConnectionHandler connectionHandler)
        {
            return new LanguageFacade(connectionHandler);
        }
        public ILanguageContentFacade LanguageContentFacade
        {
            get { return new LanguageContentFacade(); }
        }
        public ILanguageContentFacade LanguageContentTransactionalFacade(IConnectionHandler connectionHandler)
        {
          return new LanguageContentFacade(connectionHandler);
        }
        public ICurrencyFacade CurrencyFacade
        {
            get
            {
                return new CurrencyFacade();
            }
        }
        public ICityFacade CityFacade
        {
            get
            {
                return new CityFacade();
            }
        }
        public IParishFacade ParishFacade
        {
            get
            {
                return new ParishFacade();
            }
        }
        public IUnitFacade UnitFacade
        {
            get
            {
                return new UnitFacade();
            }
        }
        public IProvinceFacade ProvinceFacade
        {
            get
            {
                return new ProvinceFacade();
            }
        }
        public ICountryFacade CountryFacade
        {
            get
            {
                return new CountryFacade();
            }
        }
        public ICountryIPRangeFacade CountryIpRangeFacade
        {
            get
            {
                return new CountryIPRangeFacade();
            }
        }
    }
}
