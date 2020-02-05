using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.DA
{
    public sealed class HistoryDA : DALBase<History>
    {
        public HistoryDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class AdvertisementHistoryCommandBuilder
    {
    }
}
