using Radyn.Framework.DbHelper;
using Radyn.Statistics.DataStructure;
using Radyn.Statistics.Facade.Interface;

namespace Radyn.Statistics.Facade
{
    internal sealed class AccountsFacade : StatisticsBaseFacade<Accounts>, IAccountsFacade
    {
        internal AccountsFacade() { }

        internal AccountsFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
