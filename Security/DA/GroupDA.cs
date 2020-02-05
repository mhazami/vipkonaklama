using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;

namespace Radyn.Security.DA
{
public sealed class GroupDA : DALBase<Group>
{
public GroupDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}

    public IEnumerable<Group> GetNotAddedInUser(Guid userId)
    {
        var userCommandBuilder = new GroupCommandBuilder();
        var query = userCommandBuilder.GetNotAddedInUser(userId);
        return DBManager.GetCollection<Group>(ConnectionHandler, query);
    }
}
internal class GroupCommandBuilder
{
    public string GetNotAddedInUser(Guid userId)
    {
        return string.Format(" SELECT    Security.[Group].* from Security.[Group] where Security.[Group].Id not in (SELECT        Security.[Group].Id" +
                              " FROM            Security.[Group] INNER JOIN " +
                              " Security.UserGroup ON Security.[Group].Id = Security.UserGroup.[GroupId]  where Security.UserGroup.UserId='{0}')  order by  Security.[Group].Name", userId);
    }
}
}
