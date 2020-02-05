using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Security.DA
{
    public sealed class TrackerDA : DALBase<Tracker>
    {
        public TrackerDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        {
        }
    }
    internal class TrackerCommandBuilder
    {
    }
}
