using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Radyn.FileManager.BO;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.FileManager.Facade.Interface;
using WebDesignFolder = Radyn.FileManager.DataStructure.WebDesignFolder;

namespace Radyn.FileManager.Facade
{
    internal sealed class WebDesignFolderFacade : FileManageBaseFacade<WebDesignFolder>, IWebDesignFolderFacade
    {
        internal WebDesignFolderFacade() { }

        internal WebDesignFolderFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }
    

        public override bool Delete(params object[] keys)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
               var folderBo = new WebDesignFolderBO();
                var obj = folderBo.Get(this.ConnectionHandler, keys);
                if (!folderBo.Delete(this.ConnectionHandler, keys))
                    throw new Exception("خطایی در حذف پوشه فایل  وجود دارد");
                if (!new FolderBO().Delete(ConnectionHandler,obj.FolderId))
                    throw new Exception("خطایی در حذف پوشه فایل  وجود دارد");
                this.ConnectionHandler.CommitTransaction();
               return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
              throw new KnownException(ex.Message, ex);
            }
        }

        public IEnumerable<FileManager.DataStructure.Folder> GetParents(Guid websiteId)
        {
            try
            {

                var list = FileManagerComponent.Instance.FolderFacade.GetParents(true);
                var listfolder = new List<FileManager.DataStructure.Folder>();
                var congressFoldersBo = new FolderBO();
                foreach (var folder in list)
                {
                    var congressFolders = congressFoldersBo.Get(this.ConnectionHandler, websiteId, folder.Id);
                    if (congressFolders == null) continue;
                    listfolder.Add(folder);
                }
                return listfolder.OrderBy(folder => folder.Title);
            }
            catch (KnownException knownException)
            {

                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {

                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Insert(Guid websiteId, FileManager.DataStructure.Folder folder)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                folder.IsExternal = true;
                if (!new FolderBO().Insert(ConnectionHandler,folder))
                    throw new Exception("خطایی در ذخیره پوشه فایل وجود دارد");
                var designFolder = new WebDesignFolder { WebId = websiteId, FolderId = folder.Id };
                if (!new WebDesignFolderBO().Insert(this.ConnectionHandler, designFolder))
                    throw new Exception("خطایی در ذخیره پوشه فایل  وجود دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException knownException)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }

     
    }
}
