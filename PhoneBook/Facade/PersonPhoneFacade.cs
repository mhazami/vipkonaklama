using Radyn.PhoneBook.DataStructure;
using Radyn.PhoneBook.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.PhoneBook.Facade
{
    internal sealed class PersonPhoneFacade : PhoneBookBaseFacade<PersonPhone>, IPersonPhoneFacade
    {
        internal PersonPhoneFacade() { }

        internal PersonPhoneFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

     
    }
}
