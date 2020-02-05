using System;
using System.Data;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.BO;
using Radyn.Payment.Facade.Interface;


namespace Radyn.Payment.Facade
{
    internal sealed class WebDesignAccountFacade : PaymentBaseFacade<DataStructure.WebDesignAccount>, IWebDesignAccountFacade
    {
        internal WebDesignAccountFacade()
        {
        }

        internal WebDesignAccountFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }





        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var congressAccountBo = new WebDesignAccountBO();
                var obj = congressAccountBo.Get(this.ConnectionHandler, keys);
                if (!congressAccountBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطا در حذف اطلاعات");
                if (
                    !new AccountBO()
                        .Delete(ConnectionHandler,obj.AccountId))
                    throw new Exception("خطا در حذف اطلاعات");
                this.ConnectionHandler.CommitTransaction();
               return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Insert(Guid congressId, Payment.DataStructure.Account account)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                
                account.IsExternal = true;
                if (!new AccountBO().Insert(ConnectionHandler,account))
                    throw new Exception("خطایی در ذخیره حساب  وجود دارد");
                var congressAccount = new DataStructure.WebDesignAccount() { AccountId = account.Id, WebId = congressId };
                if (!new WebDesignAccountBO().Insert(this.ConnectionHandler, congressAccount))
                    throw new Exception(Resources.Payment.ErrorInSaveDiscountCode);
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                 Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
