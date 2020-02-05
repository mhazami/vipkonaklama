using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Gallery.DA
{
    public sealed class WebDesignGalleryDA : DALBase<Radyn.Gallery.DataStructure.WebDesignGallery>
    {
        public WebDesignGalleryDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        { }
    }
    internal class WebDesignGalleryCommandBuilder
    {
    }
}
