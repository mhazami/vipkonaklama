using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    internal sealed class GroupRoleFacade : SecurityBaseFacade<GroupRole>, IGroupRoleFacade
    {
        internal GroupRoleFacade()  { }

        internal GroupRoleFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        
    }
}
