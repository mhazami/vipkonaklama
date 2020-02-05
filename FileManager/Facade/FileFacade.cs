using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Radyn.FileManager.BO;
using Radyn.FileManager.DataStructure;
using Radyn.FileManager.Facade.Interface;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FileManager.Facade
{
    internal class FileFacade : FileManageBaseFacade<File>, IFileFacade
    {

        internal FileFacade()
        {

        }
        internal FileFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        public override bool Delete(params object[] keys)
        {
            var obj = new FileBO().Get(this.ConnectionHandler, keys);
            return obj == null || new FileBO().Delete(this.ConnectionHandler, keys);
        }




        public Guid InsertFile(File file)
        {
            try
            {

                if (!new FileBO().Insert(this.ConnectionHandler, file)) throw new Exception("خطایی ذخیره فایل وجود دارد");
                return file.Id;
            }

            catch (KnownException knownException)
            {
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source, knownException.StackTrace);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }

        }

        public Guid Insert(HttpPostedFileBase file)
        {
            try
            {

                return new FileBO().Insert(this.ConnectionHandler, file);
            }

            catch (KnownException knownException)
            {
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source, knownException.StackTrace);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }

        }

        public bool Insert(List<HttpPostedFileBase> file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (file == null) return false;
                var fileBo = new FileBO();
                foreach (var httpPostedFileBase in file)
                {
                    if(fileBo.Insert(this.ConnectionHandler, httpPostedFileBase)==Guid.Empty)
                        throw new Exception("خطایی ذخیره فایل وجود دارد");
                }
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source, knownException.StackTrace);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public Guid Insert(HttpPostedFileBase file, File fileoptions)
        {
            try
            {
                var insert = new FileBO().Insert(this.ConnectionHandler,file,fileoptions);
                if (insert==Guid.Empty)
                    throw new Exception("خطایی ذخیره فایل وجود دارد");
                return insert;

            }
            catch (KnownException knownException)
            {
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source, knownException.StackTrace);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }

        }

        public bool Insert(List<HttpPostedFileBase> file, File fileUptions)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (file == null) return false;
                var fileBo = new FileBO();
                foreach (var httpPostedFileBase in file)
                {
                    if (fileBo.Insert(this.ConnectionHandler, httpPostedFileBase,fileUptions) == Guid.Empty)
                        throw new Exception("خطایی ذخیره فایل وجود دارد");
                }
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source, knownException.StackTrace);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Update(HttpPostedFileBase file, Guid fileId)
        {
            try
            {

                var picture = new byte[file.ContentLength];
                file.InputStream.Read(picture, 0, file.ContentLength);
                var extention = file.FileName.Split('.');
                var fileContent = new File
                {
                    Content = picture,
                    ContentType = file.ContentType,
                    Extension = extention[extention.Length - 1],
                    SaveDate = DateTime.Now,
                    FileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName),
                    Id = fileId,

                };
                if (!fileId.Equals(Guid.Empty)) return new FileBO().Update(this.ConnectionHandler, fileContent);
                fileContent.Id = Guid.NewGuid();
                return new FileBO().Insert(this.ConnectionHandler, fileContent);
            }

            catch (KnownException knownException)
            {
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source, knownException.StackTrace);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Update(HttpPostedFileBase file, Guid fileId, File fileoptions)
        {
            try
            {

                var picture = new byte[file.ContentLength];
                file.InputStream.Read(picture, 0, file.ContentLength);
                var extention = file.FileName.Split('.');
                var fileContent = new File
                {
                    Content = picture,
                    ContentType = file.ContentType,
                    Extension = extention[extention.Length - 1],
                    SaveDate = DateTime.Now,
                    FileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName),
                    Id = fileId,

                };
                if (fileoptions != null)
                {
                    if (fileoptions.MaxSize > 0)
                    {
                        var i = file.ContentLength / 1024;
                        if (i > fileoptions.MaxSize)
                            throw new Exception(Resources.FileManager.Filesizeislargerthanallowedsize);

                    }
                    if (!string.IsNullOrEmpty(fileoptions.FileName))
                        fileContent.FileName = fileoptions.FileName;
                    if (fileoptions.FolderId.HasValue)
                        fileContent.FolderId = fileoptions.FolderId;
                }

                if (!fileId.Equals(Guid.Empty)) return new FileBO().Update(this.ConnectionHandler, fileContent);
                fileContent.Id = Guid.NewGuid();
                return new FileBO().Insert(this.ConnectionHandler, fileContent);
            }

            catch (KnownException knownException)
            {
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source, knownException.StackTrace);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }


        public override bool Update(File obj)
        {
            try
            {
                return new FileBO().Update(this.ConnectionHandler, obj);
            }
            catch (KnownException knownException)
            {
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source, knownException.StackTrace);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }


        public bool Update(Guid fileId, File fileoptions)
        {
            try
            {
                var file = new FileBO().Get(this.ConnectionHandler, fileId);
                if (file == null) return false;
                if (fileoptions != null)
                {
                    if (fileoptions.MaxSize > 0)
                    {
                        var i = file.Content.Length / 1024;
                        if (i > fileoptions.MaxSize)
                            throw new Exception(Resources.FileManager.Filesizeislargerthanallowedsize);

                    }
                    if (!string.IsNullOrEmpty(fileoptions.FileName))
                        file.FileName = fileoptions.FileName;
                    if (fileoptions.FolderId.HasValue)
                        file.FolderId = fileoptions.FolderId;
                }
                return new FileBO().Update(this.ConnectionHandler, file);
            }

            catch (KnownException knownException)
            {
                Log.Save(knownException.Message, LogType.ApplicationError, knownException.Source, knownException.StackTrace);
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
