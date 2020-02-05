using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;


namespace Radyn.ContentManager.DA
{
    public sealed class ContentContentDA : DALBase<ContentContent>
    {
        public ContentContentDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class ContentContentCommandBuilder
    {
    }
}
