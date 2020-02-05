using System;
using Radyn.Payment.DataStructure;

namespace Radyn.PaymentGateway.Facade.Interface
{
   public interface IMallatFacade
    {
       Transaction MellatCallBackPayRequest(Guid id, string recCode, string refId, long oredrId, long saleOrderId, long saleReferenceId);

    }
}
