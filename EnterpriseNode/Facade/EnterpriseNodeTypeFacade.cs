using Radyn.EnterpriseNode.DataStructure;
using Radyn.EnterpriseNode.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.EnterpriseNode.Facade
{
    public class EnterpriseNodeTypeFacade : EnterpriseNodeBaseFacade<EnterpriseNodeType>, IEnterpriseNodeTypeFacade
    {

        internal EnterpriseNodeTypeFacade()
        {
        }

        internal EnterpriseNodeTypeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
