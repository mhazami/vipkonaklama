using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.Facade
{
    internal sealed class UserPartialsFacade : ContentManagerBaseFacade<UserPartials>, IUserPartialsFacade
    {
        internal UserPartialsFacade()  { }

        internal UserPartialsFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
