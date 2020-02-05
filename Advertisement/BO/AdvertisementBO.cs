using System.Collections.Generic;
using Radyn.Advertisements.DA;
using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.BO
{
    internal class AdvertisementBO : BusinessBase<Advertisement>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Advertisement obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref  id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }

        public IEnumerable<Advertisement> GetAllByDate(IConnectionHandler connectionHandler, int sectionPositionId, string date)
        {
            var da = new AdvertisementDA(connectionHandler);
            return da.GetAllByDate(sectionPositionId, date);
        }
    }
}
