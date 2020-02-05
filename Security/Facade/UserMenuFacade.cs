using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    internal sealed class UserMenuFacade : SecurityBaseFacade<UserMenu>, IUserMenuFacade
    {
        internal UserMenuFacade() { }

        internal UserMenuFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

      
    }
}
