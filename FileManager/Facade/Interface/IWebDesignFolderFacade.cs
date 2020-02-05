using System;
using System.Collections.Generic;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;


namespace Radyn.FileManager.Facade.Interface
{
public interface IWebDesignFolderFacade : IBaseFacade<WebDesignFolder>
{
    IEnumerable<FileManager.DataStructure.Folder> GetParents(Guid websiteId
);

    bool Insert(Guid websiteId, FileManager.DataStructure.Folder folder);
   
    }
}
