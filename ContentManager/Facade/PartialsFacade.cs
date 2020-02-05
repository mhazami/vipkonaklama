using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.ContentManager.BO;
using Radyn.ContentManager.DataStructure;
using Radyn.ContentManager.Facade.Interface;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;

namespace Radyn.ContentManager.Facade
{
    internal sealed class PartialsFacade : ContentManagerBaseFacade<Partials>, IPartialsFacade
    {
        internal PartialsFacade()
        {
        }

        internal PartialsFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       
        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                var partialsBO = new PartialsBO();
                if (!partialsBO.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف صفحه وجود دارد");
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


      

      

       
        public bool Insert(Partials supporter, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);

                if (file != null)
                {

                    supporter.Image =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                            .Insert(file).ToString();
                }
                if (!new PartialsBO().Insert(this.ConnectionHandler, supporter))
                    throw new Exception("خطایی در حذف صفحه وجود دارد");

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

        public bool Update(Partials supporter, HttpPostedFileBase file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (file != null)
                {
                    var fileTransactionalFacade =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection);
                    var lanuageContent = new PartialsBO().GetLanuageContent(base.ConnectionHandler,
                          supporter.CurrentUICultureName, supporter.Id);
                    if (!string.IsNullOrEmpty(lanuageContent.Image))
                        fileTransactionalFacade.Update(file, lanuageContent.Image.ToGuid());
                    else
                        supporter.Image = fileTransactionalFacade.Insert(file).ToString();
                }


                if (!new PartialsBO().Update(this.ConnectionHandler, supporter))
                    throw new Exception("خطایی در حذف صفحه وجود دارد");
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
        public List<Partials> GetContentPartials(IEnumerable<int> idlist,string culture)
        {

            try
            {

                var contentBo = new ContentBO();
                var contents = contentBo.Where(ConnectionHandler, x => x.Id.In(idlist));
                return new PartialsBO().GetContentPartials(ConnectionHandler, contents, culture);
            }
            catch (KnownException ex)
            {
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public List<Partials> GetContentPartials(string culture)
        {
            try
            {
                
                var contentBo = new ContentBO();
               var contents = contentBo.Where(ConnectionHandler,x=>x.Enabled&&x.IsExternal==false);
                return new PartialsBO().GetContentPartials(ConnectionHandler, contents,culture);
            }
            catch (KnownException ex)
            {
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

    

        public List<Partials> GetOperationPartials(Guid OperationId)
        {
            try
            {
                return new PartialsBO().GetOperationPartials(ConnectionHandler, OperationId);

            }
            catch (KnownException ex)
            {
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
