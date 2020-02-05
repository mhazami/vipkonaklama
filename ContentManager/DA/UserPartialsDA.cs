using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;


namespace Radyn.ContentManager.DA
{
    public sealed class UserPartialsDA : DALBase<UserPartials>
    {
        public UserPartialsDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class UserPartialsCommandBuilder
    {
    }
}
