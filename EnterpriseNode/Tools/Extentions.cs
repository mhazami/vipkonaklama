using Radyn.EnterpriseNode.Facade;

namespace Radyn.EnterpriseNode.Tools
{
    public static class Extentions
    {
        public static string Title(this DataStructure.EnterpriseNode enterprise)
        {
            var result = string.Empty;
            if (enterprise==null)
            {
                return result;
            }
            if (enterprise.PrefixTitleId.HasValue)
            {
                if (enterprise.PrefixTitle == null)
                    enterprise.PrefixTitle = new PrefixTitleFacade().Get(enterprise.PrefixTitleId);
                result += enterprise.PrefixTitle.Title+" ";
            }
            switch (enterprise.EnterpriseNodeTypeId)
            {
                case Enums.EnterpriseNodeType.RealEnterPriseNode:
                    if (enterprise.RealEnterpriseNode == null)
                        enterprise.RealEnterpriseNode = new RealEnterpriseNodeFacade().Get(enterprise.Id);
                    if(enterprise.RealEnterpriseNode!=null)
                    result+= enterprise.RealEnterpriseNode.FirstName + " " + enterprise.RealEnterpriseNode.LastName;
                    break;
                case Enums.EnterpriseNodeType.LegalEnterPriseNode:
                    if (enterprise.LegalEnterpriseNode == null)
                        enterprise.LegalEnterpriseNode = new LegalEnterpriseNodeFacade().Get(enterprise.Id);
                    if (enterprise.LegalEnterpriseNode != null)
                        result+= enterprise.LegalEnterpriseNode.Title;
                    break;

            }
            return result;
        }
    }
}
