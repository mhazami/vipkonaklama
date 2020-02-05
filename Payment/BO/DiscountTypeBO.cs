using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.BO
{
    internal class DiscountTypeBO : BusinessBase<DiscountType>
    {

        public override bool Insert(IConnectionHandler connectionHandler, DiscountType obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }

        public override bool Delete(IConnectionHandler connectionHandler, params object[] keys)
        {
            var obj = this.Get(connectionHandler, keys);
            var discountTypeSections = new DiscountTypeSectionBO().Any(connectionHandler, x => x.DiscountTypeId == obj.Id);
            if (discountTypeSections)
                throw new Exception("این نوع تخیف قابل حذف نیست زیرا در قسمت تخفیفات استفاده شده است");
            var sections = new TempDiscountBO().Any(connectionHandler, x => x.DiscountTypeId == obj.Id);
            if (sections)
                throw new Exception("این نوع تخیف قابل حذف نیست زیرا در قسمت تخفیفات استفاده شده است");
            if (!base.Delete(connectionHandler, keys))
                throw new Exception("خطایی در حذف نوع تخفیف وجود دارد");
            var discountTypeAutoCodeBo = new DiscountTypeAutoCodeBO();
            var typeAutoCodes = discountTypeAutoCodeBo.Where(connectionHandler,
                x => x.DiscountTypeId == obj.Id);
            foreach (var discountTypeAutoCode in typeAutoCodes)
            {
                if (!discountTypeAutoCodeBo.Delete(connectionHandler, discountTypeAutoCode.Id))
                    throw new Exception("خطایی در ذخیره کد تخفیف وجود دارد");
            }

            return true;
        }

        public override DiscountType Get(IConnectionHandler connectionHandler, params object[] keys)
        {
            var item = base.Get(connectionHandler, keys);
            item.IsAutoCode = new DiscountTypeAutoCodeBO().Any(connectionHandler, x => x.DiscountTypeId == item.Id);
            item.ForceCode = item.IsAutoCode || !string.IsNullOrEmpty(item.Code);
            return item;
        }
        public bool Insert(IConnectionHandler connectionHandler, DiscountType discountType, List<DiscountTypeSection> sectiontypes, List<DiscountTypeAutoCode> discountTypeAutoCodes)
        {


            if (!this.Insert(connectionHandler, discountType, discountTypeAutoCodes))
                throw new Exception("خطایی در ذخیره نوع تخفیف وجود دارد");
            foreach (var discountTypeSection in sectiontypes)
            {
                discountTypeSection.DiscountTypeId = discountType.Id;
                if (!new DiscountTypeSectionBO().Insert(connectionHandler, discountTypeSection))
                    throw new Exception("خطایی در ذخیره نوع تخفیف وجود دارد");
            }

            return true;

        }
        public bool Update(IConnectionHandler connectionHandler, DiscountType discountType, string modelname, List<DiscountTypeSection> sectiontypes, List<DiscountTypeAutoCode> discountTypeAutoCodes)
        {


            if (!this.Update(connectionHandler, discountType, discountTypeAutoCodes))
                throw new Exception("خطایی در ذخیره نوع تخفیف وجود دارد");
            var discountTypeSectionBo = new DiscountTypeSectionBO();
            var list = discountTypeSectionBo.GetByModelName(connectionHandler, modelname);
            foreach (var discountTypeSection in sectiontypes)
            {
                if (list.Any(section => section.DiscountTypeId == discountTypeSection.DiscountTypeId && section.MoudelName == discountTypeSection.MoudelName && section.Section == discountTypeSection.Section)) continue;
                if (!discountTypeSectionBo.Insert(connectionHandler, discountTypeSection))
                    throw new Exception(Resources.Payment.ErrorInInsertDiscount);

            }
            if (list != null)
            {
                foreach (var typeSection in list.Where(x => x.DiscountTypeId == discountType.Id))
                {
                    if (sectiontypes.Any(section => section.MoudelName == typeSection.MoudelName && section.Section == typeSection.Section))
                        continue;
                    if (!discountTypeSectionBo.Delete(connectionHandler, typeSection.DiscountTypeId, typeSection.MoudelName, typeSection.Section))
                        throw new Exception(Resources.Payment.ErrorInDeleteDiscount);
                }
            }
            return true;

        }
        public bool Update(IConnectionHandler connectionHandler, DiscountType discountType, List<DiscountTypeAutoCode> discountTypeAutoCodes)
        {

            var discountTypeAutoCodeBo = new DiscountTypeAutoCodeBO();
            var typeAutoCodes = discountTypeAutoCodeBo.Where(connectionHandler,
                    x => x.DiscountTypeId == discountType.Id);
            if (discountTypeAutoCodes == null || !discountType.IsAutoCode)
            {
                foreach (var discountTypeAutoCode in typeAutoCodes)
                {

                    if (!discountTypeAutoCodeBo.Delete(connectionHandler, discountTypeAutoCode.Id))
                        throw new Exception(Resources.Payment.ErrorInSaveDiscountCode);
                }
            }
            else
            {
                foreach (var autoCode in discountTypeAutoCodes)
                {
                    if (typeAutoCodes.All(x => x.Id != autoCode.Id))
                    {
                        if (!discountTypeAutoCodeBo.Insert(connectionHandler, autoCode))
                            throw new Exception(Resources.Payment.ErrorInSaveDiscountCode);
                    }
                }
                discountType.Code = string.Empty;
                discountType.Capacity = null;
                discountType.RemainCapacity = null;

            }

            if (!base.Update(connectionHandler, discountType))
                throw new Exception(Resources.Payment.ErrorInInsertDiscount);
            return true;

        }
        public bool Insert(IConnectionHandler connectionHandler, DiscountType discountType, List<DiscountTypeAutoCode> discountTypeAutoCodes)
        {
            if (!this.Insert(connectionHandler, discountType))
                throw new Exception(Resources.Payment.ErrorInSaveDiscountCode);
            if (!discountType.IsAutoCode || discountTypeAutoCodes == null) return true;
            foreach (var autoCode in discountTypeAutoCodes)
            {
                autoCode.DiscountTypeId = discountType.Id;
                if (!new DiscountTypeAutoCodeBO().Insert(connectionHandler, autoCode))
                    throw new Exception(Resources.Payment.ErrorInSaveDiscountCode);
            }

            return true;

        }



    }
}
