using Radyn.Common.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.DA
{
    public sealed class CountryIPRangeDA : DALBase<CountryIPRange>
    {
        public CountryIPRangeDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public string GetIpCulture(string ipAddress)
        {
            var commandBuilder = new CountryIPRangeCommandBuilder();
            var query = commandBuilder.GetIpCulture(ipAddress);
            return DBManager.ExecuteScalar<string>(base.ConnectionHandler, query);
        }
    }
    internal class CountryIPRangeCommandBuilder
    {
        public string GetIpCulture(string ipAddress)
        {
            return
                string.Format(
                    "SELECT       top(1) Common.Country.LanguageId" +
                    " FROM            Common.CountryIPRange INNER JOIN " +
                    " Common.Country ON Common.CountryIPRange.CountryId = Common.Country.Id " +
                    " WHERE        (Common.CountryIPRange.EndRange >= '{0}') AND (Common.CountryIPRange.StartRange <= '{0}')",
                    ipAddress);
        }
    }
}
