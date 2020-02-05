using System;
using System.Collections.Generic;
using Radyn.FileManager.BO;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;
using Radyn.FileManager.Facade.Interface;
using Radyn.Framework.DbHelper;

namespace Radyn.FileManager.Facade
{
    public class FolderFacade : FileManageBaseFacade<Folder>, IFolderFacade
    {
        public FolderFacade() { }

           internal FolderFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

           public IEnumerable<Folder> GetParents(bool isexternal)
        {
            try
            {
                return new FolderBO().GetParents(this.ConnectionHandler,isexternal);
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

        public IEnumerable<Folder> GetAll(bool isexternal)
        {
            try
            {
                return new FolderBO().GetAll(this.ConnectionHandler, isexternal);
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

        public override bool Insert(Folder obj)
        {
            try
            {
                return new FolderBO().Insert(this.ConnectionHandler,obj);
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

        public Folder GetFirstParent(bool isexternal = false)
        {
            try
            {
                return new FolderBO().GetFirstParent(this.ConnectionHandler, isexternal);
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
        private static void FillChild(Folder list, List<Folder> folders)
        {
            var @where = FileManagerComponent.Instance.FolderFacade.Where(x => x.ParentFolderId == list.Id);
            foreach (var folder in @where)
            {
                folders.Add(folder);
                FillChild(folder, folders);
            }
        }

        public List<Folder> GetWithChilds(Guid folderId)
        {
            try
            {
                var folder=new FolderBO().Get(this.ConnectionHandler,folderId);
                var folders = new List<Folder> {folder};
                FillChild(folder, folders);
                return folders;
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
