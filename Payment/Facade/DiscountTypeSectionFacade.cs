using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.BO;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Facade.Interface;
using Radyn.Utility;

namespace Radyn.Payment.Facade
{
    internal sealed class DiscountTypeSectionFacade : PaymentBaseFacade<DiscountTypeSection>, IDiscountTypeSectionFacade
    {
        internal DiscountTypeSectionFacade() { }

        internal DiscountTypeSectionFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }


        public List<TempDiscount> GetDiscountTypes(string modualName, byte section, Guid? tempId = null)
        {

            try
            {
                var outlist = new List<TempDiscount>();
                if (tempId.HasValue)
                {
                    var tr = new TempBO().Get(this.ConnectionHandler, tempId);
                    if (tr.TransactionId.HasValue) return outlist;
                }
                var list = new DiscountTypeSectionBO().GetDiscountTypes(this.ConnectionHandler, modualName, section);
                var transactionDiscountBo = new TempDiscountBO();
                var discountTypeAutoCodeBo = new DiscountTypeAutoCodeBO();
                var discountTypeBo = new DiscountTypeBO();
                if (!list.Any()) return outlist;
                var enumerable = list.Select(x => x.Id);
                var discountTypes = discountTypeBo.Where(ConnectionHandler, x => x.Id.In(enumerable));
                var tempDiscounts = transactionDiscountBo.Where(ConnectionHandler, x => x.DiscountTypeId.In(enumerable));
                var discountTypeAutoCodes = discountTypeAutoCodeBo.Where(ConnectionHandler, x => x.DiscountTypeId.In(enumerable));
               
                foreach (var discountType in list)
                {
                    var getdiscountType = discountTypes.FirstOrDefault(x=>x.Id== discountType.Id);
                    if (getdiscountType == null) continue;
                    if (((!string.IsNullOrEmpty(getdiscountType.EndDate.Trim()) && getdiscountType.EndDate.CompareTo(DateTime.Now.ShamsiDate()) < 0)) ||
                        ((!string.IsNullOrEmpty(getdiscountType.StartDate.Trim()) && getdiscountType.StartDate.CompareTo(DateTime.Now.ShamsiDate()) > 0)) ||
                        string.IsNullOrEmpty(getdiscountType.Title) || !getdiscountType.Enabled) continue;
                    TempDiscount tempDiscount;
                    if (tempId.HasValue)
                    {
                        var discount = tempDiscounts.FirstOrDefault(x=>x.TempId==tempId&&x.DiscountTypeId==discountType.Id);
                        if (discount != null)
                        {
                            tempDiscount = discount;
                            tempDiscount.Added = true;
                        }
                        else tempDiscount = new TempDiscount() { DiscountTypeId = discountType.Id };
                    }
                    else
                        tempDiscount = new TempDiscount() { DiscountTypeId = discountType.Id };
                    if (getdiscountType.ForceCode)
                    {
                        if (getdiscountType.IsAutoCode)
                        {
                            var byFilter = discountTypeAutoCodes.Any(x =>
                               x.DiscountTypeId == getdiscountType.Id &&
                                x.Used == false);
                            if (!byFilter) continue;
                        }
                        else if (getdiscountType.RemainCapacity == 0) continue;
                    }
                    if (!getdiscountType.ForceCode && !getdiscountType.ForceAttach) tempDiscount.Added = true;
                    tempDiscount.DiscountType = getdiscountType;
                    outlist.Add(tempDiscount);
                }
                return outlist;
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


        public bool Insert(List<DiscountTypeSection> discountTypeSections)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                foreach (var discountTypeSection in discountTypeSections)
                {
                    if (!new DiscountTypeSectionBO().Insert(this.ConnectionHandler, discountTypeSection))
                        throw new Exception(Resources.Payment.ErrorInInsertDiscount);
                }
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
        public IEnumerable<DiscountTypeSection> GetByModelName(string modelname)
        {
            try
            {
                return new DiscountTypeSectionBO().GetByModelName(this.ConnectionHandler, modelname);
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


        public bool Update(string modelname, List<DiscountTypeSection> discountTypeSections)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new DiscountTypeSectionBO().Update(this.ConnectionHandler, modelname, discountTypeSections))
                    return false;
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
