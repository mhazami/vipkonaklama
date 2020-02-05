using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Radyn.ContentManager.BO
{
    internal class WebDesignMenuBO : BusinessBase<WebDesignMenu>
    {
        public List<ContentManager.DataStructure.Menu> GetParents(IConnectionHandler connectionHandler, Guid websiteId)
        {
            List<Menu> enumerable =
                    Select(connectionHandler, x => x.WebSiteMenu, x => x.WebId == websiteId);
            List<Menu> outlist = new List<ContentManager.DataStructure.Menu>();
            foreach (Menu guid in enumerable)
            {

                if (guid.ParentId != null || !guid.Enabled)
                {
                    continue;
                }

                outlist.Add(guid);
            }
            return outlist.OrderBy(x => x.Order).ToList();
        }
        public List<Radyn.ContentManager.DataStructure.Menu> GetParentsAdnChilds(IConnectionHandler connectionHandler, Guid websiteId, string culture)
        {
            List<Menu> enumerable = Select(connectionHandler, x => x.WebSiteMenu, x => x.WebId == websiteId && x.WebSiteMenu.Enabled, new OrderByModel<WebDesignMenu>() { Expression = x => x.WebSiteMenu.Order });
            List<Menu> outlist = new List<Radyn.ContentManager.DataStructure.Menu>();
            foreach (Menu menu in enumerable)
            {
                if (!string.IsNullOrWhiteSpace(culture))
                {
                    GetLanuageContent(connectionHandler, culture, menu);
                }

                outlist.Add(menu);
            }
            return outlist;



        }
    }
}
