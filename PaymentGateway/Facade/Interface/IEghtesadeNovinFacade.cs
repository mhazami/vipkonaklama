using System;
using Radyn.Payment.DataStructure;

namespace Radyn.PaymentGateway.Facade.Interface
{
    public interface IEghtesadeNovinFacade
    {
        Transaction EghtesadeNovinCallBackPayRequest(Guid id, string token, string MID, string resNum, string refNum, string state, string language, string cardPanHash);

    }
}
