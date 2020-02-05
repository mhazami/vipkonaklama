using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.PhoneBook.DataStructure;


namespace Radyn.PhoneBook.DA
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
