using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.BO;
using Radyn.Security.DataStructure;
using Radyn.Security.Facade.Interface;

namespace Radyn.Security.Facade
{
    public class MenuFacade : SecurityBaseFacade<Menu>, IMenuFacade
    {
        public MenuFacade()
        {
        }

        internal MenuFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

    


        public IEnumerable<Menu> SearchNotAddedInOpration(Guid operationId, string value)
        {
            try
            {
                var list = new MenuBO().SearchNotAddedInOpration(this.ConnectionHandler, operationId, value);
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

        public IEnumerable<Menu> SearchAddedInOpration(Guid operationId, string value)
        {
            try
            {
                var list = new MenuBO().SearchAddedInOpration(this.ConnectionHandler, operationId, value);
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
        

        public IEnumerable<Menu> SearchNotAddedInUser(Guid userId, string value)
        {
            try
            {
                var list = new MenuBO().SearchNotAddedInUser(this.ConnectionHandler, userId, value);
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

        public IEnumerable<Menu> SearchNotAddedInRole(Guid roleId, string value)
        {
            try
            {
                var list = new MenuBO().SearchNotAddedInRole(this.ConnectionHandler, roleId, value);
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


        public IEnumerable<Menu> Search(string filter)
        {
            try
            {
                var list = new MenuBO().SearchMenu(this.ConnectionHandler, filter);
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

        public bool Insert(Menu menu, HttpPostedFileBase fileBase)
        {

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (fileBase != null)
                    menu.ImageId =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Insert(fileBase);
                if (!new MenuBO().Insert(this.ConnectionHandler, menu))
                    throw new Exception("خطایی در ذخیره منو وجود دارد");
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

        public bool Update(Menu menu, HttpPostedFileBase fileBase)
        {

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (fileBase != null)
                {
                    if (menu.ImageId.HasValue)
                    {
                        if (
                            !FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                                .Update(fileBase, (Guid) menu.ImageId))
                            throw new Exception("خطایی در ویرایش عکس وجود دارد");
                    }
                    else
                        menu.ImageId =
                            FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                                .Insert(fileBase);
                }
                if (!new MenuBO().Update(this.ConnectionHandler, menu))
                    throw new Exception("خطایی در ویرایش منو وجود دارد");
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

        public IEnumerable<Menu> GetChildMenu(Guid parentId, Guid? userId)
        {
            try
            {
                return new MenuBO().GetChildMenu(this.ConnectionHandler, parentId, userId);
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





        public List<Menu> GetByOprationIdParentIsnull(Guid operationId)
        {
            try
            {

                var list = new OperationMenuBO().GetOprationMenu(this.ConnectionHandler, operationId);
                return list.OrderBy(x => x.TitleWithParentTitle).ToList();
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
