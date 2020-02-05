using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.DataStructure;
using Radyn.PhoneBook.Facade.Interface;

namespace Radyn.PhoneBook.Facade
{
    internal sealed class DepartmentFacade : PhoneBookBaseFacade<Department>, IDepartmentFacade
    {
        internal DepartmentFacade() { }

        internal DepartmentFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}

