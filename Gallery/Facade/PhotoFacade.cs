using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Gallery.BO;
using Radyn.Gallery.DataStructure;
using Radyn.Gallery.Facade.Interface;

namespace Radyn.Gallery.Facade
{
    internal sealed class PhotoFacade : GalleryBaseFacade<Photo>, IPhotoFacade
    {
        internal PhotoFacade()
        {
        }

        internal PhotoFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        
      

        public bool Insert(Photo obj, HttpPostedFileBase image)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (image != null)
                    obj.PicFile =
                        FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection).Insert(image);
                if (!new PhotoBO().Insert(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ذخیره عکس وجود دارد");
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

        public bool Update(Photo obj, HttpPostedFileBase image)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                if (image != null)
                    FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                        .Update(image, obj.PicFile);
                if (!new PhotoBO().Update(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ویرایش عکس وجود دارد");
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
                var obj = new PhotoBO().Get(this.ConnectionHandler, keys);
                if (!new PhotoBO().Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف عکس وجود دارد");
                if (
                    !FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection)
                        .Delete(obj.PicFile))
                    throw new Exception("خطایی در حذف عکس وجود دارد");
              
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

        public bool Delete(List<Photo> deletedImages)
        {
            try
            {
                var photoFacade = new PhotoFacade(this.ConnectionHandler);
                foreach (var deletedImage in deletedImages)
                {
                    if (!photoFacade.Delete(deletedImage.Id))
                        throw new Exception("خطایی در خذف عکس وجود دارد");
                }
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
