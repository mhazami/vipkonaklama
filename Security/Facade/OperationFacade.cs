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

namespace Radyn.Security.Facade
{
    public class OperationFacade : SecurityBaseFacade<Operation>, IOperationFacade
    {
        public OperationFacade()
        {
        }

        internal OperationFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       

        public List<Operation> GetAllByUserId(Guid userId)
        {
            try
            {
                var list =
                    new OperationBO().GetAllByUserId(this.ConnectionHandler, userId);
               
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

        public bool AddMenu(Guid operationId, List<Guid> menuList)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var operationMenuBO = new OperationMenuBO();
                foreach (var guid in menuList)
                {
                    var groupRole = operationMenuBO.Get(this.ConnectionHandler, operationId, guid);
                    if (groupRole == null)
                    {
                        var operationMenu = new OperationMenu {OperationId = operationId, MenuId = guid};
                        if (!operationMenuBO.Insert(this.ConnectionHandler, operationMenu))
                            throw new Exception("خطایی در ذخیره منوی عملیات  وجود دارد");
                    }
                }
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



        public bool Insert(Operation operation, HttpPostedFileBase fileBase)
        {

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (fileBase != null)
                {
                    operation.LogoId =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Insert(fileBase);
                }
                var id = operation.Id;
                if (id.Equals(Guid.Empty))
                    id = Guid.NewGuid();
                operation.Id = id;
                if (!new OperationBO().Insert(this.ConnectionHandler, operation))
                    throw new Exception("خطایی در ذخیره عملیات وجود دارد");
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

        public bool Update(Operation operation, HttpPostedFileBase fileBase)
        {

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (fileBase != null)
                {
                    if (operation.LogoId.HasValue)
                    {
                        if (
                            !FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                                .Update(fileBase, (Guid) operation.LogoId))
                            throw new Exception("خطایی در ویرایش عکس وجود دارد");
                    }
                    else
                        operation.LogoId =
                            FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                                .Insert(fileBase);
                }
                if (!new OperationBO().Update(this.ConnectionHandler, operation))
                    throw new Exception("خطایی در ویرایش عملیات وجود دارد");
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

        public IEnumerable<Operation> GetNotAddedInRole(Guid roleId)
        {
            try
            {
                return new OperationBO().GetNotAddedInRole(this.ConnectionHandler, roleId);
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

        public IEnumerable<Operation> GetNotAddedInUser(Guid userId)
        {
            try
            {
                return new OperationBO().GetNotAddedInUser(this.ConnectionHandler, userId);
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
