using System;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.BO;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Facade.Interface;

namespace Radyn.Payment.Facade
{
    internal sealed class AccountFacade : PaymentBaseFacade<Account>, IAccountFacade
    {
        internal AccountFacade()  { }

        internal AccountFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        

        public override bool Delete(params object[] keys)
        {
            try
            {
                var accountBo = new AccountBO();
                var obj = accountBo.Get(this.ConnectionHandler, keys);
                var byFilter = new TransactionBO().Any(this.ConnectionHandler, x => x.AccountId == obj.Id);
                if(byFilter)
                    throw new Exception("این شماره حساب قابل حذف نیست زیرا دارای تراکنش مالی میباشد");
                return accountBo.Delete(this.ConnectionHandler, keys);
            }
            catch (KnownException knownException)
            {
              
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
              
                 Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);  throw new KnownException(ex.Message, ex);
            }
        }
    }
}
