using System;
using System.Data;
using Radyn.FormGenerator.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.FormGenerator.BO;
using Radyn.FormGenerator.Facade.Interface;

namespace Radyn.FormGenerator.Facade
{
    internal sealed class WebDesignFormsFacade : FormGeneratorBaseFacade<WebDesignForms>, IWebDesignFormsFacade
    {
        internal WebDesignFormsFacade() { }

        internal WebDesignFormsFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

       
        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
               
                var congressFormsBo = new WebDesignFormsBO();
                var obj = congressFormsBo.Get(this.ConnectionHandler, keys);
                if (!congressFormsBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف فرم وجود دارد");
                if (!new FormStructureBO().Delete(ConnectionHandler, FileManagerConnection,obj.FormId))
                    throw new Exception("خطایی در حذف فرم وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
               
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
              throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool UpdateAndAssgine(Guid WebId, FormStructure structure, string urls, bool forUser)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.ContentManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var formAssigmentBo = new WebDesignFormsBO();
                if (!formAssigmentBo.AssgineForm(this.ConnectionHandler, ContentManagerConnection,  WebId, structure, urls, forUser))
                    throw new Exception("خطایی در ویرایش فرم وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.ContentManagerConnection.CommitTransaction();
                
                return true;

            }

            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Insert(Guid websiteId, FormStructure formStructure)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.ContentManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                formStructure.IsExternal = true;
               if (!new FormStructureBO().Insert(ConnectionHandler, ContentManagerConnection,formStructure))
                    throw new Exception("خطایی در ذخیره فرم وجود دارد");
                var congressContent = new WebDesignForms { FormId = formStructure.Id, WebId = websiteId };
                if (!new WebDesignFormsBO().Insert(this.ConnectionHandler, congressContent))
                    throw new Exception("خطایی در ذخیره فرم وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                this.ContentManagerConnection.CommitTransaction();
               
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.ContentManagerConnection.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
