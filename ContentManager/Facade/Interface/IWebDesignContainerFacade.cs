using System;
using System.Collections.Generic;
using Radyn.Framework;
using WebDesignContainer = Radyn.ContentManager.DataStructure.WebDesignContainer;

namespace Radyn.ContentManager.Facade.Interface
{
public interface IWebDesignContainerFacade : IBaseFacade<WebDesignContainer>
{
    IEnumerable<ContentManager.DataStructure.Container> GetByWebSiteId(Guid websiteId);
    bool Insert(Guid websiteId, ContentManager.DataStructure.Container container);
    
}
}
