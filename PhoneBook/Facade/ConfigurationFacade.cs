using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.DataStructure;
using Radyn.PhoneBook.Facade.Interface;

namespace Radyn.PhoneBook.Facade
{
    internal sealed class ConfigurationFacade : PhoneBookBaseFacade<Configuration>, IConfigurationFacade
    {
        internal ConfigurationFacade() { }

        internal ConfigurationFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

      


    }
}
