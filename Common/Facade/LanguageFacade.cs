using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.Common.BO;
using Radyn.Common.DataStructure;
using Radyn.Common.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.Facade
{
    internal sealed class LanguageFacade : CommonBase<Language>, ILanguageFacade
    {
        internal LanguageFacade()
        {
        }

        internal LanguageFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        public bool Insert(Language obj, HttpPostedFileBase logo)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (logo != null)
                    obj.LogoId =
                        FileManager.FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Insert(logo);
                if (!new LanguageBO().Insert(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ذخیره زبان وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }

        }

        public bool Update(Language obj, HttpPostedFileBase logo)
        {
            try
            {

                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (logo != null)
                {

                    if (
                        !FileManager.FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Update(logo, obj.LogoId))
                        throw new Exception("خطایی در ویرایش زبان وجود دارد");
                }
                if (!new LanguageBO().Update(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ویرایش وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }

        }

        public List<Language> GetValidList()
        {
            try
            {
                return new LanguageBO().OrderBy(this.ConnectionHandler,x=>x.DisplayName, language => language.Enabled);

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
