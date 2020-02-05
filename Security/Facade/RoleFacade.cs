using System;
using System.Collections.Generic;
using System.Data;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.BO;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    public class RoleFacade : SecurityBaseFacade<Role>, IRoleFacade
    {
        public RoleFacade()
        {
        }

        internal RoleFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        public override bool Insert(Role obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(obj);
        }

        public bool AddMenu(Guid roleId, List<Menu> menus)
        {

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                foreach (var menu in menus)
                {
                    var roleMenu1 = new RoleMenuBO().Get(this.ConnectionHandler, roleId, menu.Id);
                    if (roleMenu1 == null)
                    {
                        var roleMenu = new RoleMenu {MenuId = menu.Id, RoleId = roleId};
                        if (!new RoleMenuBO().Insert(this.ConnectionHandler, roleMenu))
                            throw new Exception("خطایی در ذخیره منوی نقش وجو دارد");
                    }

                }
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool AddMenu(Guid roleId, List<Guid> menus)
        {

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                foreach (var menu in menus)
                {
                    var roleMenu1 = new RoleMenuBO().Get(this.ConnectionHandler, roleId, menu);
                    if (roleMenu1 == null)
                    {
                        var roleMenu = new RoleMenu {MenuId = menu, RoleId = roleId};
                        if (!new RoleMenuBO().Insert(this.ConnectionHandler, roleMenu))
                            throw new Exception("خطایی در ذخیره منوی نقش وجو دارد");
                    }

                }
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool AddOperation(Guid roleId, Guid operationId)
        {
            try
            {
                var roleOperationBO = new RoleOperationBO();
                var roleOperation = roleOperationBO.Get(this.ConnectionHandler, roleId, operationId);
                if (roleOperation == null)
                {
                    var operationMenu = new RoleOperation {OperationId = operationId, RoleId = roleId};
                    if (!roleOperationBO.Insert(this.ConnectionHandler, operationMenu))
                        throw new Exception("خطایی در ذخیره منوی عملیات  وجود دارد");
                }
                return true;
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

        public bool AddAction(Guid roleId, Guid actionId)
        {
            try
            {
                var roleActionBO = new RoleActionBO();
                var roleOperation = roleActionBO.Get(this.ConnectionHandler, roleId, actionId);
                if (roleOperation == null)
                {
                    var roleAction = new RoleAction {ActionId = actionId, RoleId = roleId};
                    if (!roleActionBO.Insert(this.ConnectionHandler, roleAction))
                        throw new Exception("خطایی در ذخیره عملیات نقش  وجود دارد");
                }
                return true;
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

        public IEnumerable<Role> GetNotAddedInUser(Guid userId)
        {
            try
            {
                return new RoleBO().GetNotAddedInUser(this.ConnectionHandler, userId);
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

        public IEnumerable<Role> GetNotAddedInGroup(Guid groupId)
        {
            try
            {
                return new RoleBO().GetNotAddedInGroup(this.ConnectionHandler, groupId);
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
