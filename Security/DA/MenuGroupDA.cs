using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;

namespace Radyn.Security.DA
{
    public sealed class MenuGroupDA : DALBase<MenuGroup>
    {
        public MenuGroupDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class MenuGroupCommandBuilder
    {
    }
}
