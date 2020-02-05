using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.DA
{
    public sealed class SectionPositionDA : DALBase<SectionPosition>
    {
        public SectionPositionDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public SectionPosition GetByKeyWord(string keyWord)
        {
            var advertisementSectionPositionCommandBuilder = new AdvertisementSectionPositionCommandBuilder();
            var query = advertisementSectionPositionCommandBuilder.GetByKeyWord(keyWord);
            return DBManager.GetObject<SectionPosition>(base.ConnectionHandler, query);
        }
    }
    internal class AdvertisementSectionPositionCommandBuilder
    {
        public string GetByKeyWord(string keyWord)
        {
            return string.Format("SELECT     top(1) *" +
                                 " FROM         Advertisement.SectionPosition where KeyWord='{0}'", keyWord);
        }
    }
}
