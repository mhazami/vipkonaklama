using Radyn.PhoneBook.Facade;
using Radyn.PhoneBook.Facade.Interface;


namespace Radyn.PhoneBook
{
    public class PhoneBookComponenets
    {
        internal PhoneBookComponenets()
        {

        }

        public IConfigurationFacade ConfigurationFacade
        {
            get { return new ConfigurationFacade(); }
        }

        private static PhoneBookComponenets _instance;
        public static PhoneBookComponenets Instance
        {
            get { return _instance ?? (_instance = new PhoneBookComponenets()); }
        }
        public IPersonFacade PersonFacade
        {
            get { return new PersonFacade(); }
        }
        public IPhoneTypeFacade PhoneTypeFacade
        {
            get { return new PhoneTypeFacade(); }
        }
        public IAddressTypeFacade AddressTypeFacade
        {
            get { return new AddressTypeFacade(); }
        }
        public IPersonPhoneFacade PersonPhoneFacade
        {
            get { return new PersonPhoneFacade(); }
        }
        public IPersonAddressFacade PersonAddressFacade
        {
            get { return new PersonAddressFacade(); }
        }
        public IOfficeFacade OfficeFacade
        {
            get { return new OfficeFacade(); }
        }
        public IDepartmentFacade DepartmentFacade
        {
            get { return new DepartmentFacade(); }
        }
    }
}
