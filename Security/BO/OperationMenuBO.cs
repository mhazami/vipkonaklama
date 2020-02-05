using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;

namespace Radyn.Security.BO
{
    internal class OperationMenuBO : BusinessBase<OperationMenu>
    {
        public List<Menu> GetOprationMenu(IConnectionHandler connectionHandler, Guid oprationId)
        {
            return new OperationMenuBO().Select(connectionHandler, x => x.Menu,
                x => x.Menu.ParentId == null && x.Menu.Display && x.Menu.Enabled&&x.OperationId==oprationId,
                new OrderByModel<OperationMenu>() { Expression = x => x.Menu.Order }, true);
        }
        public List<Menu> GetOprationMenu(IConnectionHandler connectionHandler, Guid oprationId, int groupId)
        {
            return new OperationMenuBO().Select(connectionHandler, x => x.Menu,
                x => x.Menu.ParentId == null && x.Menu.MenuGroupId == groupId && x.Menu.Display && x.Menu.Enabled && x.OperationId == oprationId,
                new OrderByModel<OperationMenu>() { Expression = x => x.Menu.Order }, true);
        }



    }
}
