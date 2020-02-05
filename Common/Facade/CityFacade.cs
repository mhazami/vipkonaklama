using Radyn.Common.DataStructure;
using Radyn.Common.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.Facade
{
    internal sealed class CityFacade : CommonBase<City>, ICityFacade
    {
        internal CityFacade() { }

        internal CityFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

      
    }
}
