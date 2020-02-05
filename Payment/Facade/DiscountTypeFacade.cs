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
    internal sealed class DiscountTypeFacade : PaymentBaseFacade<DiscountType>, IDiscountTypeFacade
    {
        internal DiscountTypeFacade() { }

        internal DiscountTypeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        

        public bool Update(DiscountType discountType, List<DiscountTypeAutoCode> discountTypeAutoCodeFacades)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new DiscountTypeBO().Update(this.ConnectionHandler, discountType, discountTypeAutoCodeFacades))
                    throw new Exception("خطایی در ذخیره نوع تخفیف وجود دارد");
                this.ConnectionHandler.CommitTransaction();

                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();

                Log.Save(knownException.Message, LogType.ApplicationError);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Update(DiscountType discountType, string modelname, List<DiscountTypeSection> sectiontypes, List<DiscountTypeAutoCode> discountTypeAutoCodes)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new DiscountTypeBO().Update(this.ConnectionHandler, discountType,modelname, sectiontypes, discountTypeAutoCodes))
                    throw new Exception("خطایی در ذخیره نوع تخفیف وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(knownException.Message, LogType.ApplicationError);
                throw new KnownException(knownException.Message, knownException);
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
                new DiscountTypeBO().Delete(this.ConnectionHandler, keys);
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(knownException.Message, LogType.ApplicationError);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
        public bool Insert(DiscountType discountType, List<DiscountTypeAutoCode> discountTypeAutoCodeFacades)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new DiscountTypeBO().Insert(this.ConnectionHandler, discountType, discountTypeAutoCodeFacades))
                    throw new Exception("خطایی در ذخیره نوع تخفیف وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(knownException.Message, LogType.ApplicationError);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
        public bool Insert(DiscountType discountType, List<DiscountTypeSection> sectiontypes, List<DiscountTypeAutoCode> discountTypeAutoCodes)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new DiscountTypeBO().Insert(this.ConnectionHandler, discountType, sectiontypes, discountTypeAutoCodes))
                    throw new Exception("خطایی در ذخیره نوع تخفیف وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(knownException.Message, LogType.ApplicationError);
                throw new KnownException(knownException.Message, knownException);
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
