using Radyn.PhoneBook.DataStructure;
using Radyn.PhoneBook.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.PhoneBook.Facade
{
    internal sealed class PhoneTypeFacade : PhoneBookBaseFacade<PhoneType>, IPhoneTypeFacade
    {
        internal PhoneTypeFacade() { }

        internal PhoneTypeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
