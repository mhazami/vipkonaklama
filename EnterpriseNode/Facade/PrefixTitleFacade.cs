using Radyn.EnterpriseNode.DataStructure;
using Radyn.EnterpriseNode.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.EnterpriseNode.Facade
{
    internal sealed class PrefixTitleFacade : EnterpriseNodeBaseFacade<PrefixTitle>, IPrefixTitleFacade
    {
        internal PrefixTitleFacade()
        {
        }

        internal PrefixTitleFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

     

       
    }
}
