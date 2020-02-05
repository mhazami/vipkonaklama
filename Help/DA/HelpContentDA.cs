using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Help.DataStructure;

namespace Radyn.Help.DA
{
    public sealed class HelpContentDA : DALBase<HelpContent>
    {
        public HelpContentDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class HelpContentCommandBuilder
    {
    }
}
