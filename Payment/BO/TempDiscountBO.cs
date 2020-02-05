using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.BO
{
    internal class TempDiscountBO : BusinessBase<TempDiscount>
    {
  

        public override bool Insert(IConnectionHandler connectionHandler, TempDiscount obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }

        public bool Delete(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnection, TempDiscount tempDiscount)
        {
            if (tempDiscount.AttachId.HasValue)
            {
                FileManagerComponent.Instance.FileTransactionalFacade(filemanagerconnection).Delete(tempDiscount.AttachId);
            }
            return this.Delete(connectionHandler, tempDiscount);
        }
    





        public bool ModifyDiscount(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnection, List<DiscountType> discountAttaches, Guid tempId)
        {

            foreach (var transactionDiscountAttach in discountAttaches)
            {
                if (!this.CheckDiscoutAndSavetempDiscountType(connectionHandler, filemanagerconnection, transactionDiscountAttach, tempId))
                    throw new Exception(Resources.Payment.ErrorInSaveTransactionDiscount);
            }
            var list = this.Where(connectionHandler, discount => discount.TempId == tempId,true);
            foreach (var transactionDiscount in list)
            {
                if (discountAttaches.All(attach => attach.Id != transactionDiscount.DiscountTypeId))
                {
                    if (!this.Delete(connectionHandler, filemanagerconnection, transactionDiscount))
                        throw new Exception(Resources.Payment.ErrorInDeleteDiscount);
                }
            }
            return true;
        }
        public bool CheckDiscoutAndSavetempDiscountType(IConnectionHandler connectionHandler, IConnectionHandler filemanagerconnection, DiscountType discountType, Guid tempId)
        {
            var fileTransactionalFacade = FileManagerComponent.Instance.FileTransactionalFacade(filemanagerconnection);
            var discountType1 = new DiscountTypeBO().Get(connectionHandler, discountType.Id);
            var added = false;
            var discount = base.FirstOrDefault(connectionHandler,
                c => c.TempId == tempId && c.DiscountTypeId == discountType.Id,true);
            if (discount == null)
            {
                discount = new TempDiscount()
                {
                    TempId = tempId,
                    DiscountTypeId = discountType1.Id,

                };
            }
            else added = true;
            if (discountType1.ForceAttach)
            {
                if (discount.AttachId.HasValue)
                    fileTransactionalFacade.Update(discountType.UserAttachedFile, (Guid)discount.AttachId);
                else
                {
                    if (discountType.UserAttachedFile == null)
                        throw new Exception(Resources.Payment.PleaseEnterDiscountAttachFile);
                    discount.AttachId = fileTransactionalFacade.Insert(discountType.UserAttachedFile);
                }
            }
            if (discountType1.ForceCode)
            {
                if (string.IsNullOrEmpty(discountType.UserCode))
                    throw new Exception(Resources.Payment.PleaseEnterDiscountCode);
                if (discountType1.IsAutoCode)
                {
                    var firstOrDefault = new DiscountTypeAutoCodeBO().FirstOrDefault(connectionHandler, x =>
                    x.DiscountTypeId == discountType1.Id && x.Code == discountType.UserCode);
                    if (firstOrDefault == null)
                        throw new Exception(Resources.Payment.Discountcodeenteredisnotcorrect);
                    if (!added)
                    {
                        if (firstOrDefault.Used)
                            throw new Exception(Resources.Payment.Enterdiscountcodehasalreadybeenused);
                        firstOrDefault.Used = true;
                        if (!new DiscountTypeAutoCodeBO().Update(connectionHandler, firstOrDefault))
                            throw new Exception(Resources.Payment.ErrorInSaveDiscountCode);
                    }
                }
                else
                {
                    if (discountType1.Code != discountType.UserCode)
                        throw new Exception(Resources.Payment.Discountcodeenteredisnotcorrect);
                    if (!added)
                    {
                        if (discountType1.RemainCapacity == 0)
                            throw new Exception(Resources.Payment.Usingthecapacityfilledrebates);
                        discountType1.RemainCapacity--;
                        if (!new DiscountTypeBO().Update(connectionHandler, discountType1))
                            throw new Exception(Resources.Payment.ErrorInSaveDiscountCode);
                    }

                }
            }
            if (added)
            {
                if (!this.Update(connectionHandler, discount))
                    throw new Exception(Resources.Payment.ErrorInSaveTransactionDiscount);
            }
            else
            {
                if (!this.Insert(connectionHandler, discount))
                    throw new Exception(Resources.Payment.ErrorInSaveTransactionDiscount);
            }
            return true;
        }
    }
}
