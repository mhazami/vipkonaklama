using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Help.DataStructure;

namespace Radyn.Help.DA
{
    public sealed class HelpReationCollectionDA : DALBase<HelpReationCollection>
    {
        public HelpReationCollectionDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class HelpReationCollectionCommandBuilder
    {
    }
}
