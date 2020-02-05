using System;
using System.Collections.Generic;
using System.Data;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.BO;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Facade.Interface;

namespace Radyn.Payment.Facade
{
    internal sealed class TempFacade : PaymentBaseFacade<Temp>, ITempFacade
    {
        internal TempFacade() { }

        internal TempFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        public Transaction RemoveTempAndReturnTransaction(Guid tempId)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var tempBo = new TempBO();
                var removeTempAndReturnTransaction = tempBo.RemoveTempAndReturnTransaction(this.ConnectionHandler, tempId);
                this.ConnectionHandler.CommitTransaction();
                return removeTempAndReturnTransaction;
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
                Log.Save(ex.Message, LogType.ApplicationError,ex.Source,ex.StackTrace); 
                throw new KnownException(ex.Message, ex);
            }
        }
        public Transaction RemoveTempAndReturnTransactionGroup(Guid tempId)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var tempBo = new TempBO();
                var removeTempAndReturnTransaction = tempBo.RemoveTempAndReturnTransaction(this.ConnectionHandler, tempId);
                var list = tempBo.Where(this.ConnectionHandler, x => x.ParentId == tempId);
                foreach (var temp in list)
                {
                    tempBo.RemoveTempAndReturnTransaction(this.ConnectionHandler, temp.Id);
                }
                this.ConnectionHandler.CommitTransaction();
                return removeTempAndReturnTransaction;
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
        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var tempBo = new TempBO();
                if (!tempBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception(Resources.Payment.ErrorInDeleteTemp);
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

        public bool Update(Temp temp, List<DiscountType> discountAttaches)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var tempBo = new TempBO();
                if (!tempBo.Update(this.ConnectionHandler, temp))
                    throw new Exception(Resources.Payment.ErrorInSaveTransaction);
                if (!new TempDiscountBO().ModifyDiscount(this.ConnectionHandler, this.FileManagerConnection, discountAttaches, temp.Id))
                    throw new Exception(Resources.Payment.ErrorInSaveTransaction);
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace); 
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace); 
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool GroupPayTemp(Temp temp, List<Guid> model)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new TempBO().GroupPayTemp(this.ConnectionHandler, temp, model))
                    throw new Exception(Resources.Payment.ErrorInSaveTransaction);
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


        public bool Insert(Temp temp, List<DiscountType> transactionDiscountAttaches)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new TempBO().Insert(this.ConnectionHandler, temp))
                    throw new Exception(Resources.Payment.ErrorInSaveTransaction);
                if (!new TempDiscountBO().ModifyDiscount(this.ConnectionHandler, this.FileManagerConnection, transactionDiscountAttaches, temp.Id))
                    throw new Exception(Resources.Payment.ErrorInSaveTransaction);
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace); 
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace); 
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
