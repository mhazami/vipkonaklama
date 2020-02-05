using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.WebDesign.DataStructure;

namespace Radyn.WebDesign.DA
{
    public sealed class ResourceDA : DALBase<Resource>
    {
        public ResourceDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class ResourceCommandBuilder
    {
    }
}
