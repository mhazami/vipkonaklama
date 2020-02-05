using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.WebDesign.DataStructure;

namespace Radyn.WebDesign.Facade.Interface
{
    public interface IWebSiteFacade : IBaseFacade<WebSite>
    {
        WebSite GetWebSiteByUrl(string authority);
        Guid GetWebSiteFolderId(string authority);


        bool Insert(WebSite homa, List<WebSiteAlias> list);
        bool Update(WebSite webSite, List<WebSiteAlias> list);
        void ExecureQuery(List<string> querylist);
    }
}
