using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Tools;
using Radyn.Utility;

namespace Radyn.Payment.BO
{
    internal class TempBO : BusinessBase<Temp>
    {

        public override bool Insert(IConnectionHandler connectionHandler, Temp obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            obj.Date = DateTime.Now;
            return base.Insert(connectionHandler, obj);
        }
        public Transaction RemoveTempAndReturnTransaction(IConnectionHandler connectionHandler, Guid tempId)
        {
            var tre = this.Get(connectionHandler, tempId);
            if (tre == null || !tre.TransactionId.HasValue) return null;
            var transaction = new TransactionBO().Get(connectionHandler, tre.TransactionId);
            var allowdelete = true;
            if (transaction != null)
            {
                if (transaction.PayTypeId == (byte)Enums.PayType.OnlinePay && !transaction.Done)
                    allowdelete = false;
            }
            if (!allowdelete) return transaction;
            if (!this.Delete(connectionHandler, tre))
                throw new Exception(Resources.Payment.ErrorInDeleteTemp);
            return transaction;
        }
        public override bool Delete(IConnectionHandler connectionHandler, params object[] keys)
        {
            var tre = this.Get(connectionHandler, keys);
            if (tre == null) return true;
            if (!this.Delete(connectionHandler, tre))
                throw new Exception(Resources.Payment.ErrorInDeleteTemp);
            return true;
        }

        public override bool Delete(IConnectionHandler connectionHandler, Temp obj)
        {
            if (obj == null) return true;
            var tempDiscountBo = new TempDiscountBO();
            var tempdiscount = tempDiscountBo.Where(connectionHandler,
                discount => discount.TempId == obj.Id);
            foreach (var tempDiscount in tempdiscount)
            {

                if (!tempDiscountBo.Delete(connectionHandler, tempDiscount))
                    throw new Exception(Resources.Payment.ErrorInDeleteDiscount);
            }
            return base.Delete(connectionHandler, obj);
        }

        protected override void CheckConstraint(IConnectionHandler connectionHandler, Temp item)
        {
            if (item.Amount < 0) item.Amount = 0;
            if (string.IsNullOrEmpty(item.CallBackUrl))
                throw new Exception(Resources.Payment.ErrorInSaveTransaction);
            base.CheckConstraint(connectionHandler, item);
        }


        public IEnumerable<Temp> GetByUserId(IConnectionHandler connectionHandler, Guid userId)
        {
            var query = new PredicateBuilder<Temp>();
            var list = Select(connectionHandler, c => (Guid)c.ParentId, c => c.ParentId != null, true);
            if (userId != Guid.Empty)
                query.And(c => c.PayerId == userId);
            if (list.Any())
                query.And(c => c.Id.NotIn(list));
            var res = this.OrderByDescending(connectionHandler, c => c.Date, query.GetExpression());
            return res;

        }
        public int GetUserTempCount(IConnectionHandler connectionHandler, Guid userId)
        {
            var query = new PredicateBuilder<Temp>();
            var list = this.Select(connectionHandler, c => (Guid)c.ParentId, c => c.ParentId != null, true);

            if (userId != Guid.Empty)
                query.And(c => c.PayerId == userId);
            if (list.Any())
                query.And(c => c.Id.NotIn(list));

            var res = this.Count(connectionHandler, c => c.Id, query.GetExpression());
            return res;
        }

        public bool GroupPayTemp(IConnectionHandler connectionHandler, Temp temp, List<Guid> model)
        {
            temp.Description = Resources.Payment.GroupPayment + " ";
            var index = 1;
            foreach (var guid in model)
            {
                if (temp.Description.Length > 80) temp.Description += "\r\n" + " ";
                var temp1 = this.Get(connectionHandler, guid);
                if (temp1 == null) continue;
                temp.Amount += temp1.Amount;
                temp.Description += ((index > 1 && index <= model.Count) ? " " + Resources.Payment.And + " " : " ") +
                                    temp1.Description + " " + Resources.Payment.WithAmount + ":" + temp1.Amount + " " +
                                    ((Common.Definition.Enums.CurrencyType) temp1.CurrencyType)
                                    .GetDescriptionInLocalization();
                index++;
            }

            if (!this.Insert(connectionHandler, temp)) return false;
            if (!model.Any()) return true;
            var list = this.Where(connectionHandler, x => x.Id.In(model));
            var tempDiscountBo = new TempDiscountBO();
            var discounts = tempDiscountBo.Where(connectionHandler, x => x.TempId.In(model));
            foreach (var temp1 in list)
            {
                if (model.Count > 1)
                    System.Threading.Thread.Sleep(1000);
                temp1.ParentId = temp.Id;
                if (!this.Update(connectionHandler, temp1)) return false;
                var discountAttaches = discounts.Where(discount => discount.TempId == temp1.Id).ToList();
                foreach (var discountAttach in discountAttaches)
                {
                    if (discountAttaches.Count > 1)
                        System.Threading.Thread.Sleep(1000);
                    var tempDiscount = new TempDiscount()
                    {
                        TempId = temp.Id,
                        DiscountTypeId = discountAttach.DiscountTypeId,
                        AttachId = discountAttach.AttachId
                    };
                    if (!tempDiscountBo.Insert(connectionHandler, tempDiscount))
                        throw new Exception(Resources.Payment.ErrorInSaveTransactionDiscount);
                }
            }

            return true;
        }
    }
}
