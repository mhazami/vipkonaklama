using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Statistics.DataStructure;

namespace Radyn.Statistics.DA
{
    public sealed class WebSiteDA : DALBase<WebSite>
    {
        public WebSiteDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

       
    }
    internal class WebSiteCommandBuilder
    {
        
    }
}
