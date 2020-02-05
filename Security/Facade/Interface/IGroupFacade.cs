using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Security.DataStructure;

namespace Radyn.Security.Facade.Interface
{
    public interface IGroupFacade : IBaseFacade<Group>
    {
        bool AddRole(Guid roleId, Guid groupId);
        bool RemoveRole(Guid roleId, Guid groupId);

        IEnumerable<Group> GetNotAddedInUser(Guid userId);
    }
}
