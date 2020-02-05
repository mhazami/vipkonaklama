using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    internal sealed class UserOperationFacade : SecurityBaseFacade<UserOperation>, IUserOperationFacade
    {
        internal UserOperationFacade()  { }

        internal UserOperationFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
      
        
    }
}
