using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.ContentManager.DataStructure;

namespace Radyn.ContentManager.DA
{
    public sealed class WebDesignMenuDA : DALBase<WebDesignMenu>
    {
        public WebDesignMenuDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class WebDesignMenuCommandBuilder
    {
    }
}
