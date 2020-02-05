using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DA;
using Radyn.Security.DataStructure;

namespace Radyn.Security.BO
{
internal class GroupBO : BusinessBase<Group>
{
    public IEnumerable<Group> GetNotAddedInUser(IConnectionHandler connectionHandler, Guid userId)
    {
        var da = new GroupDA(connectionHandler);
        return da.GetNotAddedInUser(userId);
    }
}
}
