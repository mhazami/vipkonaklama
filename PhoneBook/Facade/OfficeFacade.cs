using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.DataStructure;
using Radyn.PhoneBook.Facade.Interface;

namespace Radyn.PhoneBook.Facade
{
    internal sealed class OfficeFacade : PhoneBookBaseFacade<Office>, IOfficeFacade
    {
        internal OfficeFacade() { }

        internal OfficeFacade(IConnectionHandler connectionHandler) 
            : base(connectionHandler){}

    }
}

