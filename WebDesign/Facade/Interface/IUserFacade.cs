using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.FormGenerator.DataStructure;
using Radyn.Framework;
using Radyn.WebDesign.DataStructure;
using Radyn.WebDesign.Definition;

namespace Radyn.WebDesign.Facade.Interface
{
    public interface IUserFacade : IBaseFacade<User>
    {

        DataTable ReportUserForms(Guid formId, Guid WebId, string culture);
        bool Insert(User user, FormStructure formModel, HttpPostedFileBase fileBase);
        bool Update(User user,  FormStructure formModel, HttpPostedFileBase fileBase);
        bool Update(User user,  FormStructure formModel, HttpPostedFileBase fileBase, List<User> childs);
        bool CompleteRegister(User user, FormStructure postFormData, HttpPostedFileBase file);
        bool RegisterWithOutSendMail(Guid WebDesignId, string email, string name, string lastName);
        bool InsertList(List<User> users);
        bool ChangePassword(Guid userId, string password);
        bool CheckOldPassword(Guid userId, string password);
        bool SendConfirmLink(string mail, string rerquestUrl, Guid WebDesignId, string name);
        bool ForgotPassword(string mail, string rerquestUrl, Guid WebDesignId);
        bool UpdateList(Guid WebDesignId, List<User> users);
        User Login(string username, string password, Guid WebDesignId);
        IEnumerable<User> Search(Guid WebDesignId, string txtSearch, User user, EnterpriseNode.Tools.Enums.Gender gender = EnterpriseNode.Tools.Enums.Gender.None, FormStructure formStructure = null);
        List<dynamic> SearchDynamic(Guid WebDesignId, string txtSearch, User user, EnterpriseNode.Tools.Enums.Gender gender = EnterpriseNode.Tools.Enums.Gender.None, FormStructure formStructure = null);
        Enums.SubscribeStatus Register(Guid WebDesignId, string mail, string name, string lastName, string rerquestUrl, string culture);
        Dictionary<User, List<string>> ImportFromExcel(HttpPostedFileBase fileBase, Guid WebDesignId, Guid? parentId);
    }
}
