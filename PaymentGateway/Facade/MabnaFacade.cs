using System;
using Radyn.Framework;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.BO;
using Radyn.PaymentGateway.Facade.Interface;

namespace Radyn.PaymentGateway.Facade
{
    public class MabnaFacade : IMabnaFacade
    {
        public Transaction MabnaCallBackPayRequest(Guid Id, string resCode, string trn, string crn, int amount)
        {
            try
            {
                return new MabnaBO().MabnaCallBackPayRequest(Id, resCode, trn, crn, amount);
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
