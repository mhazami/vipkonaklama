using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.BO;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;
using Radyn.Utility;

namespace Radyn.Security.Facade
{
    internal sealed class MenuGroupFacade : SecurityBaseFacade<MenuGroup>, IMenuGroupFacade
    {
        internal MenuGroupFacade() { }

        internal MenuGroupFacade(IConnectionHandler connectionHandler)
        : base(connectionHandler)
        { }

      
        public bool Insert(MenuGroup menuGroup, List<Guid> menulistId, HttpPostedFileBase fileBase)
        {

            try
            {

                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (fileBase != null)
                    menuGroup.ImageId =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Insert(fileBase);
                if (!new MenuGroupBO().Insert(ConnectionHandler, menuGroup, menulistId))
                    throw new Exception("خطا درذخیره دسته بندی منو وجود دارد");
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

        public bool Update(MenuGroup menuGroup, List<Guid> menulistId, HttpPostedFileBase fileBase)
        {
            try
            {

                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                
                if (fileBase != null)
                {
                    var fileTransactionalFacade = FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection);
                    if (menuGroup.ImageId.HasValue)
                    {
                        if (
                            !fileTransactionalFacade
                                .Update(fileBase, (Guid)menuGroup.ImageId))
                            throw new Exception("خطایی در ویرایش عکس وجود دارد");
                    }
                    else
                        menuGroup.ImageId =
                            fileTransactionalFacade
                                .Insert(fileBase);
                }
                if (!new MenuGroupBO().Update(ConnectionHandler, menuGroup, menulistId))
                    throw new Exception("خطا درذخیره دسته بندی منو وجود دارد");
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

        public override bool Delete(params object[] keys)
        {
            try
            {
                var byFilter = new MenuBO().Any(this.ConnectionHandler, x => x.MenuGroupId == keys[0].ToString().ToInt());
                if (byFilter)
                    throw new Exception("این دسته دارای منو است شما نمیتوانید آنرا حذف کنید");
                if (!new MenuGroupBO().Delete(ConnectionHandler, keys))
                    throw new Exception("خطا در ذخیره دسته بندی منو وجود دارد");
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
