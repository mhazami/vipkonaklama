using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.DA
{
    public sealed class TariffClassHistoryDA : DALBase<TariffClassHistory>
    {
        public TariffClassHistoryDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class TariffClassHistoryCommandBuilder
    {
    }
}
