using System;
using System.Collections.Generic;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;

namespace Radyn.ContentManager.Facade.Interface
{
    public interface IWebDesignPagesFacade : IBaseFacade<WebDesignPages>
    {
        bool Insert(Guid websiteId, ContentManager.DataStructure.Pages pages, HtmlDesgin htmlDesgin);

        IEnumerable<ContentManager.DataStructure.Pages> GetByWebSiteId(Guid websiteId);
    }
}
