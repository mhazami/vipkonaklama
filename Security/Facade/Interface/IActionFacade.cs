using System;
using System.Collections.Generic;
using Radyn.Framework;


namespace Radyn.Security.Facade.Interface
{
    public interface IActionFacade : IBaseFacade<DataStructure.Action>
    {
        IEnumerable<DataStructure.Action> GetNotaddedInUser(Guid userId);
        IEnumerable<DataStructure.Action> GetNotaddedInRole(Guid roleId);
    }
}
