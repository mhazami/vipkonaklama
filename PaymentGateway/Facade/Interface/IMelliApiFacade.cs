using System;
using Radyn.Payment.DataStructure;

namespace Radyn.PaymentGateway.Facade.Interface
{
    public interface IMelliApiFacade
    {
        Transaction MelliCallBackPayRequest(Guid Id,string token);
    }
}
