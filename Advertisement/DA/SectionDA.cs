using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.DA
{
    public sealed class SectionDA : DALBase<Section>
    {
        public SectionDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class AdvertisementSectionCommandBuilder
    {
    }
}
