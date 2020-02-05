using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DA;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.BO
{
    internal class DiscountTypeSectionBO : BusinessBase<DiscountTypeSection>
    {
        public IEnumerable<DiscountType> GetDiscountTypes(IConnectionHandler connectionHandler, string modualName, byte section)
        {
            var discountTypeDa = new DiscountTypeDA(connectionHandler);
            return discountTypeDa.GetDiscountTypes(modualName, section);
        }

        public IEnumerable<DiscountTypeSection> GetByModelName(IConnectionHandler connectionHandler, string modelname)
        {
            return new DiscountTypeSectionBO().Where(connectionHandler,x=>x.MoudelName==modelname);
        }
        public bool Update(IConnectionHandler connectionHandler, string modelname, List<DiscountTypeSection> discountTypeSections)
        {

            var list = this.GetByModelName(connectionHandler, modelname);
            foreach (var discountTypeSection in discountTypeSections)
            {
                if (list.Any(section => section.DiscountTypeId == discountTypeSection.DiscountTypeId && section.MoudelName == discountTypeSection.MoudelName && section.Section == discountTypeSection.Section)) continue;
                if (!this.Insert(connectionHandler, discountTypeSection))
                    throw new Exception(Resources.Payment.ErrorInInsertDiscount);

            }
            foreach (var typeSection in list)
            {
                if (discountTypeSections.Any(section => section.DiscountTypeId == typeSection.DiscountTypeId && section.MoudelName == typeSection.MoudelName && section.Section == typeSection.Section))
                    continue;
                if (!this.Delete(connectionHandler, typeSection.DiscountTypeId, typeSection.MoudelName, typeSection.Section))
                    throw new Exception(Resources.Payment.ErrorInDeleteDiscount);
            }
            return true;

        }
    }
}
