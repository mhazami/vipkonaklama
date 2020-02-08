using Radyn.Common.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.DA
{
    public sealed class ParishDA : DALBase<Parish>
    {
        public ParishDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class ParishCommandBuilder
    {
    }
}
