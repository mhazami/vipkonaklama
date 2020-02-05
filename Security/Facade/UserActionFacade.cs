using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    internal sealed class UserActionFacade : SecurityBaseFacade<UserAction>, IUserActionFacade
    {
        internal UserActionFacade()  { }

        internal UserActionFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
      

    }
}
