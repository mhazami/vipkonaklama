using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Facade.Interface;

namespace Radyn.Payment.Facade
{
    internal sealed class TempDiscountFacade : PaymentBaseFacade<TempDiscount>, ITempDiscountFacade
    {
        internal TempDiscountFacade() { }

        internal TempDiscountFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

     
    }
}
