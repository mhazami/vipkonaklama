using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Common.DataStructure;

namespace Radyn.Common.DA
{
    public sealed class WebDesignLanguageDA : DALBase<WebDesignLanguage>
    {
        public WebDesignLanguageDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class WebDesignLanguageCommandBuilder
    {
    }
}
