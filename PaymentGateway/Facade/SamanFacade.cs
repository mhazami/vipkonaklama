using System;
using Radyn.Framework;
using Radyn.Payment.DataStructure;
using Radyn.PaymentGateway.BO;
using Radyn.PaymentGateway.Facade.Interface;

namespace Radyn.PaymentGateway.Facade
{
    public class SamanFacade : ISamanFacade
    {


        public Transaction SamanCallBackPayRequest(Guid Id, string orederId, string status, string refNum,  string traceNo)
        {
            try
            {

                return new SamanBO().SamanCallBackPayRequest(Id, orederId, status, refNum, traceNo);
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
