using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.WebDesign.DataStructure;

namespace Radyn.WebDesign.DA
{
    public sealed class WebSiteAliasDA : DALBase<WebSiteAlias>
    {
        public WebSiteAliasDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class WebSiteAliasCommandBuilder
    {
    }
}
