using Radyn.EnterpriseNode.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.EnterpriseNode.DAL
{
    public sealed class PrefixTitleDA : DALBase<PrefixTitle>
    {
        public PrefixTitleDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class PrefixTitleCommandBuilder
    {
    }
}
