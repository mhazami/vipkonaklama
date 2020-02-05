using System;
using Radyn.Payment.DataStructure;

namespace Radyn.PaymentGateway.Facade.Interface
{
    public interface IGhavaminFacade
    {
        Transaction GhavaminCallBackPayRequest(Guid Id,string resultCode, string SayanRef, string paymentID);
    }
}
