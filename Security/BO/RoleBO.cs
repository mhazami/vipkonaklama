using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DA;
using Radyn.Security.DataStructure;

namespace Radyn.Security.BO
{
internal class RoleBO : BusinessBase<Role>
{
    public IEnumerable<Role> GetNotAddedInUser(IConnectionHandler connectionHandler, Guid userId)
    {
        var da = new RoleDA(connectionHandler);
        return da.GetNotAddedInUser(userId);
    }

    public IEnumerable<Role> GetNotAddedInGroup(IConnectionHandler connectionHandler, Guid groupId)
    {
        var da = new RoleDA(connectionHandler);
        return da.GetNotAddedInGroup(groupId);
    }

   
}
}
