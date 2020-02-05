using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;

namespace Radyn.Security.DA
{
    public sealed class OperationMenuDA : DALBase<OperationMenu>
    {
        public OperationMenuDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public List<Menu> GetOprationMenu(Guid oprationId)
        {
            var operationMenuCommandBuilder = new OperationMenuCommandBuilder();
            var query = operationMenuCommandBuilder.GetOprationMenu(oprationId);
            return DBManager.GetCollection<Menu>(base.ConnectionHandler, query);
        }
        public List<Menu> GetOprationMenu(Guid oprationId, int groupId)
        {
            var operationMenuCommandBuilder = new OperationMenuCommandBuilder();
            var query = operationMenuCommandBuilder.GetOprationMenu(oprationId,groupId);
            return DBManager.GetCollection<Menu>(base.ConnectionHandler, query);
        }
       

        public IEnumerable<Menu> GetMenuTree(Guid oprationId)
        {
            var operationMenuCommandBuilder = new OperationMenuCommandBuilder();
            var query = operationMenuCommandBuilder.GetMenuTree(oprationId);
            return DBManager.GetCollection<Menu>(base.ConnectionHandler, query);
        }

        
    }
    internal class OperationMenuCommandBuilder
    {
        public string GetOprationMenu(Guid oprationId)
        {
            return string.Format("SELECT     [Security].Menu.* " +
                                 " FROM         [Security].Menu INNER JOIN " +
                                 " [Security].OperationMenu ON [Security].Menu.Id = [Security].OperationMenu.MenuId " +
                                 " where [Security].OperationMenu.OperationId='{0}' and [Security].Menu.ParentId is null  and   [Security].Menu.Display=1 and [Security].Menu.[Enabled]=1 order by [Security].Menu.[Order]",
                oprationId);
        }
        public string GetOprationMenu(Guid oprationId,int groupId)
        {
            return string.Format("SELECT     [Security].Menu.* " +
                                 " FROM         [Security].Menu INNER JOIN " +
                                 " [Security].OperationMenu ON [Security].Menu.Id = [Security].OperationMenu.MenuId " +
                                 " where [Security].OperationMenu.OperationId='{0}' and [Security].Menu.MenuGroupId={1} and [Security].Menu.ParentId is null  and   [Security].Menu.Display=1 and [Security].Menu.[Enabled]=1 order by [Security].Menu.[Order]",
                oprationId,groupId);
        }
        
    
        public string GetMenuTree(Guid oprationId)
        {
            return string.Format("SELECT     [Security].Menu.* " +
                                 " FROM         [Security].Menu INNER JOIN " +
                                 " [Security].OperationMenu ON [Security].Menu.Id = [Security].OperationMenu.MenuId " +
                                 " where [Security].OperationMenu.OperationId='{0}' and [Security].Menu.ParentId is null and   [Security].Menu.[Enabled]=1 order by [Security].Menu.[Order]",
                oprationId);
        }

        
    }
}
