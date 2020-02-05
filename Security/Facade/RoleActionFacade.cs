using System;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.BO;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    internal sealed class RoleActionFacade : SecurityBaseFacade<RoleAction>, IRoleActionFacade
    {
        internal RoleActionFacade()
        {
        }

        internal RoleActionFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       

        public bool AddAction(Guid roleId, Guid actionsId)
        {
            try
            {
                var roleActionBo = new RoleActionBO();
                var userAction = roleActionBo.Get(this.ConnectionHandler, roleId, actionsId);
                if (userAction != null)
                    throw new Exception("این اکشن به این کاربر قبلا به اکشن های این کاربر اضافه شده است");
                var userOperation = new RoleAction() {RoleId = roleId, ActionId = actionsId};
                if (!roleActionBo.Insert(this.ConnectionHandler, userOperation))
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
    }
}
