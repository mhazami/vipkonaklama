using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.DA
{
    public sealed class AdvertisementTypeDA : DALBase<AdvertisementType>
    {
        public AdvertisementTypeDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class AdvertisementTypeCommandBuilder
    {
    }
}
