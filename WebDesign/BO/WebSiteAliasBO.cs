using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using Radyn.WebDesign.DataStructure;

namespace Radyn.WebDesign.BO
{
    internal class WebSiteAliasBO : BusinessBase<WebSiteAlias>
    {
        protected override void CheckConstraint(IConnectionHandler connectionHandler, WebSiteAlias item)
        {
            base.CheckConstraint(connectionHandler, item);
            if (!string.IsNullOrEmpty(item.Url))
            {
                if (item.WebSite!=null&&item.WebSite.InstallPath == item.Url)
                    throw new Exception("این مسیر برابر مسیر اصلی وب سایت است");
                item.Url = StringUtils.Encrypt(item.Url.ToLower());
            }
        }
        public override WebSiteAlias Get(IConnectionHandler connectionHandler, params object[] keys)
        {
            var obj = base.Get(connectionHandler, keys);
            if (obj != null)
                obj.Url = StringUtils.Decrypt(obj.Url).ToLower();
            return obj;
        }

        public List<WebSiteAlias> GetByWebSiteId(IConnectionHandler connectionHandler, Guid WebSiteId)
        {
            var list = this.Where(connectionHandler, x => x.WebSiteId == WebSiteId);
            foreach (var WebSiteAliase in list)
            {
                WebSiteAliase.Url = StringUtils.Decrypt(WebSiteAliase.Url).ToLower();
            }
            return list;
        }
    }
}
