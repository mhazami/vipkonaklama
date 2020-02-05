using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.News.DA
{
    public sealed class WebDesignNewsDA : DALBase<DataStructure.WebDesignNews>
    {
        public WebDesignNewsDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class WebDesignNewsCommandBuilder
    {
    }
}
