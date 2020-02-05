using Radyn.Framework.DbHelper;
using Radyn.Statistics.DataStructure;
using Radyn.Statistics.Facade.Interface;

namespace Radyn.Statistics.Facade
{
    internal sealed class BrowserFacade : StatisticsBaseFacade<Browser>, IBrowserFacade
{
internal BrowserFacade() { }

internal BrowserFacade(IConnectionHandler connectionHandler) 
: base(connectionHandler){}

}
}
