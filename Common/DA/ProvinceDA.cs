using Radyn.Common.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.DA
{
    public sealed class ProvinceDA : DALBase<Province>
    {
        public ProvinceDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class ProvinceCommandBuilder
    {
    }
}
