using System;
using System.Collections.Generic;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;
namespace Radyn.FileManager.Facade.Interface
{
   public interface IFolderFacade :IBaseFacade<Folder>
   {
        IEnumerable<Folder> GetAll(bool isexternal);
        IEnumerable<Folder> GetParents(bool isexternal=false);
       Folder GetFirstParent(bool isexternal = false);


       List<Folder> GetWithChilds(Guid folderId);
   }
}
