using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.DA
{
    public sealed class WebDesignPagesDA : DALBase<WebDesignPages>
    {
        public WebDesignPagesDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        {
        }
    }
}
