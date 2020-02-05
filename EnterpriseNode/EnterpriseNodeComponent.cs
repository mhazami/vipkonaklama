using Radyn.EnterpriseNode.Facade;
using Radyn.EnterpriseNode.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.EnterpriseNode
{
    public class EnterpriseNodeComponent
    {
        private EnterpriseNodeComponent()
        {

        }

        private static EnterpriseNodeComponent _instance;
        public static EnterpriseNodeComponent Instance
        {
            get { return _instance ?? (_instance = new EnterpriseNodeComponent()); }
        }

        public IEnterpriseNodeTypeFacade EnterpriseNodeTypeFacade
        {
            get
            {
                return new EnterpriseNodeTypeFacade();
            }
        }


        public IEnterpriseNodeFacade EnterpriseNodeFacade
        {
            get { return new EnterpriseNodeFacade(); }
        }
        public IEnterpriseNodeFacade EnterpriseNodeTransactionalFacade(IConnectionHandler connectionHandler)
        {
            return new EnterpriseNodeFacade(connectionHandler);
        }

        public ILegalEnterpriseNodeFacade LegalEnterpriseNodeFacade
        {
            get { return new LegalEnterpriseNodeFacade(); }
        }
        public ILegalEnterpriseNodeFacade LegalEnterpriseNodeTransactionalFacade(IConnectionHandler connectionHandler)
        {
          return new LegalEnterpriseNodeFacade(connectionHandler); 
        }
        public IRealEnterpriseNodeFacade RealEnterpriseNodeFacade
        {
            get { return new RealEnterpriseNodeFacade(); }
        }
        public IPrefixTitleFacade PrefixTitleFacade
        {
            get { return new PrefixTitleFacade(); }
        }
        public IRealEnterpriseNodeFacade RealEnterpriseNodeTransactionalFacade(IConnectionHandler connectionHandler)
        {
             return new RealEnterpriseNodeFacade(connectionHandler); 
        }
    }
}
