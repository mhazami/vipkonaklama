using Radyn.EnterpriseNode.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.EnterpriseNode.DAL
{
    public sealed class EnterpriseNodeTypeDA : DALBase<EnterpriseNodeType>
    {
        public EnterpriseNodeTypeDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class EnterpriseNodeTypeCommandBuilder
    {
    }
}
