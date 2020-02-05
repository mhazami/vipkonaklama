using System;
using System.Web;
using Radyn.Framework;
using Radyn.WebDesign.DataStructure;
using Radyn.WebDesign.Definition;

namespace Radyn.WebDesign.Facade.Interface
{
public interface IResourceFacade : IBaseFacade<Resource>
{
    bool Insert(Resource resource, HttpPostedFileBase resourceFile);
    bool Update(Resource resource, HttpPostedFileBase resourceFile);
    string GetWebSiteResourceHtml(Guid congress, string culture, Enums.UseLayout layout);
}
}
