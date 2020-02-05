using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.DA
{
    public sealed class TariffClassDA : DALBase<TariffClass>
    {
        public TariffClassDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class TariffClassCommandBuilder
    {
    }
}
