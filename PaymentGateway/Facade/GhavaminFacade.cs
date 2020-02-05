using System;
using Radyn.Framework;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.BO;
using Radyn.PaymentGateway.Facade.Interface;

namespace Radyn.PaymentGateway.Facade
{
    public class GhavaminFacade : IGhavaminFacade
    {
        
        public Transaction GhavaminCallBackPayRequest(Guid Id, string resultCode, string SayanRef, string paymentID)
        {
            try
            {
                return new GhavaminBO().GhavaminCallBackPayRequest(Id, resultCode, SayanRef, paymentID);
            }
            catch (KnownException knownException)
            {
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source,
                    knownException.StackTrace);
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
