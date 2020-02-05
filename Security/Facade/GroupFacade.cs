using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.BO;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    public class GroupFacade : SecurityBaseFacade<Group>, IGroupFacade
    {
        public GroupFacade()
        {
        }

        internal GroupFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        public bool AddRole(Guid roleId, Guid groupId)
        {

            try
            {
                var groupRoleBO = new GroupRoleBO();
                var groupRole = groupRoleBO.Get(this.ConnectionHandler, groupId, roleId);
                if (groupRole == null)
                {
                    var role = new GroupRole {GroupId = groupId, RoleId = roleId};
                    if (!groupRoleBO.Insert(this.ConnectionHandler, role))
                        throw new Exception("خطایی در ذخیره نقش گروه وجود دارد");
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

        public bool RemoveRole(Guid roleId, Guid groupId)
        {
            try
            {
                var groupRoleBO = new GroupRoleBO();
                var groupRole = groupRoleBO.Get(this.ConnectionHandler, groupId, roleId);
                if (groupRole != null)
                {
                    if (!groupRoleBO.Delete(this.ConnectionHandler, groupId, roleId))
                        throw new Exception("خطایی در حذف نقش گروه وجود دارد");
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

        public IEnumerable<Group> GetNotAddedInUser(Guid userId)
        {
            try
            {
                return new GroupBO().GetNotAddedInUser(this.ConnectionHandler, userId);
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
