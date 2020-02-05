using System.Collections.Generic;
using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.DA
{
    public sealed class AdvertisementDA : DALBase<Advertisement>
    {
        public AdvertisementDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public IEnumerable<Advertisement> GetAllByDate(int sectionPositionId, string date)
        {
            var advertisementCommandBuilder = new AdvertisementCommandBuilder();
            var query = advertisementCommandBuilder.GetAllByDate(sectionPositionId, date);
            return DBManager.GetCollection<Advertisement>(base.ConnectionHandler, query);
        }
    }
    internal class AdvertisementCommandBuilder
    {
        public string GetAllByDate(int sectionPositionId, string date)
        {
            return string.Format("SELECT     *" +
                                 " FROM         Advertisement.Advertisement where  PositionId={0} and " +
                                 " (ToDate is null or (ToDate>='{1}' and FromDate<='{1}')) order by VisitCount,ClickCount",
                sectionPositionId, date);
        }
    }
}
