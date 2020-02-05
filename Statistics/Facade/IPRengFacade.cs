using Radyn.Framework.DbHelper;
using Radyn.Statistics.DataStructure;
using Radyn.Statistics.Facade.Interface;

namespace Radyn.Statistics.Facade
{
    internal sealed class IPRengFacade : StatisticsBaseFacade<IPReng>, IIPRengFacade
    {
        internal IPRengFacade() { }

        internal IPRengFacade(IConnectionHandler connectionHandler)
        : base(connectionHandler)
        { }

       
    }
}
