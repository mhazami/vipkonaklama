using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.ContentManager.DataStructure;

namespace Radyn.ContentManager.DA
{
    public sealed class WebDesignContentDA : DALBase<WebDesignContent>
    {
        public WebDesignContentDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class WebDesignContentCommandBuilder
    {
    }
}
