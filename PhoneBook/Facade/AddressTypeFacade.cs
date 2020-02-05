using Radyn.PhoneBook.DataStructure;
using Radyn.PhoneBook.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.PhoneBook.Facade
{
    internal sealed class AddressTypeFacade : PhoneBookBaseFacade<AddressType>, IAddressTypeFacade
    {
        internal AddressTypeFacade() { }

        internal AddressTypeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
