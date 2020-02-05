using System.Collections.Generic;
using System.Web;
using Radyn.Common.DataStructure;
using Radyn.Framework;

namespace Radyn.Common.Facade.Interface
{
public interface ICountryFacade : IBaseFacade<Country>
{
    bool Insert(Country country, List<CountryIPRange> list, HttpPostedFileBase file);
    bool Update(Country country, List<CountryIPRange> list, HttpPostedFileBase file);
}
}
