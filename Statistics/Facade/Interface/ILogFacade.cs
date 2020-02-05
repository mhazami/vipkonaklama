using Radyn.Framework;
using Radyn.Statistics.Tools;

namespace Radyn.Statistics.Facade.Interface
{
    public interface ILogFacade : IBaseFacade<DataStructure.Log>
    {
        ModelView.StatisticsModel GetStatisticsModel(string url);
        bool InserNewLog(DataStructure.Log log);
    }
}
