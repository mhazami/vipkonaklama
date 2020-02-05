using Radyn.Common.DataStructure;
using Radyn.Common.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.Facade
{
    internal sealed class BankFacade : CommonBase<Bank>, IBankFacade
    {
        internal BankFacade()
        {
        }

        internal BankFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

    }
}
