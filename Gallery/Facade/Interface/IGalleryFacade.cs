using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;

namespace Radyn.Gallery.Facade.Interface
{
    public interface IGalleryFacade : IBaseFacade<Gallery.DataStructure.Gallery>
    {
        bool Insert(DataStructure.Gallery obj, HttpPostedFileBase image );
        bool Insert(DataStructure.Gallery obj, HttpPostedFileBase image,List<HttpPostedFileBase> data);
        bool Update(DataStructure.Gallery obj, HttpPostedFileBase image);
        bool Update(DataStructure.Gallery obj, HttpPostedFileBase image, List<HttpPostedFileBase> data);
        bool Insert(DataStructure.Gallery obj, List<HttpPostedFileBase> galleryPics);
        bool AddPicsToGallery(Guid galleryId, List<HttpPostedFileBase> newgalleryPics);
        
    }
}
