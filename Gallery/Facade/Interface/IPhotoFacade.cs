using System.Collections.Generic;
using System.Web;
using Radyn.Framework;
using Radyn.Gallery.DataStructure;

namespace Radyn.Gallery.Facade.Interface
{
    public interface IPhotoFacade : IBaseFacade<Photo>
    {
        bool Insert(Photo obj, HttpPostedFileBase image);
        bool Update(Photo obj, HttpPostedFileBase image);
        bool Delete(List<Photo> deletedImages);
    }
}
