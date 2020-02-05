using Radyn.Common.DataStructure;
using Radyn.Common.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.Facade
{
    internal sealed class UnitFacade : CommonBase<Unit>, IUnitFacade
    {
        internal UnitFacade() { }

        internal UnitFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
