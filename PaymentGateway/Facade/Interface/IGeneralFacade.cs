using System;
using Radyn.Payment.DataStructure;

namespace Radyn.PaymentGateway.Facade.Interface
{
    public interface IGeneralFacade
    {
        string OnlinePay(Guid tempId, Transaction transaction, string authority);
        string OnlinePay(Guid tempId, Transaction transaction, string authority, string merchantId, string terminalId, string userName, string password, string certificatePath, string certificatePassword, string merchantPublicKey, string merchantPrivateKey);
    }
}
