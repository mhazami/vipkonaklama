using Radyn.EnterpriseNode.DataStructure;
using Radyn.EnterpriseNode.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.EnterpriseNode.Facade
{
    public class LegalEnterpriseNodeFacade : EnterpriseNodeBaseFacade<LegalEnterpriseNode>, ILegalEnterpriseNodeFacade
    {
        internal LegalEnterpriseNodeFacade()
        {
        }

        internal LegalEnterpriseNodeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       


      
    }
}
