using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.ContentManager.BO;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;
using Radyn.FileManager;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;

namespace Radyn.ContentManager.Facade
{
    internal sealed class DefaultHtmlDesginFacade : ContentManagerBaseFacade<DefaultHtmlDesgin>, IDefaultHtmlDesginFacade
    {
        internal DefaultHtmlDesginFacade()
        {
        }

        internal DefaultHtmlDesginFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }


        public bool Insert(DefaultHtmlDesgin DefaultHtmlDesgin, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);

                if (file != null)
                {

                    DefaultHtmlDesgin.Image =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Insert(file, new File() { MaxSize = 200 }).ToString();
                }
                if (!new DefaultHtmlDesginBO().Insert(this.ConnectionHandler, DefaultHtmlDesgin))
                    throw new Exception("خطا در ذخیره اطلاعات");

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

        public bool Update(DefaultHtmlDesgin DefaultHtmlDesgin, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (file != null)
                {
                    var fileTransactionalFacade =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection);
                    var lanuageContent = new DefaultHtmlDesginBO().GetLanuageContent(base.ConnectionHandler,
                          DefaultHtmlDesgin.CurrentUICultureName, DefaultHtmlDesgin.Id);
                    if (!string.IsNullOrEmpty(lanuageContent.Image))
                    {
                        fileTransactionalFacade
                            .Update(file, lanuageContent.Image.ToGuid());
                    }
                    else
                    {
                        DefaultHtmlDesgin.Image =
                        fileTransactionalFacade
                            .Insert(file).ToString();
                    }
                }


                if (!new DefaultHtmlDesginBO().Update(this.ConnectionHandler, DefaultHtmlDesgin))
                    throw new Exception("خطا در ذخیره اطلاعات");
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
    }
}
