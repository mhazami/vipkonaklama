using System;
using Radyn.Framework;
using Radyn.Security.DataStructure;

namespace Radyn.Security.Facade.Interface
{
public interface IRoleActionFacade : IBaseFacade<RoleAction>
{
    bool AddAction(Guid roleId, Guid actionsId);
}
}
