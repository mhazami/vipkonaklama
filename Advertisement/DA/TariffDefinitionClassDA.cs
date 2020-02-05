using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.DA
{
    public sealed class TariffDefinitionClassDA : DALBase<TariffDefinitionClass>
    {
        public TariffDefinitionClassDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class TariffDefinitionClassCommandBuilder
    {
    }
}
