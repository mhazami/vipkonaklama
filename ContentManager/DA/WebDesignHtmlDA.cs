using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.ContentManager.DataStructure;

namespace Radyn.ContentManager.DA
{
    public sealed class WebDesignHtmlDA : DALBase<WebDesignHtml>
    {
        public WebDesignHtmlDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class WebDesignHtmlCommandBuilder
    {
    }
}
