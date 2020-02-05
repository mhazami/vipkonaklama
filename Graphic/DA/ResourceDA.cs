using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Graphic.DataStructure;

namespace Radyn.Graphic.DA
{
    public sealed class ResourceDA : DALBase<Resource>
    {
        public ResourceDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class ResourceCommandBuilder
    {
    }
}
