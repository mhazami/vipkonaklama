using Radyn.Payment.DataStructure;

namespace Radyn.PaymentGateway.Facade.Interface
{
    public interface IPayPalFacade
    {
        bool PayPalCallBackPayRequest(Transaction transaction);
      

    }
}
