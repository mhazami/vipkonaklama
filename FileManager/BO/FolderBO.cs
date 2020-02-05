using System;
using System.Collections.Generic;
using Radyn.FileManager.DAL;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FileManager.BO
{
    internal class FolderBO : BusinessBase<Folder>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Folder obj)
        {
            if (obj.Id.Equals(Guid.Empty)) obj.Id = Guid.NewGuid();
            obj.CreateDate = DateTime.Now;
            return base.Insert(connectionHandler, obj);
        }

        public IEnumerable<Folder> GetAll(IConnectionHandler connectionHandler, bool isexternal)
        {
            var da = new FolderDA(connectionHandler);
            return da.GetAll(isexternal);
        }

        public IEnumerable<Folder> GetParents(IConnectionHandler connectionHandler, bool isexternal)
        {
            var da = new FolderDA(connectionHandler);
            return da.GetParents(isexternal);
        }
        public Folder GetFirstParent(IConnectionHandler connectionHandler, bool isexternal)
        {
            var da = new FolderDA(connectionHandler);
            return da.GetFirstParent(isexternal);
        }
      
    }
}
