using Radyn.Payment.DataStructure;

namespace Radyn.PaymentGateway.Facade.Interface
{
    public interface IPasargadFacade
    {
        bool PasargadCallBackPayRequest(Transaction transaction);
      

    }
}
