using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DA;
using Radyn.Security.DataStructure;
using Radyn.Utility;

namespace Radyn.Security.BO
{
    internal class MenuBO : BusinessBase<Menu>
    {
        public List<Menu> ChildMenus(IConnectionHandler connectionHandler, List<Menu> list, Guid? userId, int? groupId=null, bool? display = null)
        {
            if (!list.Any()) return null;
            var childMenus = new List<Menu>();
            var parents = list.Select(d => d.Id).ToList();
            if (userId == null)
            {
                var predicateBuilder = new PredicateBuilder<Menu>();
                if(groupId.HasValue)
                    predicateBuilder.And(x => x.MenuGroupId==groupId);
                predicateBuilder.And(x => x.Enabled);
                predicateBuilder.And(x => x.ParentId.Value.In(list.Select(d => d.Id)));
                if (display.HasValue)
                    predicateBuilder.And(x => x.Display == display);
                childMenus = new MenuBO().OrderBy(connectionHandler, x => x.Order, predicateBuilder.GetExpression(), true);
            }
            else
                childMenus = new MenuBO().GetChildMenus(connectionHandler, parents, (Guid)userId, display);
            return childMenus;
        }
        public List<Menu> GetChildMenus(IConnectionHandler connectionHandler, List<Guid> id, Guid userId, bool? display)
        {
            var da = new MenuDA(connectionHandler);
            return da.GetChildMenus(id, userId, display);
        }
        internal void GetChildMenu(IConnectionHandler connectionHandler, List<Menu> controlslist, List<Menu> menus, Menu parent, Guid? userId, bool? display = true)
        {

            var list = menus.Where(x => x.ParentId == parent.Id&&x.Display==display);
            foreach (var menu in list)
            {
                if (parent.Children.Any(x => x.Id == menu.Id)||controlslist.Any(x=>x.Id==menu.Id)) continue;
                parent.Children.Add(menu);
                controlslist.Add(menu);
                GetChildMenu(connectionHandler, controlslist, menus, parent, userId, display);
            }
        }

        public List<Menu> GetChildMenu(IConnectionHandler connectionHandler, Guid id, Guid userId, bool? display)
        {
            var da = new MenuDA(connectionHandler);
            return da.GetChildMenu(id, userId, display);
        }

        internal IEnumerable<Menu> GetChildMenu(IConnectionHandler connectionHandler, Guid parentId, Guid? userId, bool? display = true)
        {
            var da = new MenuDA(connectionHandler);
            var list = da.GetChildMenu(parentId, userId,display);
            var outlist = new List<Menu>();
            foreach (var menu in list)
            {
                GetChild(connectionHandler,  menu, userId);
                outlist.Add(menu);
            }
            return outlist;
        }
        private void GetChild(IConnectionHandler connectionHandler,  Menu menuTree, Guid? userId, bool? display = true)
        {
            var da = new MenuDA(connectionHandler);
            var list = da.GetChildMenu(menuTree.Id, userId,display);
            foreach (var menu in list)
            {
                if (menuTree.Children.Any(x => x.Id == menu.Id)) continue;
                menuTree.Children.Add(menu);
                GetChild(connectionHandler,  menuTree, userId);
            }
        }
     
        
       
       

        public override bool Insert(IConnectionHandler connectionHandler, Menu obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            obj.Url = obj.Url.ToLower();
            return base.Insert(connectionHandler, obj);
        }


        public IEnumerable<Menu> SearchMenu(IConnectionHandler connectionHandler, string filter)
        {
            var predicateBuilder = new PredicateBuilder<Menu>();
            if (!string.IsNullOrEmpty(filter))
                predicateBuilder.And(x => x.Url.Contains(filter) || x.Title.Contains(filter));
            return this.OrderBy(connectionHandler, x => x.Order, predicateBuilder.GetExpression());
        }

        public IEnumerable<Menu> SearchNotAddedInOpration(IConnectionHandler connectionHandler, Guid operationId, string value)
        {
            var predicateBuilder = new PredicateBuilder<Menu>();
            var guids = new OperationMenuBO().Select(connectionHandler, x => x.MenuId, x => x.OperationId == operationId, true);
            if (guids.Any())
                predicateBuilder.And(x => x.Id.NotIn(guids));
            predicateBuilder.And(x => x.Enabled);
            if (!string.IsNullOrEmpty(value))
                predicateBuilder.And(x => x.Url.Contains(value) || x.Title.Contains(value));
            return this.OrderBy(connectionHandler, x => x.Order, predicateBuilder.GetExpression());
        }
        public IEnumerable<Menu> SearchAddedInOpration(IConnectionHandler connectionHandler, Guid operationId, string value)
        {
            var predicateBuilder = new PredicateBuilder<Menu>();
            var guids = new OperationMenuBO().Select(connectionHandler, x => x.MenuId, x => x.OperationId == operationId, true);
            if (guids.Any())
                predicateBuilder.And(x => x.Id.In(guids));
            predicateBuilder.And(x => x.Enabled);
            if (!string.IsNullOrEmpty(value))
                predicateBuilder.And(x => x.Url.Contains(value) || x.Title.Contains(value));
            return this.OrderBy(connectionHandler, x => x.Order, predicateBuilder.GetExpression());
        }

    
        public IEnumerable<Menu> SearchNotAddedInUser(IConnectionHandler connectionHandler, Guid userId, string value)
        {
            var predicateBuilder = new PredicateBuilder<Menu>();
            var guids = new UserMenuBO().Select(connectionHandler, x => x.MenuId, x => x.UserId == userId, true);
            if (guids.Any())
                predicateBuilder.And(x => x.Id.NotIn(guids));
            predicateBuilder.And(x => x.Enabled);
            if (!string.IsNullOrEmpty(value))
                predicateBuilder.And(x => x.Url.Contains(value) || x.Title.Contains(value));
            return this.OrderBy(connectionHandler, x => x.Title, predicateBuilder.GetExpression());
        }

        public IEnumerable<Menu> SearchNotAddedInRole(IConnectionHandler connectionHandler, Guid roleId, string value)
        {
            var predicateBuilder = new PredicateBuilder<Menu>();
            var guids = new RoleMenuBO().Select(connectionHandler, x => x.MenuId, x => x.RoleId == roleId, true);
            if (guids.Any())
                predicateBuilder.And(x => x.Id.NotIn(guids));
            predicateBuilder.And(x => x.Enabled);
            if (!string.IsNullOrEmpty(value))
                predicateBuilder.And(x => x.Url.Contains(value) || x.Title.Contains(value));
            return this.OrderBy(connectionHandler, x => x.Title, predicateBuilder.GetExpression());
        }
    }
}
