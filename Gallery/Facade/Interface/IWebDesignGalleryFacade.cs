using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;

namespace Radyn.Gallery.Facade.Interface
{
public interface IWebDesignGalleryFacade : IBaseFacade<DataStructure.WebDesignGallery>
{
    IEnumerable<Gallery.DataStructure.Gallery> GetParents(Guid websiteId);
    bool Insert(Guid websiteId,  Gallery.DataStructure.Gallery gallery, HttpPostedFileBase image);
}
}
