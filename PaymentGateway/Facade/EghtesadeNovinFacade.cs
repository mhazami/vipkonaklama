using System;
using Radyn.Framework;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.BO;
using Radyn.PaymentGateway.Facade.Interface;

namespace Radyn.PaymentGateway.Facade
{
    public class EghtesadeNovinFacade : IEghtesadeNovinFacade
    {
        public Transaction EghtesadeNovinCallBackPayRequest(Guid id, string token, string MID, string resNum, string refNum, string state, string language, string cardPanHash)
        {
            try
            {
                return new EghtesadeNovinBO().EghtesadeNovinCallBackPayRequest(id, token, MID, resNum, refNum, state, language, cardPanHash);
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
