using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;


namespace Radyn.ContentManager.DA
{
    public sealed class RolePartialsDA : DALBase<RolePartials>
    {
        public RolePartialsDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class RolePartialsCommandBuilder
    {
    }
}
