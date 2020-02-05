using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.FileManager;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.BO;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Facade.Interface;
using Radyn.Payment.Tools;
using Radyn.Utility;

namespace Radyn.Payment.Facade
{
    internal sealed class TransactionFacade : PaymentBaseFacade<Transaction>, ITransactionFacade
    {
        internal TransactionFacade() { }

        internal TransactionFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
        public Transaction DocumnetPay(Guid tempId, Transaction transaction, HttpPostedFileBase fileBase)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (fileBase == null) throw new Exception(Resources.Payment.PleaseInsertDocumentScanFile);
                var fileTransactionalFacade = FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection);
                if (transaction.DocScan.HasValue) fileTransactionalFacade.Update(fileBase, transaction.DocScan.Value, new File() { MaxSize = 200 });
                else
                    transaction.DocScan = fileTransactionalFacade.Insert(fileBase, new File() { MaxSize = 200 });
                var tempAndAddTransaction = new TransactionBO().UpdateTempAndAddTransaction(this.ConnectionHandler, tempId, transaction, (byte)Enums.PayType.Documnet);
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return tempAndAddTransaction;
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

        public Transaction UpdateTempAndAddTransaction(Guid tempId, Transaction transaction, byte payTypeId)
        {
            try
            {
                return new TransactionBO().UpdateTempAndAddTransaction(this.ConnectionHandler, tempId, transaction, payTypeId);
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public Transaction AfterGetway(Guid transactionId, string refId, string reccode, string salerefrence)
        {
            try
            {
                var transactionBo = new TransactionBO();
                var transaction = transactionBo.Get(this.ConnectionHandler, transactionId);
                if (reccode == "0")
                    transaction.Done = true;
                transaction.RefId = refId;
                if (!string.IsNullOrEmpty(salerefrence))
                    transaction.SaleReferenceId = salerefrence.ToLong();
                if (!transactionBo.Update(this.ConnectionHandler, transaction))
                    throw new Exception(Resources.Payment.ErrorInSaveTransaction);
                return transaction;
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }



        public bool Done(Guid transactionId)
        {
            try
            {
                var transactionBo = new TransactionBO();
                var transaction = transactionBo.Get(this.ConnectionHandler, transactionId);
                transaction.Done = true;
                if (!transactionBo.Update(this.ConnectionHandler, transaction))
                    throw new Exception(Resources.Payment.ErrorInSaveTransaction);
                return true;
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public List<Transaction> GetUserTransactions(Guid userId)
        {
            try
            {
                var list = new List<Transaction>();
                var tempBo = new TempBO();
                var temps = tempBo.GetByUserId(this.ConnectionHandler, userId);
                var transactions = new TransactionBO().OrderByDescending(this.ConnectionHandler,x=>x.PayDate,x=>x.PayerId== userId);
                foreach (var temp in temps)
                {
                    var transactionModel = new Transaction()
                    {
                        Id = temp.Id,
                        Amount = temp.Amount,
                        PayDate = temp.Date,
                        Description = temp.Description,
                        IsTemp = true
                    };
                    list.Add(transactionModel);
                }
                foreach (var transaction in transactions)
                {
                    list.Add(transaction);
                }
                return list;
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
        
        public int GetUserTempCount(Guid userId)
        {
            try
            {
                return new TempBO().GetUserTempCount(this.ConnectionHandler, userId);
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        


    }
}
