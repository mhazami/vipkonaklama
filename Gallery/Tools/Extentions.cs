using Radyn.Gallery.Facade;

namespace Radyn.Gallery.Tools
{
   public static  class Extentions
    {
       public static bool HasImage(this DataStructure.Gallery gallery)
       {
           return new GalleryFacade().HasPhoto(gallery.Id);
       }
       public static bool HasChild(this DataStructure.Gallery gallery)
       {
           return new GalleryFacade().HasChild(gallery.Id);
       }

      
   }
}
