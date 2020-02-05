using System;
using Radyn.Payment.DataStructure;

namespace Radyn.PaymentGateway.Facade.Interface
{
    public interface ISamanFacade
    {
        Transaction SamanCallBackPayRequest(Guid Id, string orederId, string status, string refNum,  string traceNo);

    }
}
