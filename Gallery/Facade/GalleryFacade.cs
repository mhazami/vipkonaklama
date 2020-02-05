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
    internal sealed class GalleryFacade : GalleryBaseFacade<Gallery.DataStructure.Gallery>, IGalleryFacade
    {
        internal GalleryFacade()
        {
        }

        internal GalleryFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       
        public bool Insert(DataStructure.Gallery obj, HttpPostedFileBase image)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                
                if (!new GalleryBO().Insert(this.ConnectionHandler, FileManagerConnection, obj,image))
                    throw new Exception("خطایی در ذخیره گالری وجود دارد");
                
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
        public bool Insert(DataStructure.Gallery obj,  HttpPostedFileBase image, List<HttpPostedFileBase> data)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
               this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var fileTransactionalFacade = FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection);
                if (image != null)
                    obj.Thumbnail =
                        fileTransactionalFacade.Insert(image);
                if (!new GalleryBO().Insert(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ذخیره گالری وجود دارد");
              
                if (data != null)
                {
                    foreach (var httpPostedFileBase in data)
                    {
                        var photo = new Photo
                        {
                            GalleryId = obj.Id,
                            Title = httpPostedFileBase.FileName,
                            PicFile = fileTransactionalFacade.Insert(httpPostedFileBase)
                        };
                        if (!new PhotoBO().Insert(this.ConnectionHandler, photo))
                            throw new Exception("خطایی در ذخیره گالری وجود دارد");
                    }
                }

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

        public bool Update(DataStructure.Gallery obj,  HttpPostedFileBase image)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var fileTransactionalFacade =
                    FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection);
                if (image != null)
                {
                    if (obj.Thumbnail.HasValue) fileTransactionalFacade.Update(image, (Guid)obj.Thumbnail);
                    else obj.Thumbnail = fileTransactionalFacade.Insert(image);
                }
                
                if (!new GalleryBO().Update(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ویرایش گالری وجود دارد");
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

        public bool Update(DataStructure.Gallery obj,  HttpPostedFileBase image, List<HttpPostedFileBase> data)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadUncommitted);
                var fileTransactionalFacade =
                    FileManagerComponent.Instance.FileTransactionalFacade(this.FileManagerConnection);
                if (image != null)
                {
                    if (obj.Thumbnail.HasValue) fileTransactionalFacade.Update(image, (Guid)obj.Thumbnail);
                    else obj.Thumbnail = fileTransactionalFacade.Insert(image);
                }
                if (data != null)
                {
                    foreach (var httpPostedFileBase in data)
                    {
                        var photo = new Photo
                        {
                            GalleryId = obj.Id,
                            Title = httpPostedFileBase.FileName,
                            PicFile = fileTransactionalFacade.Insert(httpPostedFileBase)
                        };
                        if (!new PhotoBO().Insert(this.ConnectionHandler, photo))
                            throw new Exception("خطایی در ذخیره گالری وجود دارد");
                    }
                }
                if (!new GalleryBO().Update(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ویرایش گالری وجود دارد");
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



        public bool Insert(DataStructure.Gallery obj, List<HttpPostedFileBase> galleryPics)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new GalleryBO().Insert(this.ConnectionHandler, obj))
                    throw new Exception("خطایی در ذخیره گالری وجود دارد");
                foreach (var pic in galleryPics)
                {
                    var photo = new Photo();
                    photo.Title = "Image-" + (galleryPics.IndexOf(pic) + 1);
                    photo.UploadDate = obj.CreateDate;
                    photo.Uploader = obj.Creator;
                    photo.GalleryId = obj.Id;
                    if (!new PhotoFacade(this.ConnectionHandler).Insert(photo, pic))
                        throw new Exception("خطایی در ذخیره عکس وجود دارد");
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

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new GalleryBO().Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف گالری وجود دارد");
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

        public bool AddPicsToGallery(Guid galleryId, List<HttpPostedFileBase> newgalleryPics)
        {
            try
            {
                var gallery = new GalleryBO().Get(this.ConnectionHandler, galleryId);
                var photoFacade = new PhotoFacade(this.ConnectionHandler);
                foreach (var pic in newgalleryPics)
                {
                    var photo = new Photo();
                    photo.Title = "Image-" + (newgalleryPics.IndexOf(pic) + 1);
                    photo.UploadDate = gallery.CreateDate;
                    photo.Uploader = gallery.Creator;
                    photo.GalleryId = gallery.Id;
                    if (!photoFacade.Insert(photo, pic))
                        throw new Exception("خطایی در ذخیره عکس وجود دارد");
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

      

        public bool HasPhoto(Guid id)
        {
            try
            {
                return new GalleryBO().HasPhoto(this.ConnectionHandler, id);

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

        public bool HasChild(Guid id)
        {
            try
            {
                return new GalleryBO().HasChild(this.ConnectionHandler, id);

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
