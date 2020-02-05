using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.DA
{
    public sealed class TariffDA : DALBase<Tariff>
    {
        public TariffDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class TariffCommandBuilder
    {
    }
}
