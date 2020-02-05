using Radyn.Framework.DbHelper;
using Radyn.Statistics.DataStructure;
using Radyn.Statistics.Facade.Interface;

namespace Radyn.Statistics.Facade
{
    internal sealed class LogItemsFacade : StatisticsBaseFacade<LogItems>, ILogItemsFacade
    {
        internal LogItemsFacade() { }

        internal LogItemsFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        
    }
}
