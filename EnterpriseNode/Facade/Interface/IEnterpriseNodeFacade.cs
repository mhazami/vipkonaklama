using System.Collections.Generic;
using System.Web;
using Radyn.EnterpriseNode.Tools;
using Radyn.Framework;

namespace Radyn.EnterpriseNode.Facade.Interface
{
    public interface IEnterpriseNodeFacade : IBaseFacade<DataStructure.EnterpriseNode>
    {
        List<DataStructure.EnterpriseNode> Search(DataStructure.EnterpriseNode filter, bool fillcomplex = true);
        List<DataStructure.EnterpriseNode> Search(string filter, Enums.EnterpriseNodeType enterpriseNodeTypeId = Enums.EnterpriseNodeType.RealEnterPriseNode, bool fillcomplex = true);

        bool Insert(DataStructure.EnterpriseNode obj, HttpPostedFileBase file);
        bool Update(DataStructure.EnterpriseNode obj, HttpPostedFileBase file);

     
       
    
    }
}
