using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.DA
{
    public sealed class DiscountTypeAutoCodeDA : DALBase<DiscountTypeAutoCode>
    {
        public DiscountTypeAutoCodeDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

       
    }
    internal class DiscountTypeAutoCodeCommandBuilder
    {
        
    }
}
