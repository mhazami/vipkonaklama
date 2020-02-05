using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;
using Radyn.Security.DataStructure;


namespace Radyn.Security.Facade.Interface
{
    public interface IMenuFacade : IBaseFacade<Menu>
    {
     
        IEnumerable<Menu> Search(string filter);
        bool Insert(Menu menu, HttpPostedFileBase fileBase);
        bool Update(Menu menu, HttpPostedFileBase fileBase);
        IEnumerable<Menu> GetChildMenu(Guid parentId, Guid? userId);
      
        List<Menu> GetByOprationIdParentIsnull(Guid operationId);
        
        IEnumerable<Menu> SearchNotAddedInOpration(Guid operationId, string value);
        IEnumerable<Menu> SearchAddedInOpration(Guid operationId, string value);
        IEnumerable<Menu> SearchNotAddedInUser(Guid userId, string value);
        IEnumerable<Menu> SearchNotAddedInRole(Guid roleId, string value);
       
    }
}
