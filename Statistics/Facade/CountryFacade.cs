using Radyn.Framework.DbHelper;
using Radyn.Statistics.DataStructure;
using Radyn.Statistics.Facade.Interface;

namespace Radyn.Statistics.Facade
{
    internal sealed class CountryFacade : StatisticsBaseFacade<Country>, ICountryFacade
{
internal CountryFacade() { }

internal CountryFacade(IConnectionHandler connectionHandler) 
: base(connectionHandler){}

}
}
