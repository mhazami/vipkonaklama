using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.DA
{
    public sealed class AttributeDA : DALBase<Attribute>
    {
        public AttributeDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class AdvertisementAttributeCommandBuilder
    {
    }
}
