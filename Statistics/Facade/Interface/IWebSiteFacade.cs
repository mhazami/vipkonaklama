using System;
using System.Web;
using Radyn.Framework;
using Radyn.Statistics.DataStructure;

namespace Radyn.Statistics.Facade.Interface
{
public interface IWebSiteFacade : IBaseFacade<WebSite>
{
    bool Insert(WebSite webSite, EnterpriseNode.DataStructure.EnterpriseNode enterpriseNode, HttpPostedFileBase file);
    bool Update(WebSite webSite, EnterpriseNode.DataStructure.EnterpriseNode enterpriseNode, HttpPostedFileBase file);
    
    Guid GetByUrlForEditor(string authority);
    bool Modify(string url, string title, Guid owner);
}
}
