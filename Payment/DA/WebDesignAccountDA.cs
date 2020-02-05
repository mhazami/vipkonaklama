using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Payment.DA
{
    public sealed class WebDesignAccountDA : DALBase<DataStructure.WebDesignAccount>
    {
        public WebDesignAccountDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        {
        }
    }
}
