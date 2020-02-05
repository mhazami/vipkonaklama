using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Security.DA
{
    public sealed class ActionDA : DALBase<Action>
    {
        public ActionDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public IEnumerable<DataStructure.Action> GetNotaddedInUser(Guid userId)
        {
            var userCommandBuilder = new ActionCommandBuilder();
            var query = userCommandBuilder.GetNotaddedInUser(userId);
            return DBManager.GetCollection<DataStructure.Action>(ConnectionHandler, query);
        }

        public IEnumerable<DataStructure.Action> GetNotaddedInRole(Guid roleId)
        {
            var userCommandBuilder = new ActionCommandBuilder();
            var query = userCommandBuilder.GetNotaddedInRole(roleId);
            return DBManager.GetCollection<DataStructure.Action>(ConnectionHandler, query);
        }
    }
    internal class ActionCommandBuilder
    {
        public string GetNotaddedInUser(Guid userId)
        {
            return string.Format(" SELECT    Security.[Action].* from Security.[Action] where Security.[Action].Id not in (SELECT        Security.[Action].Id" +
                            " FROM            Security.Action INNER JOIN " +
                            " Security.UserAction ON Security.[Action].Id = Security.UserAction.ActionId  where Security.UserAction.UserId='{0}')  order by  Security.[Action].Name ", userId);
        }

        public string GetNotaddedInRole(Guid roleId)
        {
            return string.Format(" SELECT    Security.[Action].* from Security.[Action] where Security.[Action].Id not in (SELECT        Security.[Action].Id" +
                            " FROM            Security.[Action] INNER JOIN " +
                            " Security.RoleAction ON Security.[Action].Id = Security.RoleAction.ActionId  where Security.RoleAction.RoleId='{0}')  order by  Security.[Action].Name ", roleId);
        }
    }
}
