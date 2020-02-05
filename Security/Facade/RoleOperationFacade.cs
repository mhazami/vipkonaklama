using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    internal sealed class RoleOperationFacade : SecurityBaseFacade<RoleOperation>, IRoleOperationFacade
    {
        internal RoleOperationFacade()  { }

        internal RoleOperationFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
    }
}
