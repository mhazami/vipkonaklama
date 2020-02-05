using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FAQ.DA
{
    public sealed class WebDesignFAQDA : DALBase<DataStructure.WebDesignFAQ>
    {
        public WebDesignFAQDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class WebDesignFAQCommandBuilder
    {
    }
}
