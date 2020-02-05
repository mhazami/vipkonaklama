using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.ContentManager.DataStructure;

namespace Radyn.ContentManager.DA
{
    public sealed class WebDesignContainerDA : DALBase<WebDesignContainer>
    {
        public WebDesignContainerDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class WebDesignContainerCommandBuilder
    {
    }
}
