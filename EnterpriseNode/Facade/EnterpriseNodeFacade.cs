using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.EnterpriseNode.BO;
using Radyn.EnterpriseNode.Facade.Interface;
using Radyn.EnterpriseNode.Tools;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.EnterpriseNode.Facade
{
    public class EnterpriseNodeFacade : EnterpriseNodeBaseFacade<DataStructure.EnterpriseNode>, IEnterpriseNodeFacade
    {
        internal EnterpriseNodeFacade()
        {
        }
            
        internal EnterpriseNodeFacade(IConnectionHandler connectionHandler) : base(connectionHandler)
        {
        }

     


        public override bool Insert(DataStructure.EnterpriseNode obj)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new EnterpriseNodeBO().Insert(this.ConnectionHandler, obj))
                   throw new KnownException("خطا در ثبت اطلاعات شخص");
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
       

        public override bool Update(DataStructure.EnterpriseNode obj)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new EnterpriseNodeBO().Update(this.ConnectionHandler, obj))
                    throw new KnownException("خطا در ثبت اطلاعات شخص");
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

     

        public bool Insert(DataStructure.EnterpriseNode obj, HttpPostedFileBase file)
        {

            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new EnterpriseNodeBO().Insert(this.ConnectionHandler, this.FileManagerConnection, obj, file))
                    throw new KnownException("خطا در ثبت اطلاعات شخص");
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

        public List<DataStructure.EnterpriseNode> Search(DataStructure.EnterpriseNode filter,bool fillcomplex=true)
        {
            try
            {
                var list = new EnterpriseNodeBO().Search(this.ConnectionHandler, filter);
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
        public List<DataStructure.EnterpriseNode> Search(string filter, Enums.EnterpriseNodeType enterpriseNodeTypeId = Enums.EnterpriseNodeType.RealEnterPriseNode, bool fillcomplex = true)
        {
            try
            {
                var list = string.IsNullOrEmpty(filter)
                    ? new List<DataStructure.EnterpriseNode>()
                    : new EnterpriseNodeBO().Search(this.ConnectionHandler, filter.Trim(),
                        enterpriseNodeTypeId);
               

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
     

      
     
        public bool Update(DataStructure.EnterpriseNode obj, HttpPostedFileBase file)
        {
            
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new EnterpriseNodeBO().Update(this.ConnectionHandler, this.FileManagerConnection, obj, file))
                    throw new KnownException("خطا در ثبت اطلاعات شخص");
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
                
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
             
                var obj = new EnterpriseNodeBO().Get(this.ConnectionHandler, keys);
                if (!new EnterpriseNodeBO().Delete(this.ConnectionHandler, this.FileManagerConnection, obj))
                    throw new KnownException("خطا در حذف اطلاعات شخص");
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
