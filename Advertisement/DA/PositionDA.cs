using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.DA
{
    public sealed class PositionDA : DALBase<Position>
    {
        public PositionDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class PositionCommandBuilder
    {
    }
}
