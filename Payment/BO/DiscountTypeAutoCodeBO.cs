using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.BO
{
    internal class DiscountTypeAutoCodeBO : BusinessBase<DiscountTypeAutoCode>
    {

        public override bool Insert(IConnectionHandler connectionHandler, DiscountTypeAutoCode obj)
        {

            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }

        public List<DiscountTypeAutoCode> GenerateAutoCode(IConnectionHandler connectionHandler, Guid discounttypeId, int count, int characterCount)
        {
            var discountTypeAutoCodelist = new List<DiscountTypeAutoCode>() { };
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < count; i++)
            {
                var builder = new StringBuilder();
                for (int j = 0; j < characterCount; j++)
                {
                    var chi = Math.Floor(99 * random.NextDouble());
                    if (chi <= 48)
                        chi += 48;
                    if (chi > 57 && chi < 65)
                        chi -= 8;
                    if (chi > 90)
                    {
                        chi -= 10;
                    }
                    var ch = Convert.ToChar(Convert.ToInt32(chi));
                    builder.Append(ch);
                }
                var code = builder.ToString().ToUpper();
                if (this.Any(connectionHandler, x => x.Code == code && x.DiscountTypeId == discounttypeId))
                    continue;
                var discountTypeAutoCode = new DiscountTypeAutoCode { Id = Guid.NewGuid(), Code = code, Used = false, DiscountTypeId = discounttypeId, State = ObjectState.New };
                discountTypeAutoCodelist.Add(discountTypeAutoCode);
            }
            var byFilter = this.Where(connectionHandler, x => x.DiscountTypeId == discounttypeId);
            foreach (var discountTypeAutoCode in byFilter)
            {
                discountTypeAutoCode.State = ObjectState.Dirty;
                discountTypeAutoCodelist.Add(discountTypeAutoCode);
            }
            return discountTypeAutoCodelist;
        }
    }
}
