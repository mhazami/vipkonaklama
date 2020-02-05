using Radyn.Common.DataStructure;
using Radyn.Common.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.Facade
{
    internal sealed class CurrencyFacade : CommonBase<Currency>, ICurrencyFacade
{
internal CurrencyFacade() { }

internal CurrencyFacade(IConnectionHandler connectionHandler) 
: base(connectionHandler){}

}
}
