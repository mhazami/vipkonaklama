using System;
using Radyn.Payment.DataStructure;

namespace Radyn.PaymentGateway.Facade.Interface
{
    public interface IIranKishFacade
    {
        Transaction IranKishCallBackPayRequest(Guid Id, string privateKey, string status, string refNum);

    }
}
