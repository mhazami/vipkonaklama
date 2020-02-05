using Radyn.Common.DataStructure;
using Radyn.Framework;

namespace Radyn.Common.Facade.Interface
{
public interface ICountryIPRangeFacade : IBaseFacade<CountryIPRange>
{
    bool ValidateIp(CountryIPRange ip);
    string GetByIpAddress(string ipAddress);
}
}
