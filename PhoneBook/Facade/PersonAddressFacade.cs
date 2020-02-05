using Radyn.PhoneBook.DataStructure;
using Radyn.PhoneBook.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.PhoneBook.Facade
{
    internal sealed class PersonAddressFacade : PhoneBookBaseFacade<PersonAddress>, IPersonAddressFacade
    {
        internal PersonAddressFacade() { }

        internal PersonAddressFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

      
    }
}
