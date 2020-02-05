using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Radyn.FormGenerator.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Message;
using Radyn.Utility;
using Radyn.WebDesign.BO;
using Radyn.WebDesign.DataStructure;
using Radyn.WebDesign.Definition;
using Radyn.WebDesign.Facade.Interface;
namespace Radyn.WebDesign.Facade
{
    internal sealed class UserFacade : WebDesignBaseFacade<DataStructure.User>, IUserFacade
    {
        internal UserFacade() { }
        internal UserFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }








        public DataTable ReportUserForms(Guid formId, Guid WebId, string culture)
        {
            try
            {
                var result = new UserBO().ReportUserForms(this.ConnectionHandler, formId, WebId, culture);
                return result;
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




        #region BooleanResult
        public bool Insert(User user, 
            FormGenerator.DataStructure.FormStructure formModel, HttpPostedFileBase fileBase)
        {

            try
            {

                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FormGeneratorConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (
                    !new UserBO().Insert(this.ConnectionHandler, this.EnterpriseNodeConnection,
                        this.FormGeneratorConnection, user, formModel, fileBase))
                    throw new Exception(Resources.WebDesign.ErrorInSaveUser);
                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                this.FormGeneratorConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                this.FormGeneratorConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                this.FormGeneratorConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Update(User user,
            FormGenerator.DataStructure.FormStructure formModel)
        {

            var userBo = new UserBO();

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FormGeneratorConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (
                     !userBo.Update(this.ConnectionHandler, this.EnterpriseNodeConnection, this.FormGeneratorConnection,
                         user, formModel))
                    throw new Exception(Resources.WebDesign.ErrorInSaveUser);
                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                this.FormGeneratorConnection.CommitTransaction();
                return true;


            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                this.FormGeneratorConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                this.FormGeneratorConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }

        }
        public bool Update(User user,
            FormGenerator.DataStructure.FormStructure formModel, HttpPostedFileBase fileBase)
        {

            var userBo = new UserBO();

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FormGeneratorConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (
                     !userBo.Update(this.ConnectionHandler, this.EnterpriseNodeConnection, this.FormGeneratorConnection,
                         user,  formModel, fileBase))
                    throw new Exception(Resources.WebDesign.ErrorInSaveUser);
                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                this.FormGeneratorConnection.CommitTransaction();
                return true;


            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                this.FormGeneratorConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                this.FormGeneratorConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }

        }
        public bool Update(User user, 
            FormGenerator.DataStructure.FormStructure formModel, HttpPostedFileBase fileBase,
           List<User> childs)
        {

            var userBo = new UserBO();
            ModelView.ModifyResult<User> inform;
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FormGeneratorConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                ;
                if (
                     !userBo.Update(this.ConnectionHandler, this.EnterpriseNodeConnection, this.FormGeneratorConnection,
                         user, formModel, fileBase))
                    throw new Exception(Resources.WebDesign.ErrorInSaveUser);
                inform = new UserBO().UpdateList(this.ConnectionHandler, childs);

                this.ConnectionHandler.CommitTransaction();
               this.EnterpriseNodeConnection.CommitTransaction();
                this.FormGeneratorConnection.CommitTransaction();
                



            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                this.FormGeneratorConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                this.FormGeneratorConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            try
            {
                if (inform.SendInform)
                {
                    new UserBO().InformUserRegister(ConnectionHandler, user.WebId, inform.InformList);
                }
            }
            catch
            {

            }

            return inform.Result;

        }
        public bool CompleteRegister(User user, 
            FormStructure postFormData, HttpPostedFileBase file)
        {
            var userBo = new UserBO();
            var formEntitiys = new ModelView.InFormEntitiyList<User>();
            bool result;
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FormGeneratorConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                user.Status = Enums.UserStatus.Register;
                if (
                     !userBo.Update(this.ConnectionHandler, this.EnterpriseNodeConnection, this.FormGeneratorConnection,
                         user, postFormData, file))
                    throw new Exception(Resources.WebDesign.ErrorInSaveUser);
                formEntitiys.Add(user, Resources.WebDesign.UserInsertEmail, Resources.WebDesign.UserInsertSMS);
                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                this.FormGeneratorConnection.CommitTransaction();
                result = true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                this.FormGeneratorConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.EnterpriseNodeConnection.RollBack();
                this.FormGeneratorConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            try
            {
                if (result)
                    userBo.InformUserRegister(this.ConnectionHandler, user.WebId, formEntitiys);
            }
            catch (Exception)
            {


            }
            return result;


        }




        public User Login(string mail, string password, Guid WebId)
        {
            try
            {
                var hashPassword = StringUtils.HashPassword(password);
                return new UserBO().FirstOrDefault(this.ConnectionHandler, x => x.Username.ToLower() == mail && x.Password == hashPassword && x.WebId == WebId);


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
      



        public bool ChangePassword(Guid userId, string password)
        {
            try
            {
                var userBO = new UserBO();
                var user = userBO.Get(this.ConnectionHandler, userId);
                user.Password = password;
                return userBO.Update(this.ConnectionHandler, user);
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

        public bool CheckOldPassword(Guid userId, string password)
        {
            try
            {
                var referee = new UserBO().Get(this.ConnectionHandler, userId);
                var oldpass = StringUtils.HashPassword(password);
                return referee.Password.Equals(oldpass);

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
       




        public bool SendConfirmLink(string mail, string rerquestUrl, Guid WebId, string name)
        {
            try
            {

                var user = new UserBO().FirstOrDefault(this.ConnectionHandler, x => x.EnterpriseNode.Email == mail && x.WebId == WebId);
                if (user == null)
                    throw new Exception(Resources.WebDesign.UserNameNotRegisterInCongress);
                var congress = new WebSiteBO().Get(this.ConnectionHandler, WebId);
                var configuration = congress.Configuration;
                var title = congress.Title;
                string body =
                    string.Format(Resources.WebDesign.UserSubscribeMailContent
                        , name, rerquestUrl + "?Id=" + user.Id + "&hid=" + WebId,
                        congress.Title);
                return MessageComponenet.Instance.MailFacade.SendMail(configuration.MailHost, configuration.MailPassword,
                    configuration.MailUserName, configuration.MailFrom, configuration.MailPort, congress.Title,
                    configuration.EnableSSL, mail, Resources.WebDesign.Register + title, body);
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

        public bool ForgotPassword(string email, string requestUrl, Guid WebId)
        {
            try
            {
                var user = new UserBO().FirstOrDefault(this.ConnectionHandler, x => x.EnterpriseNode.Email == email && x.WebId == WebId);
                if (user == null)
                    throw new Exception(Resources.WebDesign.UserNameNotRegisterInCongress);

                var homa = new WebSiteBO().Get(this.ConnectionHandler, WebId);
                var configuration = homa.Configuration;
                var body =
                    string.Format(Resources.WebDesign.UserForgatPasswordMailContent
                        , user.EnterpriseNode.DescriptionField, requestUrl + "?Id=" + user.Id + "&hid=" + WebId,
                        user.WebSite.Title);
                if (
                    !MessageComponenet.Instance.MailFacade.SendMail(configuration.MailHost, configuration.MailPassword,
                        configuration.MailUserName, configuration.MailFrom, configuration.MailPort, homa.Title,
                        configuration.EnableSSL, user.EnterpriseNode.Email, Resources.WebDesign.PasswordRecovery, body))
                    return false;
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

        public bool RegisterWithOutSendMail(Guid WebId, string email, string name, string lastName)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var any = new UserBO().Any(this.ConnectionHandler, x => x.EnterpriseNode.Email == email && x.WebId == WebId);
                if (!any)
                {
                    var user = new User { Username = email.ToLower(), WebId = WebId };
                   user.Status =Enums.UserStatus.PreRegister;
                    if (
                        !new UserBO().Insert(this.ConnectionHandler, this.EnterpriseNodeConnection, user,
                            null))
                        throw new Exception(Resources.WebDesign.ErrorInSaveUser);
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



        public bool UpdateList(Guid WebDesignId, List<User> users)
        {
            var userBO = new UserBO();
            ModelView.ModifyResult<User> inform;
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                
                inform = userBO.UpdateList(this.ConnectionHandler,  users);
                this.ConnectionHandler.CommitTransaction();
                

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
            try
            {
                if (inform.SendInform)
                {
                    new UserBO().InformUserRegister(ConnectionHandler, WebDesignId, inform.InformList);
                }
            }
            catch
            {

            }

            return inform.Result;

        }

        #endregion

      


        public Enums.SubscribeStatus Register(Guid WebId, string mail, string name, string lastName,
            string rerquestUrl, string culture)
        {
            var userBo = new UserBO();
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var congress = new WebSiteBO().Get(this.ConnectionHandler, WebId);
                var user = userBo.FirstOrDefault(this.ConnectionHandler, x => x.EnterpriseNode.Email == mail && x.WebId == WebId);
                var configuration = congress.Configuration;
                Enums.SubscribeStatus subscribeStatus;
                if (user == null)
                {
                    user = new User { Username = mail.ToLower(), WebId = WebId };
                   user.Status = Enums.UserStatus.PreRegister;
                    
                    if (
                        !userBo.Insert(this.ConnectionHandler, this.EnterpriseNodeConnection, user,
                            null))
                        throw new Exception(Resources.WebDesign.ErrorInSaveUser);
                    var title = congress.Title;
                    string body =
                        string.Format(Resources.WebDesign.UserSubscribeMailContent
                            , name + " " + lastName, rerquestUrl + "?Id=" + user.Id + "&hid=" + WebId,
                            congress.Title);
                    subscribeStatus =
                        !MessageComponenet.Instance.MailFacade.SendMail(configuration.MailHost,
                            configuration.MailPassword, configuration.MailUserName, configuration.MailFrom,
                            configuration.MailPort, congress.Title, configuration.EnableSSL, mail,
                            Resources.WebDesign.Register + "  " + title, body)
                            ? Enums.SubscribeStatus.NotConfirmed
                            : Enums.SubscribeStatus.MailSent;

                }
                else if (user.Status != Enums.UserStatus.PreRegister)
                    subscribeStatus = Enums.SubscribeStatus.Registered;
                else
                    subscribeStatus = Enums.SubscribeStatus.NotConfirmed;
                this.ConnectionHandler.CommitTransaction();
                this.EnterpriseNodeConnection.CommitTransaction();
                return subscribeStatus;
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




        public Dictionary<User, List<string>> ImportFromExcel(HttpPostedFileBase fileBase, Guid WebDesignId,
            Guid? parentId)
        {
            try
            {

                return new UserBO().ImportFromExcel(this.ConnectionHandler, fileBase, WebDesignId, parentId);
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




        public bool InsertList(List<User> users)
        {

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.EnterpriseNodeConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var @select = new UserBO().Select(ConnectionHandler, x => x.Id);
                var userBo = new UserBO();
                foreach (var user in users)
                {
                    if (@select.All(x => x != user.Id))
                    {
                        if (!userBo.Insert(this.ConnectionHandler, this.EnterpriseNodeConnection, user, null))
                            throw new Exception(Resources.WebDesign.ErrorInSaveUser);
                    }
                    else
                    {
                        if (!userBo.Update(this.ConnectionHandler, this.EnterpriseNodeConnection, user, null))
                            throw new Exception(Resources.WebDesign.ErrorInSaveUser);
                    }
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


    


        public IEnumerable<User> Search(Guid WebDesignId, string txtSearch, User user,  EnterpriseNode.Tools.Enums.Gender gender,
            FormStructure formStructure)
        {
            try
            {
                return new UserBO().Search(this.ConnectionHandler, WebDesignId, txtSearch, user,  gender, formStructure);
                
              
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
        public List<dynamic> SearchDynamic(Guid WebDesignId, string txtSearch, User user,  EnterpriseNode.Tools.Enums.Gender gender,
                FormStructure formStructure)
        {
            try
            {
               return new UserBO().SearchDynamic(this.ConnectionHandler, WebDesignId, txtSearch, user,  gender, formStructure);
               

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
       

     
  



     

        public IEnumerable<ModelView.ReportChartModel> ChartUserStatusCount(Guid WebDesignId)
        {
            try
            {
                return new UserBO().ChartUserStatusCount(this.ConnectionHandler, WebDesignId);
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

        public IEnumerable<ModelView.ReportChartModel> ChartUserMothCount(Guid WebDesignId, string year)
        {
            try
            {
                return new UserBO().ChartUserMothCount(this.ConnectionHandler, WebDesignId, year);

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

        public IEnumerable<ModelView.ReportChartModel> ChartUserDayCount(Guid WebDesignId, string moth, string year)
        {
            try
            {
                return new UserBO().ChartUserDayCount(this.ConnectionHandler, WebDesignId, moth, year);
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
