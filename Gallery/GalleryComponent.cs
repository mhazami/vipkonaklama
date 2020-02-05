using Radyn.Framework.DbHelper;
using Radyn.Gallery.Facade;
using Radyn.Gallery.Facade.Interface;

namespace Radyn.Gallery
{
    public class GalleryComponent
    {
        private GalleryComponent()
        {

        }

        public static GalleryComponent Instance
        {
            get { return new GalleryComponent(); }
        }

        public IGalleryFacade GalleryFacade
        {
            get { return new GalleryFacade(); }
        }
        public IGalleryFacade GalleryTransactinalFacade(IConnectionHandler connectionHandler)
        {
           return new GalleryFacade(connectionHandler); 
        }
        public IPhotoFacade PhotoFacade
        {
            get { return new PhotoFacade(); }
        }
        public IPhotoFacade PhotoTransactinalFacade(IConnectionHandler connectionHandler)
        {
            return new PhotoFacade(connectionHandler); 
        }
        public IWebDesignGalleryFacade WebDesignGalleryFacade
        {
            get { return new WebDesignGalleryFacade(); }
        }


    }
}
