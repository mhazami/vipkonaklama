using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.DA
{
    public sealed class PagesDA : DALBase<Pages>
    {
        public PagesDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        {
        }
    }
}
