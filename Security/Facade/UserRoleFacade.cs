using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    internal sealed class UserRoleFacade : SecurityBaseFacade<UserRole>, IUserRoleFacade
    {
        internal UserRoleFacade()  { }

        internal UserRoleFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        
    }
}
