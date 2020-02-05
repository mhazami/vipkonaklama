using Radyn.Framework.DbHelper;
using Radyn.Statistics.DataStructure;
using Radyn.Statistics.Facade.Interface;

namespace Radyn.Statistics.Facade
{
    internal sealed class OSFacade : StatisticsBaseFacade<OS>, IOSFacade
{
internal OSFacade() { }

internal OSFacade(IConnectionHandler connectionHandler) 
: base(connectionHandler){}

}
}
