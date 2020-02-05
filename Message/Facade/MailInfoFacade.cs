using System;
using System.Collections.Generic;
using System.Data;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Message.BO;
using Radyn.Message.DataStructure;
using Radyn.Message.Facade.Interface;
using Radyn.Message.Tools;

namespace Radyn.Message.Facade
{
    internal sealed class MailInfoFacade : MessageBaseFacade<MailInfo>, IMailInfoFacade
    {
        internal MailInfoFacade()
        {
        }

        internal MailInfoFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }



     

        public MailInfo GetMail(Guid userId, Guid id)
        {
            try
            {
                
                return new MailInfoBO().GetMail(this.ConnectionHandler,userId,id);
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

        public IEnumerable<ModelView.InternalMailSelectUser> SearchUsers(Guid currentUserId, string selected, string value)
        {
            try
            {

                return new MailInfoBO().SearchUsers(this.ConnectionHandler, currentUserId, selected, value);

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

        public bool DeleteGroup(string idlist)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new MailInfoBO().DeleteGroup(this.ConnectionHandler, idlist))
                    throw new Exception("خطایی در حذف پیام وجو دارد");
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
    }
}
