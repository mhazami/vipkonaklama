using System;
using Radyn.Payment.DataStructure;

namespace Radyn.PaymentGateway.Facade.Interface
{
    public interface IZarinPalFacade
    {
        Transaction ZarinPalCallBackPayRequest(Guid transId, string status, string authority);
    }
}
