using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.WebDesign.DataStructure;

namespace Radyn.WebDesign.Facade.Interface
{
    public interface IWebSiteAliasFacade : IBaseFacade<WebSiteAlias>
    {
        List<WebSiteAlias> GetByWebSiteId(Guid WebSiteId);
    }
}
