using System;
using System.Collections.Generic;
using System.Web;
using Radyn.FileManager.DataStructure;
using Radyn.Framework;
namespace Radyn.FileManager.Facade.Interface
{
    public interface IFileFacade : IBaseFacade<File>
    {

        Guid InsertFile(File file);
        Guid Insert(HttpPostedFileBase file);
        bool Insert(List<HttpPostedFileBase> file);
        Guid Insert(HttpPostedFileBase file, File fileUptions);
        bool Insert(List<HttpPostedFileBase> file, File fileUptions);
        
        bool Update(HttpPostedFileBase file, Guid fileId);
        bool Update(HttpPostedFileBase file, Guid fileId, File fileuptions);
        bool Update(Guid fileId, File fileoptions);




    }

}
