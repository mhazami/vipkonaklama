using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.EnterpriseNode;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.BO;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;
using Radyn.Security.Tools;
using Radyn.Utility;

namespace Radyn.Security.Facade
{
    //    [FacadeCalssAttr()]
    public class UserFacade : SecurityBaseFacade<User>, IUserFacade
    {
        public UserFacade()
        {
        }

        internal UserFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

     
        public bool CheckOldPassword(Guid userId, string password)
        {
            try
            {
                var referee = new UserBO().Get(this.ConnectionHandler, userId);
                return referee.Password.Equals(StringUtils.HashPassword(password));

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
        
        public IEnumerable<UserInfo> GetAllUserInfo()
        {
            try
            {
                var list = new List<UserInfo>();
                foreach (var u in new UserBO().GetAll(this.ConnectionHandler))
                {
                    list.Add(new UserInfo
                    {
                        Id = u.Id,
                        User = u,
                        EnterPriseNode = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(u.Id)
                    });
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

        public User Login(string username, string password)
        {
            try
            {
                var hashPassword = StringUtils.HashPassword(password);
                var lower = username.ToLower();
                var firstOrDefault = new UserBO().FirstOrDefault(this.ConnectionHandler, user =>
                    user.Username == lower && user.Password == hashPassword);
                return firstOrDefault;
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





        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                var hashPassword = StringUtils.HashPassword(oldPassword);
                var lower = username.ToLower();
                var obj = base.FirstOrDefault(user =>

                    user.Username == lower &&
                    user.Password == hashPassword
                );
                if (obj == null)
                    throw new Exception("کلمه عبور جاری صحیح نمی باشد.");
                obj.Password = StringUtils.HashPassword(newPassword);
                if (!new UserBO().Update(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در تغییر رمز عبور وجود دارد");
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

        public string ForgotPassword(string username)
        {
            try
            {
                var lower = username.ToLower();
                var obj = base.FirstOrDefault(user => user.Username == lower);
                if (obj == null)
                    throw new Exception("نام کاربری وارد شده معتبر نمی باشد.");
                var newPassword = StringUtils.HashPassword(DateTime.Now.ToString()).Substring(0, 8);
                obj.Password = StringUtils.HashPassword(newPassword);
                if (!new UserBO().Update(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در تغییر رمز عبور وجود دارد");
                return newPassword;
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

        public Access AccessMenu(Guid userId, string url)
        {
            try
            {

                return new UserBO().AccessMenu(this.ConnectionHandler, userId, url);

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


        public override bool Insert(User obj)
        {
            try
            {
                obj.Password = StringUtils.HashPassword(obj.Password);
                if (!new UserBO().Insert(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ذخیره کابر وجود دارد");
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

        public override bool Update(User obj)
        {
            try
            {
                var userBo = new UserBO();
              if (!userBo.Update(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ویرایش کابر وجود دارد");
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

        public bool Insert(User obj, EnterpriseNode.DataStructure.EnterpriseNode enterPrise, HttpPostedFileBase image)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadCommitted);
                var id = obj.Id;
                BOUtility.GetGuidForId(ref id);
                obj.Id = id;
            

                enterPrise.Id = obj.Id;
                if (
                    !EnterpriseNodeComponent.Instance.EnterpriseNodeTransactionalFacade(this.EnterpriseNodeConnection)
                        .Insert(enterPrise, image))
                    throw new Exception("خطایی در ذخیره اطلاعات کاربری وجود دارد");

                if (!new UserBO().Insert(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ذخیره کاربر وجود دارد");

                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadCommitted);
                
                var userBo = new UserBO();
                var obj = userBo.Get(this.ConnectionHandler, keys);
                if (obj != null)
                {
                    if (!userBo.DeleteAssosiations(ConnectionHandler,obj))
                        throw new Exception("خطایی در حذف کاربر وجود دارد");

                    if (!userBo.Delete(this.ConnectionHandler, obj))
                            throw new Exception("خطایی در حذف کاربر وجود دارد");
                    if (!EnterpriseNodeComponent.Instance.EnterpriseNodeTransactionalFacade(this.EnterpriseNodeConnection).Delete(obj.Id))
                        throw new Exception("به دلیل وجود اطلاعات وابسته مجاز به حذف این کاربر نیستید");
                }

                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Update(User obj, EnterpriseNode.DataStructure.EnterpriseNode enterPrise, HttpPostedFileBase image)
        {

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadCommitted);
                if (
                    !EnterpriseNodeComponent.Instance.EnterpriseNodeTransactionalFacade(this.EnterpriseNodeConnection)
                        .Update(enterPrise, image))
                    throw new Exception("خطایی در ویرایش اطلاعات کاربری وجود دارد");
                var userBo = new UserBO();
                if (!userBo.Update(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ویرایش کاربر وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool AddRole(Guid roleId, Guid userId)
        {
            try
            {
                var userRoleBO = new UserRoleBO();
                var userRole = userRoleBO.Get(this.ConnectionHandler, userId, roleId);
                if (userRole == null)
                {
                    var operationMenu = new UserRole { UserId = userId, RoleId = roleId };
                    if (!userRoleBO.Insert(this.ConnectionHandler, operationMenu))
                        throw new Exception("خطایی در ذخیره  نقش کاربر  وجود دارد");
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


        public bool AddOperation(Guid userId, Guid operationId)
        {
            try
            {
                var userOperationBO = new UserOperationBO();
                var userRole = userOperationBO.Get(this.ConnectionHandler, userId, operationId);
                if (userRole == null)
                {
                    var userOperation = new UserOperation { UserId = userId, OperationId = operationId };
                    if (!userOperationBO.Insert(this.ConnectionHandler, userOperation))
                        throw new Exception("خطایی در ذخیره  عملیات کاربر  وجود دارد");
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

        public bool AddMenu(Guid userId, List<Guid> menulist)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var userMenuBO = new UserMenuBO();
                foreach (var guid in menulist)
                {
                    var userMenu = userMenuBO.Get(this.ConnectionHandler, userId, guid);
                    if (userMenu == null)
                    {
                        var userOperation = new UserMenu { UserId = userId, MenuId = guid };
                        if (!userMenuBO.Insert(this.ConnectionHandler, userOperation))
                            throw new Exception("خطایی در ذخیره  منوی کاربر  وجود دارد");
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


        public bool AddAction(Guid userId, Guid actionId)
        {
            try
            {
                var userActionBO = new UserActionBO();
                var userAction = userActionBO.Get(this.ConnectionHandler, userId, actionId);
                if (userAction != null)
                    throw new Exception("این اکشن به این کاربر قبلا به اکشن های این کاربر اضافه شده است");
                var userOperation = new UserAction { UserId = userId, ActionId = actionId };
                if (!userActionBO.Insert(this.ConnectionHandler, userOperation))
                    throw new Exception("خطایی در ذخیره  اکشن کاربر  وجود دارد");
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

        public bool AddGroup(Guid userId, Guid groupId)
        {
            try
            {
                var userGroupBO = new UserGroupBO();
                var userGroup = userGroupBO.Get(this.ConnectionHandler, userId, groupId);
                if (userGroup != null)
                    throw new Exception("این گروه به این کاربر قبلا به گروه های های این کاربر اضافه شده است");
                var userOperation = new UserGroup { UserId = userId, GroupId = groupId };
                if (!userGroupBO.Insert(this.ConnectionHandler, userOperation))
                    throw new Exception("خطایی در ذخیره  گروه کاربر  وجود دارد");
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

    }
}
