using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;
using Radyn.Security.DataStructure;
using Radyn.Security.Tools;


namespace Radyn.Security.Facade.Interface
{
    public interface IUserFacade : IBaseFacade<User>
    {
        IEnumerable<UserInfo> GetAllUserInfo();

        User Login(string username, string password);
       
        bool ChangePassword(string username, string oldPassword, string newPassword);
        bool CheckOldPassword(Guid userId, string password);
        string ForgotPassword(string username);

        Access AccessMenu(Guid userId, string url);
        bool Insert(User user, EnterpriseNode.DataStructure.EnterpriseNode enterPrise, HttpPostedFileBase fileBase);
        bool Update(User user, EnterpriseNode.DataStructure.EnterpriseNode enterPrise, HttpPostedFileBase fileBase);
        bool AddRole(Guid roleId, Guid userId);
        bool AddOperation(Guid userId, Guid operationId);
        bool AddMenu(Guid userId, List<Guid> menuId);
        bool AddAction(Guid userId, Guid actionId);
        bool AddGroup(Guid userId, Guid groupId);
    }
}
