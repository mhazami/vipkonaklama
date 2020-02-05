using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.Facade
{
    internal sealed class RolePartialsFacade : ContentManagerBaseFacade<RolePartials>, IRolePartialsFacade
    {
        internal RolePartialsFacade()  { }

        internal RolePartialsFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        
    }
}
