using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;
using Radyn.Payment.BO;
using Radyn.Payment.Facade.Interface;
using WebDesignDiscountType = Radyn.Payment.DataStructure.WebDesignDiscountType;

namespace Radyn.Payment.Facade
{
    internal sealed class WebDesignDiscountTypeFacade : PaymentBaseFacade<WebDesignDiscountType>, IWebDesignDiscountTypeFacade
    {
        internal WebDesignDiscountTypeFacade()
        {
        }

        internal WebDesignDiscountTypeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }



        public IEnumerable<Payment.DataStructure.DiscountType> GetByWebId(Guid WebId)
        {
            try
            {
                var list = new List<Payment.DataStructure.DiscountType>();
                var congressAccounts = new WebDesignDiscountTypeBO().Where(this.ConnectionHandler,
                    x => x.WebId == WebId);
                foreach (var congressAccount in congressAccounts)
                {
                    if (congressAccount.WebSiteDiscountType == null) continue;
                    list.Add(congressAccount.WebSiteDiscountType);
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

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var DiscountTypeBo = new WebDesignDiscountTypeBO();
                var obj = DiscountTypeBo.Get(this.ConnectionHandler, keys);
                if (!DiscountTypeBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception(Resources.Payment.ErrorInDeleteDiscount);
                if (
                    !new DiscountTypeBO()
                        .Delete(ConnectionHandler,obj.DiscountTypeId))
                    throw new Exception(Resources.Payment.ErrorInDeleteDiscount);
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

        public bool Insert(Guid WebId, Payment.DataStructure.DiscountType discountType, List<DiscountTypeSection> sectiontypes,
            List<DiscountTypeAutoCode> discountTypeAutoCodes)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
               discountType.IsExternal = true;
                if (!new DiscountTypeBO().Insert(ConnectionHandler,discountType, sectiontypes, discountTypeAutoCodes))
                    throw new Exception("خطایی در ذخیره حساب  وجود دارد");

                var DiscountType = new WebDesignDiscountType
                {
                    DiscountTypeId = discountType.Id,
                    WebId = WebId
                };
                if (!new WebDesignDiscountTypeBO().Insert(this.ConnectionHandler, DiscountType))
                    throw new Exception(Resources.Payment.ErrorInDeleteDiscount);
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
        public Guid UpdateStatusAfterTransactionGroupTemp(Guid userId, Guid id)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var tempTransactionalFacade = new TempBO();
                var tr = tempTransactionalFacade.RemoveTempAndReturnTransaction(ConnectionHandler,id);
                if (tr == null) return Guid.Empty;
                var byFilter = tempTransactionalFacade.Where(ConnectionHandler,x => x.ParentId == id);
                if (byFilter.Any())
                {
                    foreach (var temp in byFilter)
                    {

                        tempTransactionalFacade.RemoveTempAndReturnTransaction(ConnectionHandler,temp.Id);
                    }
                }
                this.ConnectionHandler.CommitTransaction();
              this.FileManagerConnection.CommitTransaction();
                return tr.Id;
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
