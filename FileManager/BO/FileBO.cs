using System;
using System.Web;
using Radyn.FileManager.CacheManager;
using Radyn.FileManager.DAL;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using File = Radyn.FileManager.DataStructure.File;

namespace Radyn.FileManager.BO
{
    public class FileBO : BusinessBase<File>
    {
        public override bool Insert(IConnectionHandler connectionHandler, File obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            obj.SaveDate = DateTime.Now;
            var da = new FileDA(connectionHandler);
            return da.Insert(obj) > 0;
        }

        public override File Get(IConnectionHandler connectionHandler, params object[] keys)
        {
            var file = FileCacheManager.FileCache.GetItem(keys[0].ToString().ToGuid());
            if (file == null)
            {
                file = base.Get(connectionHandler, keys); 
                if (file != null)
                    FileCacheManager.FileCache.AddItem(file);
            }
            return file;

        }

        public override bool Delete(IConnectionHandler connectionHandler, params object[] keys)
        {
            FileCacheManager.FileCache.RemoveItem(keys[0].ToString().ToGuid());
            return base.Delete(connectionHandler, keys);
        }

        public Guid Insert(IConnectionHandler connectionHandler, HttpPostedFileBase file)
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
                    FileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName),

                };

                return this.Insert(connectionHandler, fileContent) ? fileContent.Id : Guid.Empty;
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
        public Guid Insert(IConnectionHandler connectionHandler, HttpPostedFileBase file, File fileoptions)
        {
            try
            {

                var picture = new byte[file.ContentLength];

                file.InputStream.Read(picture, 0, file.ContentLength);
                var extention = file.FileName.Split('.');
                var fileContent = new DataStructure.File
                {
                    Content = picture,
                    ContentType = file.ContentType,
                    Extension = extention[extention.Length - 1],
                    FileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName),

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
                    if (fileoptions.FolderId != null)
                        fileContent.FolderId = fileoptions.FolderId;
                }

                if (!this.Insert(connectionHandler, fileContent))
                    throw new Exception("خطایی ذخیره فایل وجود دارد");
                return fileContent.Id;

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
        public override bool Update(IConnectionHandler connectionHandler, File obj)
        {
            var da = new FileDA(connectionHandler);
            if (da.Update(obj) <= 0) return false;
            FileCacheManager.FileCache.RemoveItem(obj.Id);
            return true;
        }

       
    }
}
