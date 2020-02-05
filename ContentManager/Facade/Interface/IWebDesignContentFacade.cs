using System;
using System.Collections.Generic;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using WebDesignContent = Radyn.ContentManager.DataStructure.WebDesignContent;

namespace Radyn.ContentManager.Facade.Interface
{
    public interface IWebDesignContentFacade : IBaseFacade<WebDesignContent>
    {
        IEnumerable<ContentManager.DataStructure.Content> GetByWebSiteId(Guid webSiteId, bool onlyenabled);
        bool Insert(Guid websiteId, ContentManager.DataStructure.Content content, ContentContent contentcontent);
    }
}
