using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;

namespace Radyn.Security.DA
{
public sealed class RoleDA : DALBase<Role>
{
public RoleDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}

    public IEnumerable<Role> GetNotAddedInUser(Guid userId)
    {
        var userCommandBuilder = new RoleCommandBuilder();
        var query = userCommandBuilder.GetNotAddedInUser(userId);
        return DBManager.GetCollection<Role>(ConnectionHandler, query);
    }

    public IEnumerable<Role> GetNotAddedInGroup(Guid groupId)
    {
        var userCommandBuilder = new RoleCommandBuilder();
        var query = userCommandBuilder.GetNotAddedInGroup(groupId);
        return DBManager.GetCollection<Role>(ConnectionHandler, query);
    }
}
internal class RoleCommandBuilder
{
    public string GetNotAddedInUser(Guid userId)
    {
        return string.Format(" SELECT    Security.Role.* from Security.Role where Security.Role.Id not in (SELECT        Security.Role.Id" +
                             " FROM            Security.Role INNER JOIN " +
                             " Security.UserRole ON Security.Role.Id = Security.UserRole.RoleId  where Security.UserRole.UserId='{0}')",userId);
    }

    public string GetNotAddedInGroup(Guid groupId)
    {
        return string.Format(" SELECT    Security.Role.* from Security.Role where Security.Role.Id not in (SELECT        Security.Role.Id " +
                              " FROM            Security.Role INNER JOIN " +
                              " Security.GroupRole ON Security.Role.Id = Security.GroupRole.RoleId  where Security.GroupRole.GroupId='{0}')", groupId);
    }
}
}
