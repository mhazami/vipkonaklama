using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Payment.DataStructure;

namespace Radyn.Payment.DA
{
    public sealed class WebDesignDiscountTypeDA : DALBase<WebDesignDiscountType>
    {
        public WebDesignDiscountTypeDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class WebDesignDiscountTypeCommandBuilder
    {
    }
}
