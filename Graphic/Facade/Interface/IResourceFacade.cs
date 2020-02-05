using System;
using System.Web;
using Radyn.Framework;
using Radyn.Graphic.DataStructure;

namespace Radyn.Graphic.Facade.Interface
{
public interface IResourceFacade : IBaseFacade<Resource>
{
        bool Insert(Resource resource, HttpPostedFileBase resourceFile);
        bool Update(Resource resource, HttpPostedFileBase resourceFile);
        string GetThemeResourceHtml(Guid websiteId, string culture);
    }
}
