using System;
using System.Collections.Generic;
using System.Web;
using Radyn.ContentManager.DA;
using Radyn.ContentManager.DataStructure;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.ContentManager.BO
{
    public class MenuBO : BusinessBase<Menu>
    {
       

        public IEnumerable<Menu> GetChildMenu(IConnectionHandler connectionHandler, Guid parentId, Guid? selected)
        {

            var list = this.OrderBy(connectionHandler, x => x.Order, x => x.ParentId == parentId);
            var outlist = new List<Menu>();
            foreach (var menu in list)
            {
                if (menu.Id == selected)
                    menu.Selected = true;
                GetChild(connectionHandler, menu, selected);
                outlist.Add(menu);
            }
            return outlist;
        }
        public override bool Insert(IConnectionHandler connectionHandler, Menu obj)
        {

            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            if (obj.Order == 0)
                obj.Order = this.GetMaxOrder(connectionHandler) + 1;
            return base.Insert(connectionHandler, obj);
        }
        public bool Insert(IConnectionHandler connectionHandler, IConnectionHandler FileManagerConnection, Menu obj, HttpPostedFileBase fileBase)
        {
           
                if (fileBase != null)
                    obj.ImageUrl =
                        FileManagerComponent.Instance.FileTransactionalFacade(FileManagerConnection)
                            .Insert(fileBase);
                if (!this.Insert(connectionHandler, obj))
                    throw new Exception("خطایی در ذخیره منو وجود دارد");


                return true;

        }

        public  bool Delete(IConnectionHandler connectionHandler, IConnectionHandler FileManagerConnection, params object[] keys)
        {
            var obj = new MenuBO().Get(connectionHandler, keys);
            if (obj == null) return false;
            if (obj.ImageUrl != null)
                if (
                    !FileManagerComponent.Instance.FileTransactionalFacade(FileManagerConnection)
                        .Delete(obj.ImageUrl))
                    return false;
            var contents = new ContentBO().Where(connectionHandler, content => content.MenuId == obj.Id);
            if (contents.Count > 0)
            {
                foreach (var content in contents)
                {
                    content.MenuId = null;
                    if (!new ContentBO().Update(connectionHandler, content))
                        throw new Exception("خطایی در ویرایش منوی محتوا وجود دارد");
                }

            }

            return true;

        }

        private int GetMaxOrder(IConnectionHandler connectionHandler)
        {
            var da = new MenuDA(connectionHandler);
            return da.GetMaxOrder();
        }
      
        private void GetChild(IConnectionHandler connectionHandler, Menu menuTree, Guid? selected)
        {

            var list = this.OrderBy(connectionHandler, x => x.Order, x => x.ParentId == menuTree.Id);
            foreach (var menu in list)
            {
                if (menu.Id == selected)
                    menu.Selected = true;
                menuTree.Children.Add(menu);
                GetChild(connectionHandler, menu, selected);
            }
        }

    }
}
