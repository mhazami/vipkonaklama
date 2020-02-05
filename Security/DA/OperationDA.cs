using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;

namespace Radyn.Security.DA
{
    public sealed class OperationDA : DALBase<Operation>
    {
        public OperationDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public List<Operation> GetAllByUserId(Guid userId)
        {
            var userCommandBuilder = new OperationCommandBuilder();
            var query = userCommandBuilder.GetAllByUserId(userId);
            return DBManager.GetCollection<Operation>(ConnectionHandler, query);
        }

        public IEnumerable<Operation> GetNotAddedInRole(Guid roleId)
        {
            var userCommandBuilder = new OperationCommandBuilder();
            var query = userCommandBuilder.GetNotAddedInRole(roleId);
            return DBManager.GetCollection<Operation>(ConnectionHandler, query);
        }

        public IEnumerable<Operation> GetNotAddedInUser(Guid userId)
        {
            var userCommandBuilder = new OperationCommandBuilder();
            var query = userCommandBuilder.GetNotAddedInUser(userId);
            return DBManager.GetCollection<Operation>(ConnectionHandler, query);
        }
    }
    internal class OperationCommandBuilder
    {
        public string GetAllByUserId(Guid userId)
        {
            return string.Format("SELECT DISTINCT  " +
                                 " Security.Operation.* " +
                                 " FROM         Security.Operation INNER JOIN " +
                                 " Security.RoleOperation ON Security.Operation.Id = Security.RoleOperation.OperationId INNER JOIN  " +
                                 " Security.UserRole ON Security.RoleOperation.RoleId = Security.UserRole.RoleId where Security.Operation.Enabled=1 and  Security.UserRole.UserId='{0}' " +
                                 " union " +
                                 "SELECT   DISTINCT  Security.Operation.* " +
                                 " FROM         Security.Operation INNER JOIN " +
                                 " Security.UserOperation ON Security.Operation.Id = Security.UserOperation.OperationId where Security.Operation.Enabled=1 and Security.UserOperation.UserId='{0}' " +
                                 "union " +
                                 " SELECT   DISTINCT  Security.Operation.* " +
                                 " FROM         Security.Operation INNER JOIN " +
                                 " Security.RoleOperation ON Security.Operation.Id = Security.RoleOperation.OperationId INNER JOIN " +
                                 " Security.GroupRole ON Security.RoleOperation.RoleId = Security.GroupRole.RoleId INNER JOIN " +
                                 " Security.UserRole ON Security.RoleOperation.RoleId = Security.UserRole.RoleId where  Security.Operation.Enabled=1 and Security.UserRole.UserId='{0}' ", userId);
        }

        public string GetNotAddedInRole(Guid roleId)
        {
            return
                string.Format(
                    " select * from  Security.Operation where  Security.Operation.Id not in ( SELECT     distinct   Security.Operation.Id " +
                    " FROM            Security.Operation INNER JOIN " +
                    " Security.RoleOperation ON Security.Operation.Id = Security.RoleOperation.OperationId where Security.RoleOperation.RoleId='{0}')",roleId);
                
        }

        public string GetNotAddedInUser(Guid userId)
        {
            return
               string.Format(
                   " select * from  Security.Operation where  Security.Operation.Id not in ( SELECT     distinct   Security.Operation.Id " +
                   " FROM            Security.Operation INNER JOIN " +
                   " Security.UserOperation ON Security.Operation.Id = Security.UserOperation.OperationId where Security.UserOperation.UserId='{0}')", userId);
        }
    }
}
