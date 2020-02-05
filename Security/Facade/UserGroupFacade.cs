using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    internal sealed class UserGroupFacade : SecurityBaseFacade<UserGroup>, IUserGroupFacade
    {
        internal UserGroupFacade()  { }

        internal UserGroupFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

      
       
    }
}
