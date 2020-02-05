using System;
using Radyn.Framework;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.BO;
using Radyn.PaymentGateway.Facade.Interface;

namespace Radyn.PaymentGateway.Facade
{
    public class MellatFacade : IMallatFacade
    {

        public Transaction MellatCallBackPayRequest(Guid id, string recCode, string refId, long oredrId, long saleOrderId, long saleReferenceId)
        {
            try
            {

                return new MellatBO().MellatCallBackPayRequest(id, recCode, refId, oredrId, saleOrderId, saleReferenceId);
            }
            catch (KnownException knownException)
            {
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source, knownException.StackTrace);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

    }
}
