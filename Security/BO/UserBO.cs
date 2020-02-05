using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DA;
using Radyn.Security.DataStructure;
using Radyn.Security.Tools;
using Radyn.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Radyn.Security.BO
{
    internal class UserBO : BusinessBase<User>
    {

        public Access AccessMenu(IConnectionHandler connectionHandler, Guid userId, string url)
        {

            UserDA da = new UserDA(connectionHandler);
            return da.AccessMenu(userId, url);
        }

        public override bool Insert(IConnectionHandler connectionHandler, User obj)
        {
            Guid id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            if (!string.IsNullOrEmpty(obj.Password))
                obj.Password = StringUtils.HashPassword(obj.Password);
            return base.Insert(connectionHandler, obj);
        }


        public override bool Update(IConnectionHandler connectionHandler, User user)
        {
            var oldpass = new UserBO().SelectFirstOrDefault(connectionHandler, x => x.Password, x => x.Id == user.Id);
            user.Password =
                (user.Password != null && !string.IsNullOrEmpty(user.Password.Trim()) &&
                 (oldpass == null || (oldpass.ToLower() != user.Password.ToLower())))
                    ? StringUtils.HashPassword(user.Password)
                    : oldpass;
            return base.Update(connectionHandler, user);
        }

        protected override void CheckConstraint(IConnectionHandler connectionHandler, User item)
        {
            bool byFilter = Any(connectionHandler, x =>

                x.Username.ToLower() == item.Username.ToLower() &&
                x.Id != item.Id
            );
            if (byFilter)
            {
                throw new Exception("این نام کاربری قبلا ثبت شده است");
            }

            item.Username = item.Username.ToLower();
            base.CheckConstraint(connectionHandler, item);
        }


        public bool DeleteAssosiations(IConnectionHandler connectionHandler, User user)
        {
            UserOperationBO uoBO = new UserOperationBO();
            List<UserOperation> oprationuser = uoBO.Where(connectionHandler, c => c.UserId == user.Id);
            if (oprationuser.Any())
            {
                foreach (UserOperation item in oprationuser)
                {
                    if (!uoBO.Delete(connectionHandler, item))
                    {
                        throw new Exception("خطا در حذف کاربر");
                    }
                }


            }

            return true;
        }
    }
}