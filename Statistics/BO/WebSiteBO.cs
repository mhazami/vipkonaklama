using System;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Statistics.DataStructure;
using Radyn.Utility;

namespace Radyn.Statistics.BO
{
internal class WebSiteBO : BusinessBase<WebSite>
{
    public WebSite GetByUrl(IConnectionHandler connectionHandler, string url)
    {
        return FirstOrDefault(connectionHandler, x => x.Url.ToLower() == url);
    }
    
    public override bool Insert(IConnectionHandler connectionHandler, WebSite obj)
    {
        var id = obj.Id;
        BOUtility.GetGuidForId(ref id);
        obj.Id = id;
        obj.RegisterDate = DateTime.Now.ShamsiDate();
        return base.Insert(connectionHandler, obj);
    }
}
}
