using Radyn.Common.DataStructure;
using Radyn.Common.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.Facade
{
    internal sealed class ParishFacade : CommonBase<Parish>, IParishFacade
    {
        internal ParishFacade() { }

        internal ParishFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

      
    }
}
