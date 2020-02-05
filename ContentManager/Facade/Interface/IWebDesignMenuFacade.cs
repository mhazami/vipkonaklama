using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;
using Radyn.ContentManager.DataStructure;

namespace Radyn.ContentManager.Facade.Interface
{
public interface IWebDesignMenuFacade : IBaseFacade<WebDesignMenu>
{
    bool Insert(Guid id, ContentManager.DataStructure.Menu menu, HttpPostedFileBase file);
    IEnumerable<ContentManager.DataStructure.Menu> MenuTree(Guid websiteId);
    List<Radyn.ContentManager.DataStructure.Menu> MenuTree(Guid congressId, string culture);
}
}
