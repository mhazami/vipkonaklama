using Radyn.Framework.DbHelper;
using Radyn.Statistics.DataStructure;
using Radyn.Statistics.Facade.Interface;

namespace Radyn.Statistics.Facade
{
    internal sealed class SearchEngineFacade : StatisticsBaseFacade<SearchEngine>, ISearchEngineFacade
    {
        internal SearchEngineFacade() { }

        internal SearchEngineFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
