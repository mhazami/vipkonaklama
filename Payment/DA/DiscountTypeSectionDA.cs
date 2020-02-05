using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.DA
{
    public sealed class DiscountTypeSectionDA : DALBase<DiscountTypeSection>
    {
        public DiscountTypeSectionDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class DiscountTypeSectionCommandBuilder
    {
    }
}
