using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DA;
using Action = Radyn.Security.DataStructure.Action;

namespace Radyn.Security.BO
{
    internal class ActionBO : BusinessBase<DataStructure.Action>
    {
        public IEnumerable<Action> GetNotaddedInUser(IConnectionHandler connectionHandler, Guid userId)
        {
            var da = new ActionDA(connectionHandler);
            return da.GetNotaddedInUser(userId);
        }

        public IEnumerable<Action> GetNotaddedInRole(IConnectionHandler connectionHandler, Guid roleId)
        {
            var da = new ActionDA(connectionHandler);
            return da.GetNotaddedInRole(roleId);
        }
    }
}
