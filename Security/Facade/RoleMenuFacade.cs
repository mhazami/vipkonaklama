using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    internal sealed class RoleMenuFacade : SecurityBaseFacade<RoleMenu>, IRoleMenuFacade
    {
        internal RoleMenuFacade()  { }

        internal RoleMenuFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

      
    }
}
