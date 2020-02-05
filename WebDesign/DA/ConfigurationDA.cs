using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.WebDesign.DataStructure;

namespace Radyn.WebDesign.DA
{
    public sealed class ConfigurationDA : DALBase<Configuration>
    {
        public ConfigurationDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class ConfigurationCommandBuilder
    {
    }
}
