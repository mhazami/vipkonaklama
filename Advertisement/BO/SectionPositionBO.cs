using Radyn.Advertisements.DA;
using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.BO
{
    internal class SectionPositionBO : BusinessBase<SectionPosition>
    {
        public SectionPosition GetByKeyWord(IConnectionHandler handler, string keyWord)
        {
            var advertisementSectionPositionDa = new SectionPositionDA(handler);
            return advertisementSectionPositionDa.GetByKeyWord(keyWord);
        }
    }
}
