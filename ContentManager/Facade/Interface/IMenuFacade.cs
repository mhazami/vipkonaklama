using System;
using System.Collections.Generic;
using System.Web;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;


namespace Radyn.ContentManager.Facade.Interface
{
    public interface IMenuFacade : IBaseFacade<Menu>
    {
        bool Insert(Menu menu,  HttpPostedFileBase fileBase);
        bool Update(Menu menu,  HttpPostedFileBase fileBase);
       
        IEnumerable<Menu> MenuTree(Guid? selected);

        IEnumerable<Menu> GetChildMenu(Guid parentId, Guid selected);

    }
}
