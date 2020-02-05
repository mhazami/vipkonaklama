using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.BO;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    internal sealed class OperationMenuFacade : SecurityBaseFacade<OperationMenu>, IOperationMenuFacade
    {
        internal OperationMenuFacade()
        {
        }

        internal OperationMenuFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

     
        public List<Menu> GetOprationMenu(Guid oprationId, Guid? userId, bool? display = true)
        {
            try
            {
                var menus = new List<Menu>();
                var list = new OperationMenuBO().GetOprationMenu(this.ConnectionHandler, oprationId);
                var childMenus = new MenuBO().ChildMenus(this.ConnectionHandler, list, userId, null,display);
                var menuBo = new MenuBO();
                foreach (var menu in list)
                {
                    menuBo.GetChildMenu(this.ConnectionHandler, menus, childMenus, menu, userId, display);

                }
                return list;
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }

        }


        public List<Menu> GetOprationMenu(Guid oprationId, int groupId, Guid? userId, bool? display = true)
        {
            try
            {
                var menus=new List<Menu>();
                var list = new OperationMenuBO().GetOprationMenu(this.ConnectionHandler, oprationId, groupId);
                var childMenus = new MenuBO().ChildMenus(this.ConnectionHandler, list, userId, groupId, display);
                var menuBo = new MenuBO();
                foreach (var menu in list)
                {
                    menuBo.GetChildMenu(this.ConnectionHandler, menus, childMenus, menu, userId, display);

                }
                return list;
            }
            catch (KnownException ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }

        }


    }
}
