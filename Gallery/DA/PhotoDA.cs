using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Gallery.DataStructure;

namespace Radyn.Gallery.DA
{
    public sealed class PhotoDA : DALBase<Photo>
    {
        public PhotoDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    }
    internal class PhotoCommandBuilder
    {
    }
}
