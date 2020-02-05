using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;

namespace Radyn.Security.BO
{
    internal class MenuGroupBO : BusinessBase<MenuGroup>
    {

        public bool Insert(IConnectionHandler connectionHandler, MenuGroup menuGroup, List<Guid> menulistId)
        {
            if (!base.Insert(connectionHandler, menuGroup))
                throw new Exception("خطا درذخیره دسته بندی منو وجود دارد");
            foreach (var guid in menulistId)
            {
                var menu = new MenuBO().Get(connectionHandler, guid);
                if (menu == null) continue;
                menu.MenuGroupId = menuGroup.Id;
                if (!new MenuBO().Update(connectionHandler, menu))
                    throw new Exception("خطا درذخیره دسته بندی منو وجود دارد");
            }
            return true;
        }
        public bool Update(IConnectionHandler connectionHandler, MenuGroup menuGroup, List<Guid> menulistId)
        {
            if (!base.Update(connectionHandler, menuGroup))
                throw new Exception("خطا درذخیره دسته بندی منو وجود دارد");
            var menuBo = new MenuBO();
            var enumerable =
                new OperationMenuBO().GetOprationMenu(connectionHandler, menuGroup.OperationId, menuGroup.Id);
                    
            foreach (var guid in menulistId)
            {
                var menu = menuBo.Get(connectionHandler, guid);
                if (menu == null || menu.MenuGroupId == menuGroup.Id) continue;
                menu.MenuGroupId = menuGroup.Id;
                if (!menuBo.Update(connectionHandler, menu))
                    throw new Exception("خطا درذخیره دسته بندی منو وجود دارد");
            }
            foreach (var menu in enumerable)
            {
                if (menulistId.Any(x => x == menu.Id)) continue;
                menu.MenuGroupId = null;
                if (!menuBo.Update(connectionHandler, menu))
                    throw new Exception("خطا درذخیره دسته بندی منو وجود دارد");
            }
            return true;
        }
    }
}
