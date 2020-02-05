using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Security.DataStructure;

namespace Radyn.Security.Facade.Interface
{
    public interface IRoleFacade : IBaseFacade<Role>
    {
        bool AddMenu(Guid roleId,List<Menu> menus);
        bool AddMenu(Guid roleId,List<Guid> menusId);
        bool AddOperation(Guid roleId, Guid operationId);
        bool AddAction(Guid roleId, Guid actionId);


        IEnumerable<Role> GetNotAddedInUser(Guid userId);
        IEnumerable<Role> GetNotAddedInGroup(Guid groupId);
    }
}
