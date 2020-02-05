using System;
using Radyn.Payment.DataStructure;

namespace Radyn.PaymentGateway.Facade.Interface
{
    public interface ISaderatFacade
    {
        Transaction SaderatCallBackPayRequest(Guid Id, string orederId, string resultCode, string referenceId);

    }
}
