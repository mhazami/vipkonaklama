using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;
using Radyn.Security.DataStructure;

namespace Radyn.Security.Facade.Interface
{
    public interface IOperationFacade : IBaseFacade<Operation>
    {
        bool AddMenu(Guid operationId, List<Guid> menuId);
      
        List<Operation> GetAllByUserId(Guid userId);
        bool Insert(Operation operation, HttpPostedFileBase fileBase);
        bool Update(Operation operation, HttpPostedFileBase fileBase);
        IEnumerable<Operation> GetNotAddedInRole(Guid roleId);
        IEnumerable<Operation> GetNotAddedInUser(Guid userId);
    }
}
