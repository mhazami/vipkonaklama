using System;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Gallery.DA
{
    public sealed class GalleryDA : DALBase<Gallery.DataStructure.Gallery>
    {
        public GalleryDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public int HasPhoto(Guid id)
        {
            var galleryCommandBuilder = new GalleryCommandBuilder();
            var query = galleryCommandBuilder.HasPhoto(id);
            return DBManager.ExecuteScalar<int>(base.ConnectionHandler, query);
        }

        public int HasChild(Guid id)
        {
            var galleryCommandBuilder = new GalleryCommandBuilder();
            var query = galleryCommandBuilder.HasChild(id);
            return DBManager.ExecuteScalar<int>(base.ConnectionHandler, query);
        }

       
    }
    internal class GalleryCommandBuilder
    {
        public string HasPhoto(Guid id)
        {
            return string.Format("SELECT   COUNT(Id) " +
                                 " FROM         Gallery.Photo where Gallery.Photo.GalleryId='{0}'", id);
        }

        public string HasChild(Guid id)
        {
            return string.Format("SELECT   COUNT(Id) " +
                                 " FROM         Gallery.Gallery where Gallery.ParentGallery='{0}'", id);
        }

       
    }
}
