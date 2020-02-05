using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.DA
{
    public sealed class CustomerAdvertisementDA : DALBase<CustomerAdvertisement>
    {
        public CustomerAdvertisementDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class CustomerAdvertisementCommandBuilder
    {
    }
}
